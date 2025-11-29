using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void LoadIngameScene()
    {
        SceneManager.LoadScene("store");
    }
}
