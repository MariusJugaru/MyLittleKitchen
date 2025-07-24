using Unity.VisualScripting;
using UnityEngine;

public class TrashcanScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Transform obj = other.transform;

        if (obj.CompareTag("Untagged") && obj.parent)
            obj = obj.parent;

        if (obj.CompareTag("Food") || obj.CompareTag("Item"))
        {
            // Debug.Log(other.transform);
            Destroy(obj.gameObject);
        }
            
    }
}
