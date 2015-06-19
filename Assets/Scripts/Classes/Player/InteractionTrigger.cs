using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionTrigger : MonoBehaviour {
    void Awake() {
    }
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public virtual void Entered(GameObject gameObjectEntering) {
    }
    
    public virtual void Exited(GameObject gameObjectExiting) {
    }
    
    public virtual void Interact(GameObject gameObjectInteracting) {
    }
}
