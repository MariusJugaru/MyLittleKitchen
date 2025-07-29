using System;
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

    [Header("Audio")]
    public AudioSource audioSrc;
    public AudioSource audioFlame;
    public AudioSource audioFlameOn;

    [Header("Particles")]
    public ParticleSystem particles;

    public bool preHeat = false;

    private void StopParticles()
    {
        particles.Stop();
    }

    private new void Start()
    {
        base.Start();

        particles.Play();
        StopParticles();

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
        if (OnKeyPressed(KeyCode.E, button) || preHeat)
        {
            preHeat = false;
            if (!on)
            {
                audioFlame.time = 0;
                audioFlame.Play();
                audioSrc.Play();
                audioFlameOn.Play();
                particles.Play();
            }
            else
            {
                audioFlame.Stop();
                particles.Stop();
            }
                
            on = !on;
            button.transform.rotation *= Quaternion.Euler(90, 0, 0);
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
            {
                EnableAllChildrenScripts(items.gameObject);
                EnableAllChildrenSounds(items.gameObject);
            }
            else
            {
                DisableAllChildrenScripts(items.gameObject);
                DisableAllChildrenSounds(items.gameObject);
            }
                
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
                    DisableAllChildrenSounds(items.gameObject);
                }
                Debug.Log("Exit:" + equipment);

                equipment = null;
                equipmentCollider = null;
            }

        }

        return true;
    }

    void EnableAllChildrenScripts(GameObject parent, bool hasOil = false)
    {
        MonoBehaviour[] scripts = parent.GetComponentsInChildren<CookingScript>(true);
        // MonoBehaviour outline = parent.GetComponent<Outline>();

        foreach (CookingScript script in scripts)
        {
            if (script.gameObject == parent) continue;

            script.enabled = true;
            if (hasOil)
                script.hasOil = true;
            
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

    void EnableAllChildrenSounds(GameObject parent)
    {
        AudioSource[] audios = parent.GetComponentsInChildren<AudioSource>(true);

        foreach (AudioSource audio in audios)
        {
            audio.enabled = true;
        }
    }

    void DisableAllChildrenSounds(GameObject parent)
    {
        AudioSource[] audios = parent.GetComponentsInChildren<AudioSource>(true);

        foreach (AudioSource audio in audios)
        {
            audio.enabled = false;
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

            StoreItemsScript storeItemsScript = equipment.GetComponent<StoreItemsScript>();

            if (on)
            {
                // if the equipment has items and the heating source is on enable their scripts
                // the food has a cooking script
                EnableAllChildrenScripts(items.gameObject, storeItemsScript.hasOil);
                EnableAllChildrenSounds(items.gameObject);
            }
        }
        else if (item.CompareTag("Food") && on &&
            item.transform.parent != null && item.transform.parent.parent != null &&
            item.transform.parent.parent.name == "Items")
        {
            // MonoBehaviour[] scripts = item.transform.parent.GetComponents<MonoBehaviour>();
            // MonoBehaviour outline = item.transform.parent.GetComponent<Outline>();

            // foreach (MonoBehaviour script in scripts)
            // {
            //     if (script != outline)
            //         script.enabled = true;
            // }

            CookingScript cookingScript = item.transform.parent.GetComponent<CookingScript>();
            if (cookingScript)
                cookingScript.enabled = true;

            Transform equipment = item.transform.parent.parent.parent;
            if (equipment != null && equipment.CompareTag("Equipment") &&
                equipment.GetComponent<StoreItemsScript>().hasOil)
                cookingScript.hasOil = true;

            AudioSource audio = item.transform.parent.GetComponent<AudioSource>();
            if (audio)
                audio.enabled = true;
        }

    }

}
