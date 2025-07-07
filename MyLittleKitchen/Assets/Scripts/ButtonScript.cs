using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private Camera cam;
    private RaycastHit hit;

    [Header("Button")]
    public GameObject button;
    public float maxDistToItem = 3f;

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

            if (Physics.Raycast(ray, out hit, maxDistToItem) && hit.transform == objectTouched.transform)
                return true;
            else
                return false;
        }
        return false;
    }
}
