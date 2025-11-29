using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void LoadIngameScene()
    {
        SceneManager.LoadScene("NextSceneTest");
    }
}
