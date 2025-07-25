using UnityEngine;

public class AutoDeleteScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(Delete), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
