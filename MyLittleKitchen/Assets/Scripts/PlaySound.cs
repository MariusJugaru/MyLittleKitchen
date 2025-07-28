using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSrc;

    // Update is called once per frame
    void Update()
    {
        if (audioSrc && Input.GetMouseButtonDown(0))
        {
            float randomValue = Random.Range(0.8f, 1.0f);
            audioSrc.pitch = randomValue;

            audioSrc.Play();
        }
        
    }
}
