using UnityEngine;

public class HeatScript : ButtonScript
{
    [Header("Heat source")]
    [Range(1, 3)]
    public int heat = 2;
    private bool on = false;

    private

    // Update is called once per frame
    void Update()
    {
        if (OnKeyPressed(KeyCode.E, button))
        {
            on = !on;
            button.transform.rotation *= Quaternion.Euler(0, 0, 90);
            Debug.Log("Pressed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (on) {
            GameObject item;
            if (other.transform.parent != null)
            {
                item = other.transform.parent.gameObject;
            }
            else
            {
                item = other.transform.gameObject;
            }
            Debug.Log("Trigger:" + item);
        }
        
    }
}
