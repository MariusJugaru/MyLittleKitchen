using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private Camera cam;
    private RaycastHit hit;

    [Header("Button")]
    public GameObject button;
    public float maxDistToItem = 2f;

    public void Start()
    {
        cam = Camera.main;
    }

    // returns true if the game object button got pressed, else false.
    protected bool OnKeyPressed(KeyCode key, GameObject objectTouched)
    {
        if (Input.GetKeyDown(key))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxDistToItem, ~0, QueryTriggerInteraction.Ignore) &&
                hit.transform == objectTouched.transform)
                return true;
            else
                return false;
        }
        return false;
    }
}
