using UnityEngine;

public class DIceAnimation : MonoBehaviour
{

    Transform diceTransform;

    private int _diceTransformRotationX;

    private int _diceTransformRotationY;

    private int _diceTransformRotationZ;


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
        
        _diceTransformRotationX = _diceTransformRotationX + 1;

        _diceTransformRotationY += 1;

        _diceTransformRotationZ += 1;



        _diceTransformPosition.x = _diceTransformRotationX;

        _diceTransformPosition.y = _diceTransformRotationY;

        _diceTransformPosition.z = _diceTransformRotationZ;


        diceTransform.rotation = Quaternion.Euler(_diceTransformPosition);

    }
}
