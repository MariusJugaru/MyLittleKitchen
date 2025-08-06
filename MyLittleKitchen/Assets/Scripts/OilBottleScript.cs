using UnityEngine;

public class OilBottleScript : MonoBehaviour
{
    [Header("Camera")]
    private Camera cam;
    public float maxDistToItem = 2;

    private RaycastHit hit;
    public AudioSource audioSrc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PourOil();
        }
    }

    void PourOil()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        StoreItemsScript storeScript;

        if (Physics.Raycast(ray, out hit, maxDistToItem, ~0, QueryTriggerInteraction.Ignore) &&
            (storeScript = hit.transform.GetComponent<StoreItemsScript>()))
        {
            float randomValue = Random.Range(0.8f, 1.1f);
            audioSrc.pitch = randomValue;
            audioSrc.Play();

            storeScript.hasOil = true;
            if (storeScript.oil)
            {
                if (!storeScript.oil.activeSelf)
                {
                    storeScript.oil.SetActive(true);
                    return;
                }

                if (storeScript.maxOilHeight >= storeScript.oil.transform.localPosition.y)
                {
                    storeScript.oil.transform.localPosition += new Vector3(0, 0.01f, 0);
                    Debug.Log(storeScript.oil.transform.localPosition.y);
                }
                    
                
            }
                
        }
    }
}
