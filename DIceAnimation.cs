using UnityEngine;

public class DIceAnimation : MonoBehaviour
{

    Transform diceTransform;

    private float _diceTransformRotationX;

    private float _diceTransformRotationY;

    private float _diceTransformRotationZ;


    Vector3 _diceTransformPosition;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        diceTransform = GetComponent<Transform>();


        _diceTransformPosition = diceTransform.position;



        _diceTransformRotationX = 0;

        _diceTransformRotationY = 0;

        _diceTransformRotationZ = 0;

    }

    // Update is called once per frame
    void Update()
    {

        _diceTransformRotationX = _diceTransformRotationX + 300 * Time.deltaTime;

        _diceTransformRotationY += 300 * Time.deltaTime;

        _diceTransformRotationZ += 300 * Time.deltaTime;



        _diceTransformPosition.x = _diceTransformRotationX;

        _diceTransformPosition.y = _diceTransformRotationY;

        _diceTransformPosition.z = _diceTransformRotationZ;


        diceTransform.rotation = Quaternion.Euler(_diceTransformPosition);




        
    }



    private void OnMouseEnter()
    {
        print("Mouse is On Dice.");
    }

    private void OnMouseExit() 
    { 
        print("Mouse left on Dice."); 
    }



    private void OnMouseDown()
    {
        print("Mouse click Dice");
    }

}
