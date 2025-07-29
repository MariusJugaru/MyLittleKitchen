using UnityEngine;

public class SpatulaScript : MonoBehaviour
{
    [Header("Camera")]
    private Camera cam;
    public float maxDistToItem = 2;

    private Transform itemHolder;
    private RaycastHit hit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        itemHolder = transform.Find("Item");
    }

    void Update()
    {
        // Spatula can hold only a "Food" item, you can interact with it with left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit, maxDistToItem, ~0, QueryTriggerInteraction.Ignore)) return;

            Transform item;
            if (itemHolder.childCount == 0)
            {
                item = hit.transform;

                if (!item.CompareTag("Food")) return;
                if (item.parent != null && item.parent.CompareTag("Food"))
                    item = item.parent;

                CookingScript cookingScript = item.GetComponent<CookingScript>();
                if (cookingScript.needsOil && !cookingScript.hasOil && cookingScript.clickReq != 0)
                {
                    cookingScript.clickReq--;

                    Transform equipment = item.parent.parent;
                    if (!equipment || !equipment.CompareTag("Equipment")) return;

                    AudioSource audioSrc = equipment.GetComponent<AudioSource>();
                    if (audioSrc) audioSrc.Play();
                    
                    return;
                }

                PickItem(item);
            }
            else
            {
                item = itemHolder.GetChild(0);
                Vector3 newPos = hit.point + new Vector3(0, 0.05f, 0);

                DropItem(item, newPos);
            }
                
        }
    }

    void PickItem(Transform item)
    {
        BoxCollider box = item.GetComponent<BoxCollider>();
        Rigidbody body = item.GetComponent<Rigidbody>();

        // deactivate boxCollider and rigidbody
        if (box != null)
            box.enabled = false;
        if (body != null)
            body.isKinematic = true;
        ItemHandler.DisableAllChildColliders(item.gameObject);
        ItemHandler.DisableAllChildRigidbody(item.gameObject);

        // move item to item holder
        item.SetParent(itemHolder.transform, worldPositionStays: false);
        item.GetChild(0).localPosition = Vector3.zero;
        item.GetChild(0).localRotation = Quaternion.identity;
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;

        MonoBehaviour cookingScript = item.GetComponent<CookingScript>();
        if (cookingScript)
            cookingScript.enabled = false;
        MonoBehaviour placementHover = item.GetComponent<PlacementHoverScript>();
        if (placementHover)
            placementHover.enabled = true;
        AudioSource audio = item.GetComponent<AudioSource>();
        if (audio)
            audio.enabled = false;
    }

    void DropItem(Transform item, Vector3 newPos)
    {
        BoxCollider box = item.GetComponent<BoxCollider>();
        Rigidbody body = item.GetComponent<Rigidbody>();

        // deactivate boxCollider and rigidbody
        if (box != null)
            box.enabled = true;
        if (body != null)
            body.isKinematic = false;
        ItemHandler.EnableAllChildColliders(item.gameObject);
        ItemHandler.EnableAllChildRigidbody(item.gameObject);

        Debug.Log("ITEM: " + item);
        MonoBehaviour placementHover = item.GetComponent<PlacementHoverScript>();
        if (placementHover)
            placementHover.enabled = false;

        // move item to new position
        item.SetParent(null, worldPositionStays: false);
        item.localPosition = newPos;
        
        // Set new rotation
        Vector3 currentRotItem = item.rotation.eulerAngles;
        Vector3 currentRotCam = cam.transform.rotation.eulerAngles;
        Vector3 newRotation = new Vector3(currentRotItem.x, currentRotCam.y, currentRotItem.z);

        item.rotation = Quaternion.Euler(newRotation);
    }
}
