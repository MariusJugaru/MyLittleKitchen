using UnityEngine;

public class WhiskScript : ItemFunctionManager
{

    private Transform paw;
    private Animator pawAnimator;

    // Update is called once per frame
    void Update()
    {
        if (UseItem())
        {
            paw = transform.parent.parent.parent;
            if (!paw.CompareTag("Paw"))
            {
                paw = transform.parent.parent;
                if (!paw.CompareTag("Paw"))
                {
                    Debug.Log("Hand does not have the paw tag or something went wrong");
                    return;
                }
            }


            BowlWhiskingScript bowlWhiskingScript = hit.transform.GetComponent<BowlWhiskingScript>();
            if (!bowlWhiskingScript) return;

            int eggsCount = 0;

            

            foreach (Transform child in hit.transform.Find("Items"))
            {
                CookingScript cookingScript = child.GetComponent<CookingScript>();
                if (!cookingScript) continue;

                if (cookingScript.foodType == CookingScript.FoodType.FriedEgg && cookingScript.state == CookingScript.CookState.raw)
                {
                    eggsCount++;
                    Destroy(child.gameObject);
                }

            }

            if (eggsCount == 0) return;

            pawAnimator = paw.GetComponent<Animator>();
            pawAnimator.SetTrigger("IsUsingWhisk");

            bowlWhiskingScript.hasBeatenEggs = true;
            bowlWhiskingScript.beatenEggs.SetActive(true);
        }
    }
}
