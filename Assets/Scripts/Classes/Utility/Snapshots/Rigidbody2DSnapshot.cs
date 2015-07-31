using UnityEngine;
using System.Collections;

public class Rigidbody2DSnapshot : BaseSnapshot<Rigidbody2D> {
    Vector2 velocity = Vector2.zero;
    float angularVelocity = 0f;
    bool isKinematic = false;
    
    public override void Capture(Rigidbody2D gameObjectToCapture) {
        Rigidbody2D rigidbody2D = gameObjectToCapture.GetComponent<Rigidbody2D>();
        velocity = rigidbody2D.velocity;
        angularVelocity = rigidbody2D.angularVelocity;
        isKinematic = rigidbody2D.isKinematic;
    }
    
    public override void Restore(Rigidbody2D gameObjectToRestore) {
        Rigidbody2D rigidbody2D = gameObjectToRestore.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = velocity;
        rigidbody2D.angularVelocity = angularVelocity;
        rigidbody2D.isKinematic = isKinematic;
    }
}