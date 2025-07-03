using UnityEngine;

public class PlacementHoverScript : MonoBehaviour
{
    public GameObject hoverCirclePrefab;
    public float size = 1;
    private GameObject hoverCircle;

    [Header("Camera")]
    private Camera cam;
    public float maxDistToItem;

    private RaycastHit hit;

    void Start()
    {
        cam = Camera.main;
    }

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
            {
                hoverCircle = Instantiate(hoverCirclePrefab, newPos, Quaternion.identity);
                Vector3 circleScale = hoverCircle.transform.localScale;
                Vector3 scaleModif = new Vector3(size, 1, size);
                hoverCircle.transform.localScale = Vector3.Scale(circleScale, scaleModif);
            }
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
