using UnityEngine;

public class WhiskScript : ItemFunctionManager
{

    // Update is called once per frame
    void Update()
    {
        if (UseItem())
        {
            BowlWhiskingScript bowlWhiskingScript = hit.transform.GetComponent<BowlWhiskingScript>();
            if (!bowlWhiskingScript) return;

            bowlWhiskingScript.hasBeatenEggs = true;
            bowlWhiskingScript.beatenEggs.SetActive(true);
        }
    }
}
