using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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


    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnKeyPressed(KeyCode.E, button))
        {
            CheckFood();
        }
    }

    void CheckFood()
    {
        // turn off the collider while checking, so new plates can't be added.
        BoxCollider trigger = transform.GetComponent<BoxCollider>();
        // trigger.enabled = false;

        foreach (PlateRequirements plateReq in allPlates)
        {
            List<int> scores = CheckPlate(plateReq);

            foreach (int score in scores)
                Debug.Log("Score: " + score);


        }

        // trigger.enabled = true;
    }

    List<int> CheckPlate(PlateRequirements expectedPlate)
    {
        List<int> scores = new List<int>();

        foreach (GameObject plate in plates)
        {
            Transform items = plate.transform.Find("Items");
            List<GameObject> foodList = new List<GameObject>();

            Debug.Log(items);

            if (items == null)
            {
                Debug.Log("Items not found");
                return null;
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
            scores.Add(score);
        }

        return scores;
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
