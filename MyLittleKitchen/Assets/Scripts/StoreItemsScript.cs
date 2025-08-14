using UnityEngine;

public class StoreItemsScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject items;

    [Header("Oil")]
    public GameObject oil;
    public bool hasOil = false;
    public float maxOilHeight = 0.2f;

    [Header("Water")]
    public GameObject water;
    public bool hasWater = false;
    public float maxWaterOil = 0.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (items == null)
        {
            Debug.Log("Please set items parent game object");
            return;
        }

        if (!other.CompareTag("Item") && !other.CompareTag("Food") && !other.CompareTag("FoodItem")) return;

        Transform item = other.transform;
        if (item.parent != null && (item.parent.CompareTag("Food") || item.parent.CompareTag("Item")))
        {
            item = item.parent;
        }

        item.SetParent(items.transform, worldPositionStays: true);

        Debug.Log("Item stored " + gameObject);

        // Modify extra modifiers for the cooking script
        CookingScript cookingScript = item.GetComponent<CookingScript>();
        if (!cookingScript) return;

        if (hasWater) cookingScript.hasWater = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Item") && !other.CompareTag("Food") && !other.CompareTag("FoodItem")) return;

        Transform item = other.transform;
        if (item.parent != null && item.parent.CompareTag("Food"))
        {
            item = item.parent;
        }

        item.SetParent(null, worldPositionStays: true);

        CookingScript cookingScript = item.GetComponent<CookingScript>();
        if (!cookingScript) return;

        if (hasWater) cookingScript.hasWater = false;
    }
}
