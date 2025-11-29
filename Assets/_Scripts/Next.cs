using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public void LoadIngameScene()
    {
        SceneManager.LoadScene("store");
    }
}
