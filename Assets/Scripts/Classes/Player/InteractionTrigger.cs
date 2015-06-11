using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionTrigger : MonoBehaviour {
    public GameObject rootGameObject = null;
    
    void Awake() {
    }
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public virtual void Entered() {
    }
    
    public virtual void Exited() {
    }
    
    public virtual void Interact() {
    }
}
