using System.Threading;
using UnityEngine;

public class EggTimerScript : MonoBehaviour
{
    [Header("Spinning part")]
    public GameObject eggTop;

    private float initialRotation;
    private float rotation;

    public float sensX = 400;

    public AudioSource audioSource;
    public AudioClip timerTicking;
    public AudioClip timerRinging;

    private bool isSetting = false;
    private bool isFinished = true;

    public static bool isHoldingClick = false;
    public bool isHoldingEgg = false;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (eggTop)
        {
            initialRotation = eggTop.transform.localEulerAngles.y;
            rotation = initialRotation;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isHoldingEgg) return;

            isSetting = true;
            isFinished = false;
            isHoldingClick = true;

            // get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;

            rotation = Mathf.Repeat(eggTop.transform.localEulerAngles.y, 360f) + mouseX;

            //if (rotation > 0 && rotation < 360)
            eggTop.transform.localEulerAngles += new Vector3(0, mouseX, 0);

        }
        else
        {
            isHoldingClick = false;
            if (isFinished) return;
            if (isSetting)
            {
                isSetting = false;
                audioSource.clip = timerTicking;
                audioSource.time = 0;
                audioSource.loop = true;
                audioSource.Play();
            }

            rotation = Mathf.Repeat(eggTop.transform.localEulerAngles.y, 360f) - 6 * Time.deltaTime;
            if (rotation < 0)
            {
                isFinished = true;
                audioSource.clip = timerRinging;
                audioSource.time = 0;
                audioSource.loop = false;
                audioSource.Play();
            }

            eggTop.transform.localEulerAngles -= new Vector3(0, 6 * Time.deltaTime, 0);
        }
        
    }
}
