using UnityEngine;
using UnityEngine.UI;

public class LoadStarsMenuScript : MonoBehaviour
{
    [Header("Level")]
    public string level;

    [Header("Stars")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Sprite star;

    void Start()
    {
        int stars = PlayerPrefs.GetInt(level, 0);

        if (stars >= 1)
            star1.GetComponent<Image>().sprite = star;
        if (stars >= 2)
            star2.GetComponent<Image>().sprite = star;
        if (stars == 3)
            star3.GetComponent<Image>().sprite = star;
    }

}
