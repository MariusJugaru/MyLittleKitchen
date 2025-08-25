using UnityEngine;

public class EggPourScript : ItemFunctionManager
{

    public BowlWhiskingScript bowlWhiskingScript;

    // Update is called once per frame
    void Update()
    {
        if (UseItem() && bowlWhiskingScript.hasBeatenEggs)
        {
            Transform equipment = hit.transform;
            while (!equipment.CompareTag("Equipment"))
            {
                equipment = equipment.parent;
            }
            if (equipment.name != "PanGood") return;

            bowlWhiskingScript.hasBeatenEggs = false;
            bowlWhiskingScript.beatenEggs.SetActive(false);

            Transform omelettePrefab = equipment.Find("Prefabs").Find("OmeletteManager");
            Transform omelette = Instantiate(omelettePrefab, omelettePrefab.position, omelettePrefab.rotation);
            omelette.SetParent(equipment.Find("Items"));

            StoreItemsScript storeItemsScript = equipment.GetComponent<StoreItemsScript>();
            if (storeItemsScript.isHeating)
            {
                omelette.GetComponent<CookingScript>().enabled = true;
                omelette.GetComponent<AudioSource>().enabled = true;
            }

            omelette.gameObject.SetActive(true);
        }   
    }
}
