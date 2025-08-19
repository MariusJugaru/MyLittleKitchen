using UnityEngine;

public class FoodManager : MonoBehaviour
{

    [Header("Can be peeled")]
    public bool canPeel = false;
    public GameObject peeledPrefab;

    [Header("Can be cut")]
    public bool canCut = false;
    public bool multipleCutPrefabs = false;
    public GameObject[] cutPrefab;


}
