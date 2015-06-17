using UnityEngine;
using System.Collections;

public class Rigidbody2DSnapshot {
    Vector2 savedVelocity = Vector2.zero;
    float savedAngularVelocity = 0f;
    
    public void Pause(GameObject newGameObjectToPause) {
        savedVelocity = newGameObjectToPause.GetComponent<Rigidbody2D>().velocity;
        savedAngularVelocity = newGameObjectToPause.GetComponent<Rigidbody2D>().angularVelocity;
        newGameObjectToPause.GetComponent<Rigidbody2D>().isKinematic = true;
    }
    
    
    public void Unpause(GameObject newGameObjectToPause, bool enableIsKinematic = false) {
        newGameObjectToPause.GetComponent<Rigidbody2D>().velocity = savedVelocity;
        newGameObjectToPause.GetComponent<Rigidbody2D>().angularVelocity = savedAngularVelocity;
        newGameObjectToPause.GetComponent<Rigidbody2D>().isKinematic = enableIsKinematic;
    }
}