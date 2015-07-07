using UnityEngine;
using System.Collections;
using Acrocatic;

public class PlayerFaceUp : MonoBehaviour {

    private GameObject playerGameObject;
    private Acrocatic.Player player;
    
    // Use this for initialization
    void Start() {
        // Grabs from the same object this script is attached to if isn't
        // declared ahead of time
        if(playerGameObject == null) {
            playerGameObject = this.gameObject;
        }
        player = playerGameObject.GetComponent<Acrocatic.Player>();
    }
    
    
    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.W) && player.hor == 0) {
			player.FacingUp(true);
			player.playerRun.enabled = false;
			player.playerJump.enabled = false;
            //player.FacingUp(true);
        }
		else if(Input.GetKeyUp(KeyCode.W)) {
            player.FacingUp(false);
			player.playerRun.enabled = true;
			player.playerJump.enabled = true;
		}
    }
}