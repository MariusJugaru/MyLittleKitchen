using UnityEngine;

public class EggCrackScript : MonoBehaviour
{
    public GameObject crackedEggPrefab;

    [Header("Camera")]
    private Camera cam;
    public float maxDistToItem = 3;

    private RaycastHit hit;

    private Transform paw;
    private Animator pawAnimator;

    private bool isCracked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CrackEgg();
    }

    private void CrackEgg()
    {
        if (!Input.GetMouseButtonDown(0) || isCracked) return;
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistToItem, ~0, QueryTriggerInteraction.Ignore))
        {
            isCracked = true;
            
            paw = transform.parent.parent;
            if (!paw.CompareTag("Paw"))
            {
                Debug.Log("Hand does not have the paw tag or something went wrong");
                return;
            }

            // paw rotates dow and up
            pawAnimator = transform.parent.parent.GetComponent<Animator>();
            pawAnimator.SetBool("isCracking", true);

            transform.GetComponent<AudioSource>().Play();

            Invoke(nameof(ChangeEggPrefab), 0.2f);
        }
    }

    private void ChangeEggPrefab()
    {
        pawAnimator.SetBool("isCracking", false);
        GameObject eggCracked = Instantiate(crackedEggPrefab, transform.position, transform.rotation);
        eggCracked.transform.SetParent(transform.parent);
        ItemHandler.DisableAllChildColliders(eggCracked);
        ItemHandler.DisableAllChildRigidbody(eggCracked);

        Destroy(gameObject);
    }
}
