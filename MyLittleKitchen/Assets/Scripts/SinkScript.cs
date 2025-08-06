using Unity.VisualScripting;
using UnityEngine;

public class SinkScript : ButtonScript
{

    public AudioSource audioSrc;

    private bool sinkOn = false;
    private Transform item;
    private StoreItemsScript storeItemsScript;


    // Update is called once per frame
    void Update()
    {
        if (OnKeyPressed(KeyCode.E, button))
        {
            if (sinkOn)
            {
                sinkOn = false;
                audioSrc.Stop();
            }
            else
            {
                sinkOn = true;
                audioSrc.Play();
            }
        }

        if (sinkOn && storeItemsScript && storeItemsScript.water)
        {
            storeItemsScript.water.SetActive(true);
            if (storeItemsScript.maxOilHeight >= storeItemsScript.water.transform.localPosition.y)
            {
                storeItemsScript.water.transform.localPosition += new Vector3(0, 0.05f * Time.deltaTime, 0);
            }
        }

        item = null;
        storeItemsScript = null;


    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Equipment")) return;

        item = other.transform;
        storeItemsScript = item.GetComponent<StoreItemsScript>();
    }
}
