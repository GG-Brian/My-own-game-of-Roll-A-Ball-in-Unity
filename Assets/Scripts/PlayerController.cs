using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed = 0;
    public float jumpAmount = 1;
    public float totalCollectibles;
    public Vector3 checkpoint;
    public Vector3 initialPosition;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject groundLevel;
    public GameObject wallsLevel;

    public GameObject levelSelectMenu;
    bool levelSelectIsActive = false;

    private Rigidbody rb;
    private int collectibles;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start(){
        initialPosition = transform.position;
        checkpoint = initialPosition;
        rb = GetComponent<Rigidbody>();
        collectibles = 0;

        SetCollectiblesTriggerChanges();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(){
        if(Physics.Raycast(transform.position, Vector2.down, 1f)){
            rb.AddForce(Vector3.up * (jumpAmount * (collectibles + 1)), ForceMode.Impulse);
        }
    }

    void OnEsc(){
        if (levelSelectIsActive){
            Time.timeScale = 1;
            levelSelectMenu.SetActive(false);
            levelSelectIsActive = false;}
        else {
            Time.timeScale = 0;
            levelSelectMenu.SetActive(true);
            levelSelectIsActive = true;}
    }

    void FixedUpdate(){
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            
            collectibles += 1;
            SetCollectiblesTriggerChanges();

            Vector3 wallsLevelScale = wallsLevel.transform.localScale;
            wallsLevelScale.y += .13f;
            wallsLevel.transform.localScale = wallsLevelScale;

        } else if (other.gameObject.CompareTag("Respawn")){
            other.gameObject.SetActive(false);
            collectibles += 1;
            checkpoint = transform.position;
            SetCollectiblesTriggerChanges();
        
        } else if (other.gameObject.CompareTag("Death")){
            if (checkpoint.y != initialPosition.y){ transform.position = checkpoint; }
            else { SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); }
        }
    }

    void SetCollectiblesTriggerChanges(){
        countText.text = "Estrellitas: " + collectibles.ToString() + "/" + totalCollectibles.ToString();
        if (collectibles > (totalCollectibles - 1)){
            groundLevel.SetActive(false);
        } else {
            Color lessOpacity = groundLevel.GetComponent<MeshRenderer>().material.color;
            lessOpacity.a -= 0.045f;
            groundLevel.GetComponent<MeshRenderer>().material.color = lessOpacity;
        }
        if (collectibles > totalCollectibles){
            winTextObject.SetActive(true);
            countText.enabled = false;

            rb.useGravity = false;
            rb.velocity = new Vector3(0,0,0);
            rb.angularVelocity = new Vector3(0,0,0);                
            
            StartCoroutine(waitBeforeNextLevel());
        }

        IEnumerator waitBeforeNextLevel(){
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}