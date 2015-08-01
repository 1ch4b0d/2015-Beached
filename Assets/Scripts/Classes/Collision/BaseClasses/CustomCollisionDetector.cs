using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomCollisionDetector : MonoBehaviour {

    public GameObject gameObjectColliding = null;
    
    public List<CustomCollision> customCollisions  = null;
    
    // Use this for initialization
    public virtual void Awake() {
        // I forget why this is set this way, but default to itself
        if(gameObjectColliding == null) {
            gameObjectColliding = this.gameObject;
        }
    }
    
    // Use this for initialization
    public virtual void Start() {
    }
    
    // Update is called once per frame
    public virtual void Update() {
    }
    
    public virtual void OnCollisionEnter2D(Collision2D enterCollision2D) {
        foreach(CustomCollision customCollision in customCollisions) {
            customCollision.Entered(enterCollision2D.gameObject);
        }
    }
    
    public virtual void OnCollisionStay2D(Collision2D stayCollision2D) {
        foreach(CustomCollision customCollision in customCollisions) {
            customCollision.Stay(stayCollision2D.gameObject);
        }
    }
    
    public virtual void OnCollisionExit2D(Collision2D exitCollision2D) {
        foreach(CustomCollision customCollision in customCollisions) {
            customCollision.Exited(exitCollision2D.gameObject);
        }
    }
}