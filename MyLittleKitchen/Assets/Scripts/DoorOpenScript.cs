using UnityEngine;

public class DoorOpenScript : ButtonScript
{
    [Header("Open direction")]
    public bool clockwise = false;

    [Header("Audio")]
    public AudioSource audioSrc;
    public AudioClip openDoor;
    public AudioClip closeDoor;

    private bool open = false;

    // Update is called once per frame
    void Update()
    {
        if (!OnKeyPressed(KeyCode.E, button)) return;

        if (open)
        {
            if (clockwise)
                transform.rotation *= Quaternion.Euler(0, 90, 0);
            else
                transform.rotation *= Quaternion.Euler(0, -90, 0);
            open = false;

            if (!audioSrc) return;
            audioSrc.clip = closeDoor;
            audioSrc.Play();
        }
        else
        {
            if (clockwise)
                transform.rotation *= Quaternion.Euler(0, -90, 0);
            else
                transform.rotation *= Quaternion.Euler(0, 90, 0);
            open = true;

            if (!audioSrc) return;
            audioSrc.clip = openDoor;
            audioSrc.Play();
        }
    }
}
