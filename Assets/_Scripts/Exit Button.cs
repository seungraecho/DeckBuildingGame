using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public GameObject UIObject;

    public void MyFunction()
    {
        //UI 활성화
        UIObject.SetActive(false);
    }
}