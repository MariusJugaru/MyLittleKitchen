using UnityEngine;
using UnityEngine.UI;

public class EggScript : MonoBehaviour
{
    public GameObject rawEggPrefab;
    public GameObject eggShellPrefab;

    [Header("Camera")]
    private Camera cam;
    public float maxDistToItem = 2;

    private RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        PlaceEgg();
    }

    private void PlaceEgg()
    {
        // Raycast from player camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistToItem, ~0, QueryTriggerInteraction.Ignore))
        {
            Vector3 newPos = hit.point + new Vector3(0, 0.05f, 0);

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(rawEggPrefab, newPos, Quaternion.identity);

                ItemHandler.isItem = false;
                ItemHandler.changedState = true;

                GameObject shell = Instantiate(eggShellPrefab, transform.position, transform.rotation);
                shell.transform.SetParent(transform.parent);

                ItemHandler.DisableAllChildColliders(shell);
                ItemHandler.DisableAllChildRigidbody(shell);
                
                Destroy(transform.gameObject);
            }
        }
    }
}
