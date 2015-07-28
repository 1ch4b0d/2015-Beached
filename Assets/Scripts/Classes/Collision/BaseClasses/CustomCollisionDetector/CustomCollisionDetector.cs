using UnityEngine;
using System.Collections;

public class CustomCollisionDetector : MonoBehaviour {

    public GameObject gameObjectColliding = null;
    
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
    
    public virtual void OnCollisionEnter2D(Collision2D collision2D) {
        // Debug.Log("Detector just triggered on " + collision2D.gameObject.name);
        CustomCollision customCollision = collision2D.collider.gameObject.GetComponent<CustomCollision>();
        if(customCollision != null) {
            customCollision.Entered(gameObjectColliding);
        }
    }
    
    public virtual void OnCollisionStay2D(Collision2D collision2D) {
        CustomCollision customCollision = collision2D.collider.gameObject.GetComponent<CustomCollision>();
        if(customCollision != null) {
            customCollision.Stay(gameObjectColliding);
        }
    }
    
    public virtual void OnCollisionExit2D(Collision2D collision2D) {
        CustomCollision customCollision = collision2D.collider.gameObject.GetComponent<CustomCollision>();
        if(customCollision != null) {
            customCollision.Exited(gameObjectColliding);
        }
    }
}