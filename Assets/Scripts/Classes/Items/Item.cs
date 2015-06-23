using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
    public List<Collider2D> colliders = null;
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public void DisableColliders() {
        ToggleColliders(false);
    }
    
    public void EnableColliders() {
        ToggleColliders(true);
    }
    
    public void ToggleColliders(bool enabled) {
        foreach(Collider2D collider in colliders) {
            collider.enabled = enabled;
        }
    }
}
