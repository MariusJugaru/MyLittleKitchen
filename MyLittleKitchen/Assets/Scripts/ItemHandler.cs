using UnityEngine;
using UnityEngine.UIElements;

public class ItemHandler : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;
    public float maxDistToItem;

    [Header("Hands")]
    public GameObject leftHand;
    public GameObject rightHand;

    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Transform item = rightHand.transform.childCount > 0 ? rightHand.transform.GetChild(0) : null;

            if (item != null)
            {
                PlaceItem(rightHand, item);
                return;
            }
            PickItem(rightHand);
        }

    }
    
    void DisableAllChildColliders(GameObject parent)
    {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    void EnableAllChildColliders(GameObject parent)
    {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }

    private void PlaceItem(GameObject hand, Transform item)
    {
        if (hand == null)
        {
            Debug.Log("Hand is not valid.");
            return;
        }

        // Raycast from player camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistToItem))
        {
            Vector3 hitpoint = hit.point;
            Transform placePos = item.transform.Find("PlacePos");

            Vector3 offset;
            if (placePos == null)
            {
                offset = new Vector3(0, item.localScale.y / 2f + 0.1f, 0);
            }
            else
            {
                offset = item.position - placePos.position;
                Debug.Log("offset= " + offset);
            }

            Vector3 newPos = hitpoint + offset;

            // Place item at the mouse position
            item.SetParent(null, worldPositionStays: false);
            item.localPosition = newPos;

            // Set new rotation
            Vector3 currentRotItem = item.rotation.eulerAngles;
            Vector3 currentRotCam = cam.transform.rotation.eulerAngles;
            Vector3 newRotation = new Vector3(currentRotItem.x, currentRotCam.y, currentRotItem.z);

            item.rotation = Quaternion.Euler(newRotation);

            BoxCollider box = item.GetComponent<BoxCollider>();
            Rigidbody body = item.GetComponent<Rigidbody>();

            // activate boxCollider and rigidbody
            if (box != null)
                box.enabled = true;
            if (body != null)
                body.isKinematic = false;
            EnableAllChildColliders(item.gameObject);

            // deactivate item scripts
            MonoBehaviour[] scripts = item.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
                script.enabled = false;

            Debug.Log(hitpoint);
        }
    }

    // Picks up 
    private void PickItem(GameObject hand)
    {
        if (hand == null)
        {
            Debug.Log("Hand is not valid.");
            return;
        }

        // Raycast from player camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistToItem) && hit.transform.CompareTag("Item"))
        {
            Transform item;
            if (hit.transform.parent == null)
            {
                item = hit.transform;
            }
            else
            {
                item = hit.transform.parent;
            }

            BoxCollider box = item.GetComponent<BoxCollider>();
            Rigidbody body = item.GetComponent<Rigidbody>();

            // deactivate boxCollider and rigidbody
            if (box != null)
                box.enabled = false;
            if (body != null)
                body.isKinematic = true;

            // move item into the hand
            item.SetParent(hand.transform, worldPositionStays: false);
            item.localPosition = Vector3.zero;
            item.localRotation = Quaternion.identity;

            DisableAllChildColliders(item.gameObject);

            // activate item scripts
            MonoBehaviour[] scripts = item.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
                script.enabled = true;

            Debug.Log(item);
        }
    }
}
