using UnityEngine;
using System.Collections;

public class BasicPlayerController : MonoBehaviour {
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        PerformInputLogic();
    }
    
    // TODO: Extract movement logic to its own caluclation and instead use this
    //       method to for detecting player input
    protected void PerformInputLogic() {
        Vector3 movementOffset = Vector3.zero;
        float movementSpeed = 5f;
        
        // Up
        if(Input.GetKey(KeyCode.W)) {
            movementOffset.y += movementSpeed;
        }
        // Right
        if(Input.GetKey(KeyCode.D)) {
            movementOffset.x += movementSpeed;
        }
        // Down
        if(Input.GetKey(KeyCode.S)) {
            movementOffset.y -= movementSpeed;
        }
        // Left
        if(Input.GetKey(KeyCode.A)) {
            movementOffset.x -= movementSpeed;
        }
        
        movementOffset.z = 0f;
        movementOffset = movementOffset * Time.deltaTime;
        
        this.transform.position += movementOffset;
    }
    
    protected void UpdateAnimator() {
    }
}
