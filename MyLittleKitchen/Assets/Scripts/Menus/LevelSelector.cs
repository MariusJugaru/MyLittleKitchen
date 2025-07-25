using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    
    public void PlayLevel1()
    {
        SceneManager.LoadScene("MainGame");
    }
}
