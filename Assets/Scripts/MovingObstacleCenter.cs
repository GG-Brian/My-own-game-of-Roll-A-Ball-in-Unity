using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleCenter : MonoBehaviour {

    private Vector3 initialPosition;

    [SerializeField]
    private float frequency = 3.50f;

    [SerializeField]
    private float magnitude = 4.5f;

    [SerializeField]
    private float offset = 0f;

    // Start is called before the first frame update
    void Start(){
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update(){
        transform.position = initialPosition + transform.right * Mathf.Sin(Time.time * frequency + offset) * magnitude;
    }
}
