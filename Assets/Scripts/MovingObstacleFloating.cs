using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleFloating : MonoBehaviour {
    
    [SerializeField] public float moveSpeed = 3.5f;
    [SerializeField] public float frequency = 3.5f;
    [SerializeField] public float magnitude = 0.5f;

    Vector3 initialPosition;
    bool facingUp;

    // Start is called before the first frame update
    void Start(){ initialPosition = transform.position; }

    // Update is called once per frame
    void Update(){

        CheckWhereToFace();

        if (facingUp){ MoveRight(); }
                else { MoveLeft(); }
    }

    void CheckWhereToFace(){
             if (initialPosition.y < 0.5f){ facingUp = true; }
        else if (initialPosition.y > 4.5f){ facingUp = false; }
    }

    void MoveRight(){
        initialPosition += transform.up * Time.deltaTime * moveSpeed;
        transform.position = initialPosition + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
    void MoveLeft(){
        initialPosition -= transform.up * Time.deltaTime * moveSpeed;
        transform.position = initialPosition + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

}