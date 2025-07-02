using UnityEngine;

public class PlacementHoverScript : MonoBehaviour
{
    public GameObject hoverCirclePrefab;
    private GameObject hoverCircle;

    [Header("Camera")]
    public Camera cam;
    public float maxDistToItem;

    private RaycastHit hit;

    void FixedUpdate()
    {
        DrawHover();
    }

    private void DrawHover()
    {
        // Raycast from player camera to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistToItem))
        {
            Vector3 newPos = hit.point;

            if (hoverCircle == null)
                hoverCircle = Instantiate(hoverCirclePrefab, newPos, Quaternion.identity);
            else
                hoverCircle.transform.position = newPos;
        }
        else
        {
            if (hoverCircle != null)
                Destroy(hoverCircle);
        }
    }

    private void OnDisable()
    {
        if (hoverCircle != null)
            Destroy(hoverCircle);
    }
}
