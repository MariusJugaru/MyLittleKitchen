using UnityEngine;

public class HeatScript : ButtonScript
{
    [Header("Heat source")]
    [Range(1, 3)]
    public int heat = 2;
    private bool on = false;
    private bool changedState = false;

    public GameObject equipment;
    private Collider triggerCollider;
    public Collider equipmentCollider;

    private new void Start()
    {
        base.Start();

        // get the trigger collider for the heat source
        Collider[] colliders = GetComponents<Collider>();

        foreach (Collider col in colliders)
        {
            if (col.isTrigger)
            {
                triggerCollider = col;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OnKeyPressed(KeyCode.E, button))
        {
            on = !on;
            button.transform.rotation *= Quaternion.Euler(0, 0, 90);
            Debug.Log("Pressed");

            changedState = true;
        }

        bool flowControl = CheckEquipmentCollider();
        if (!flowControl)
        {
            return;
        }

        // if the equipment is still on the heating source but the heat state has changed makes changes to food
        if (changedState)
        {
            changedState = false;

            if (equipment == null || equipmentCollider == null) return;

            Transform items = equipment.transform.Find("Items");
            if (items == null) return;

            if (on)
                EnableAllChildrenScripts(items.gameObject);
            else
                DisableAllChildrenScripts(items.gameObject);
        }
    }

    // checks if the equipment is still on the heating source
    private bool CheckEquipmentCollider()
    {
        if (equipment != null)
        {
            if (triggerCollider == null)
            {
                Debug.Log("no trigger collider");
                return false;
            }
            if (equipmentCollider == null)
            {
                Debug.Log("no equipment collider");
                return false;
            }
            bool isInsideTrigger = triggerCollider.bounds.Intersects(equipmentCollider.bounds);

            if (!isInsideTrigger || equipmentCollider.enabled == false)
            {
                Transform items = equipment.transform.Find("Items");
                if (items != null)
                {
                    DisableAllChildrenScripts(items.gameObject);
                }
                Debug.Log("Exit:" + equipment);

                equipment = null;
                equipmentCollider = null;
            }

        }

        return true;
    }

    void EnableAllChildrenScripts(GameObject parent)
    {
        MonoBehaviour[] scripts = parent.GetComponentsInChildren<MonoBehaviour>(true);

        foreach (MonoBehaviour script in scripts)
        {
            if (script.gameObject != parent)
            {
                script.enabled = true;
            }
        }
    }

    void DisableAllChildrenScripts(GameObject parent)
    {
        MonoBehaviour[] scripts = parent.GetComponentsInChildren<MonoBehaviour>(true);

        foreach (MonoBehaviour script in scripts)
        {
            if (script.gameObject != parent)
            {
                script.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject item;
        item = other.transform.gameObject;

        Transform items = null;
        
        if (item.CompareTag("Equipment"))
        {
            equipment = item;
            equipmentCollider = equipment.GetComponent<Collider>();
            items = item.transform.Find("Items");
            Debug.Log("Trigger:" + item);
        }

        if (on && items != null)
        {
            // if the equipment has items and the heating source is on enable their scripts
            // the food has a cooking script
            EnableAllChildrenScripts(items.gameObject);
        }

    }

}
