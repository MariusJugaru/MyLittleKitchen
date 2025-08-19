using UnityEngine;

public class ItemFunctionManager : MonoBehaviour
{
    [Header("Camera")]
    private Camera cam;
    public float maxDistToItem = 2;

    protected RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }


    public bool UseItem()
    {
        if (!Input.GetMouseButtonDown(0)) return false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistToItem, ~0, QueryTriggerInteraction.Ignore))
            return true;
        return false;
    }

}
