using Unity.VisualScripting;
using UnityEngine;

public class TrashcanScript : MonoBehaviour
{
    public AudioSource audioSrc;

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
            float randomValue = Random.Range(0.8f, 1.2f);
            audioSrc.pitch = randomValue;
            audioSrc.Play();
            Destroy(obj.gameObject);
        }
            
    }
}
