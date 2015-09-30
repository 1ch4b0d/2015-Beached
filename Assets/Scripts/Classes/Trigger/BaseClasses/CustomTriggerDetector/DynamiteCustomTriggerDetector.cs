using UnityEngine;
using System.Collections;

public class DynamiteCustomTriggerDetector : ItemCustomTriggerDetector {
    // Use this for initialization
    public override void Start() {
        base.Start();
    }
    
    // Update is called once per frame
    public override void Update() {
        base.Update();
    }
    
    public override void OnTriggerEnter2D(Collider2D collider) {
        CustomTrigger customTrigger = collider.gameObject.GetComponent<CustomTrigger>();
        // Detonator detonator = collider.gameObject.GetComponent<Detonator>();
        
        //----------------------------
        // WARNING WARNING WARNING!!! - Performance concern
        //----------------------------
        // Using the Utility.GetFirstParent<Detonator>() method he is
        // very likely a performance issue if this project were to scale up.
        //
        // The correct solution would provide a reference to the object that
        // is colliding with this gameobject instead of a reference to the
        // sub gameobject that is a child of the actual game object interacting
        // (the collider is a sub game object of the main game object)
        //----------------------------
        Detonator detonator = collider.gameObject.GetFirstParent<Detonator>();
        if(customTrigger != null) {
            if(detonator == null) {
                customTrigger.Entered(this.gameObject);
            }
        }
    }
    
    // public override void OnTrigger2D(Collider2D collider) {
    // }
    
    public override void OnTriggerExit2D(Collider2D collider) {
        CustomTrigger customTrigger = collider.gameObject.GetComponent<CustomTrigger>();
        // Detonator detonator = collider.gameObject.GetComponent<Detonator>();
        Detonator detonator = collider.gameObject.GetFirstParent<Detonator>();
        if(customTrigger != null) {
            if(detonator == null) {
                customTrigger.Exited(this.gameObject);
            }
        }
    }
}