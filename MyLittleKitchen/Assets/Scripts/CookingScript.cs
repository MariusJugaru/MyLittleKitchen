using UnityEngine;

public class CookingScript : MonoBehaviour
{
    [Header("Cook state")]
    public GameObject cookedPrefab;
    public GameObject slightlyBurntPrefab;
    public GameObject burntPrefab;

    public CurrentState state = CurrentState.raw;
    public enum CurrentState
    {
        raw,
        cooked,
        slightlyBurnt,
        burnt
    };

    [Header("Cook times")]
    public float timeModifier = 1;

    public float time1;
    public float time2;
    public float time3;

    [Header("Max heat")]
    public float maxHeat;

    private float currentTime = 0;

    private GameObject food;
    private GameObject aux;

    void Start()
    {
        food = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        currentTime += Time.deltaTime * timeModifier;

        // check if the food goes to next cook state
        if (state == CurrentState.raw && currentTime >= time1)
        {
            state = CurrentState.cooked;
            currentTime = 0;

            food.SetActive(false);
            aux = Instantiate(cookedPrefab, food.transform.position, food.transform.rotation);
            aux.transform.SetParent(transform, worldPositionStays: true);

            // set the same collider and rigidbody state
            Collider collider = aux.GetComponent<Collider>();
            Rigidbody rb = aux.GetComponent<Rigidbody>();
            collider.enabled = food.GetComponent<Collider>().enabled;
            rb.isKinematic = food.GetComponent<Rigidbody>().isKinematic;

            Destroy(food);

            food = aux;
            aux = null;
        }
        else if (state == CurrentState.cooked && currentTime >= time2)
        {
            state = CurrentState.slightlyBurnt;
            currentTime = 0;

            food.SetActive(false);
            aux = Instantiate(slightlyBurntPrefab, food.transform.position, food.transform.rotation);
            aux.transform.SetParent(transform, worldPositionStays: true);

            // set the same collider and rigidbody state
            Collider collider = aux.GetComponent<Collider>();
            Rigidbody rb = aux.GetComponent<Rigidbody>();
            collider.enabled = food.GetComponent<Collider>().enabled;
            rb.isKinematic = food.GetComponent<Rigidbody>().isKinematic;

            Destroy(food);

            food = aux;
            aux = null;
        }
        else if (state == CurrentState.slightlyBurnt && currentTime >= time3)
        {
            state = CurrentState.burnt;
            currentTime = 0;

            food.SetActive(false);
            aux = Instantiate(burntPrefab, food.transform.position, food.transform.rotation);
            aux.transform.SetParent(transform, worldPositionStays: true);

            // set the same collider and rigidbody state
            Collider collider = aux.GetComponent<Collider>();
            Rigidbody rb = aux.GetComponent<Rigidbody>();
            collider.enabled = food.GetComponent<Collider>().enabled;
            rb.isKinematic = food.GetComponent<Rigidbody>().isKinematic;

            Destroy(food);

            food = aux;
            aux = null;
        }

    }
}
