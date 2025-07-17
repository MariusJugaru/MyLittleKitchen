using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ServeFoodScript : ButtonScript
{
    // list of plates that are on the serving area
    List<GameObject> plates = new List<GameObject>();

    // setting the food requirements
    [System.Serializable]
    public class PlateExpectation
    {
        public CookingScript.FoodType requiredFood;
        public int quantity;
    }

    // expectations for a single plate
    [System.Serializable]
    public class PlateRequirements
    {
        public List<PlateExpectation> expectations = new List<PlateExpectation>();
    }

    // expectations for multiple plates
    [Header("Required Food")]
    public List<PlateRequirements> allPlates = new List<PlateRequirements>();

    [Header("Score screen")]
    public GameObject scoreUI;
    public GameObject scoreText;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Sprite star;

    private float maxScore = 0;
    private float currentScore = 0;


    private new void Start()
    {
        base.Start();

        foreach (PlateRequirements req in allPlates)
        {
            foreach (PlateExpectation exp in req.expectations)
            {
                maxScore += exp.quantity * 3;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OnKeyPressed(KeyCode.E, button))
        {
            button.GetComponent<AudioSource>().Play();
            Invoke("CheckFood", 1.0f);
        }
    }

    void CheckFood()
    {
        // Sanity testing
        if (allPlates.Count == 0)
        {
            Debug.Log("All requirements have been met./ No requirements are set.");
            return;
        }
        if (plates.Count == 0)
        {
            Debug.Log("No plates on the table.");
            return;
        }

        // turn off the collider while checking, so new plates can't be added.
        BoxCollider trigger = transform.GetComponent<BoxCollider>();
        trigger.enabled = false;

        PlateRequirements plateReq = allPlates[0];
        currentScore += CheckPlate(plateReq);

        allPlates.Remove(plateReq);
        Debug.Log("Score: " + currentScore);

        trigger.enabled = true;

        if (allPlates.Count == 0)
        {
            scoreUI.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PauseMenu.GameIsPaused = true;

            if (currentScore >= maxScore / 3)
                star1.GetComponent<Image>().sprite = star;
            if (currentScore >= 2 * maxScore / 3)
                star2.GetComponent<Image>().sprite = star;
            if (currentScore == maxScore)
                star3.GetComponent<Image>().sprite = star;

            scoreText.GetComponent<TextMeshProUGUI>().text = "SCORE\n" + currentScore;
        }
    }

    int CheckPlate(PlateRequirements expectedPlate)
    {
        GameObject plate = plates[0];
        
        Transform items = plate.transform.Find("Items");
        List<GameObject> foodList = new List<GameObject>();

        Debug.Log(items);

        if (items == null)
        {
            Debug.Log("Items not found");
            return -1;
        }

        // get food managers
        Debug.Log("Foods");
        for (int i = 0; i < items.childCount; ++i)
        {
            GameObject child = items.GetChild(i).gameObject;
            foodList.Add(child);
        }


        // compute the score for the current plate
        int score = 0;
        foreach (PlateExpectation expectation in expectedPlate.expectations)
        {
            int quantity = expectation.quantity;
            for (int i = foodList.Count - 1; i >= 0; i--)
            {
                GameObject food = foodList[i];

                CookingScript manager = food.GetComponent<CookingScript>();
                if (manager == null) continue;

                // checks if the current food is expected and the quantity hasn't been reached yet
                if (expectation.requiredFood != manager.foodType) continue;
                if (quantity == 0) break;
                quantity--;

                foodList.RemoveAt(i);

                switch (manager.state)
                {
                    case CookingScript.CookState.raw:
                        score += 0;
                        break;
                    case CookingScript.CookState.cooked:
                        score += 3;
                        break;
                    case CookingScript.CookState.slightlyBurnt:
                        score += 2;
                        break;
                    case CookingScript.CookState.burnt:
                        score += 1;
                        break;
                }

                Debug.Log(manager.state);
            }
        }

        Debug.Log("plate: " + plate);

        plates.Remove(plate);
        Destroy(plate);

        return score;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Equipment") && other.transform.Find("Items") != null)
        {
            plates.Add(other.gameObject);

            foreach (GameObject obj in plates)
                Debug.Log(obj);
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Equipment"))
        {
            plates.Remove(other.gameObject);
            foreach (GameObject obj in plates)
                Debug.Log(obj);
        }

    }
}
