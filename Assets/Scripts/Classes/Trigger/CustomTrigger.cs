using UnityEngine;
using System.Collections;

//TODO: REMOVE NGUI TRIGGER FUNCTIONALITY OR GIVE THIS A
//      NAMESPACE AND RENAME IT TO "TRIGGER", BECAUSE THE
//      NAME CUSTOM TRIGGER IS DUMB
public class CustomTrigger : MonoBehaviour {
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
    
    public virtual void Execute(GameObject gameObjectInteracting) {
    }
}
