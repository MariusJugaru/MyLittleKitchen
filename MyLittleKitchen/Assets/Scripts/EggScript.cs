using UnityEngine;
using UnityEngine.UI;

public class EggScript : MonoBehaviour
{
    public GameObject rawEggPrefab;

    [Header("Camera")]
    public Camera cam;
    public float maxDistToItem;

    private RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        PlaceEgg();
    }

    private void PlaceEgg()
    {
        // Raycast from player camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistToItem))
        {
            Vector3 newPos = hit.point + new Vector3(0, 0.05f, 0);

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(rawEggPrefab, newPos, Quaternion.identity);
                Destroy(transform.gameObject);
            }
        }
    }
}
