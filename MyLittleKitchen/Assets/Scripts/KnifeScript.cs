using UnityEngine;

public class KnifeScript : ItemFunctionManager
{

    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if (!UseItem()) return;

        // Check for item properties
        FoodManager foodManager = hit.transform.GetComponent<FoodManager>();
        if (!foodManager) return;

        if (foodManager.canCut)
        {
            if (!foodManager.multipleCutPrefabs)
            {
                if (!foodManager.cutPrefab[0])
                {
                    Debug.Log("Missing cut prefab");
                    return;
                }

                Instantiate(foodManager.cutPrefab[0], hit.transform.position, hit.transform.rotation);
                audioSource.Play();
                Destroy(hit.transform.gameObject);
            }
            else
            {
                // TODO: Menu for multiple cut possibilities
            }
        }
        
    }
}
