using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleSides : MonoBehaviour {
    
    [SerializeField] public float moveSpeed = 3.5f;
    [SerializeField] public float frequency = 3.5f;
    [SerializeField] public float magnitude = 0.5f;

    Vector3 initialPosition;
    bool facingRight;

    // Start is called before the first frame update
    void Start(){ initialPosition = transform.position; }

    // Update is called once per frame
    void Update(){

        CheckWhereToFace();

        if (facingRight){ MoveRight(); }
                   else { MoveLeft(); }
    }

    void CheckWhereToFace(){
             if (initialPosition.x > 4.5f){ facingRight = false; }
        else if (initialPosition.x < -4.5f){ facingRight = true; }
    }

    void MoveRight(){
        initialPosition += transform.right * Time.deltaTime * moveSpeed;
        transform.position = initialPosition + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
    void MoveLeft(){
        initialPosition -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = initialPosition + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

}