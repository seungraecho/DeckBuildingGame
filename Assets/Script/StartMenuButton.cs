using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButton : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Start Button Clicked");
        SceneManager.LoadScene("Background");
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse Entered Start Button");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited Start Button");
    }

}
