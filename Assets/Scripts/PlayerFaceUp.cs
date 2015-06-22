using UnityEngine;
using System.Collections;
using Acrocatic;

public class PlayerFaceUp : MonoBehaviour {

    private GameObject playerGameObject;
    private Player playerReference;
    private Animator playerAnimation;
    
    public AudioClip shootSound;
    
    // Use this for initialization
    void Start() {
        // Setting up references.
        if(playerGameObject == null) {
            playerGameObject = GameObject.Find("Player");
        }
        playerReference = playerGameObject.GetComponent<Player>();
        playerAnimation = playerGameObject.GetComponent<Animator>();
    }
    
    
    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.W)) {
            playerReference.facingUp = true;
            playerAnimation.SetBool("facingRight", true);
        }
        else if(Input.GetKeyUp(KeyCode.W)) {
            playerReference.facingUp = false;
        }
    }
}
