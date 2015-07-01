using UnityEngine;
using System.Collections;

public class CustomTriggerDetector : MonoBehaviour {

    // Use this for initialization
    public virtual void Start() {
    }
    
    // Update is called once per frame
    public virtual void Update() {
    }
    
    public virtual void OnTriggerEnter2D(Collider2D collider) {
        CustomTrigger customTrigger = collider.gameObject.GetComponent<CustomTrigger>();
        if(customTrigger != null) {
            customTrigger.Entered(this.gameObject);
        }
    }
    
    // public virtual void OnTrigger2D(Collider2D collider) {
    // }
    
    public virtual void OnTriggerExit2D(Collider2D collider) {
        CustomTrigger customTrigger = collider.gameObject.GetComponent<CustomTrigger>();
        if(customTrigger != null) {
            customTrigger.Exited(this.gameObject);
        }
    }
}
