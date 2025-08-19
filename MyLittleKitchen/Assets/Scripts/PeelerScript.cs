using UnityEngine;

public class PeelerScript : ItemFunctionManager
{
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if (!UseItem()) return;

        // Check for item properties
        FoodManager foodManager = hit.transform.GetComponent<FoodManager>();
        if (!foodManager) return;

        if (foodManager.canPeel)
        {
            if (!foodManager.peeledPrefab)
            {
                Debug.Log("Missing peeled prefab");
                return;
            }

            Instantiate(foodManager.peeledPrefab, hit.transform.position, hit.transform.rotation);
            audioSource.Play();
            Destroy(hit.transform.gameObject);

        }
    }
}
