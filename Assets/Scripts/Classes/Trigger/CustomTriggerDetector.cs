using UnityEngine;
using System.Collections;

public class CustomTriggerDetector : MonoBehaviour {

    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public void OnTriggerEnter2D(Collider2D collider) {
        CustomTrigger customTrigger = collider.gameObject.GetComponent<CustomTrigger>();
        if(customTrigger != null) {
            customTrigger.Entered(this.gameObject);
        }
    }
    
    // public void OnTrigger2D(Collider2D collider) {
    // }
    
    public void OnTriggerExit2D(Collider2D collider) {
        CustomTrigger customTrigger = collider.gameObject.GetComponent<CustomTrigger>();
        if(customTrigger != null) {
            customTrigger.Exited(this.gameObject);
        }
    }
}
