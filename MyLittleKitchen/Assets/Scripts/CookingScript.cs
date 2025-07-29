using UnityEngine;

public class CookingScript : MonoBehaviour
{
    [Header("Cook state")]
    public GameObject cookedPrefab;
    public GameObject slightlyBurntPrefab;
    public GameObject burntPrefab;

    
    public enum CookState
    {
        raw,
        cooked,
        slightlyBurnt,
        burnt
    };
    public CookState state = CookState.raw;

    public enum FoodType
    {
        FriedEgg,
    };

    public FoodType foodType;

    [Header("Oil stick")]
    public bool needsOil = false;
    public bool hasOil = false;
    public int clickReq;

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
        clickReq = Random.Range(1, 3);
    }

    void Update()
    {
        currentTime += Time.deltaTime * timeModifier;

        // check if the food goes to next cook state
        if (state == CookState.raw && currentTime >= time1)
        {
            state = CookState.cooked;
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
        else if (state == CookState.cooked && currentTime >= time2)
        {
            state = CookState.slightlyBurnt;
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
        else if (state == CookState.slightlyBurnt && currentTime >= time3)
        {
            state = CookState.burnt;
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
