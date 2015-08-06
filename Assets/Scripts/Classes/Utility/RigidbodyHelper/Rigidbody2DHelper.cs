using UnityEngine;
using System.Collections;

public class Rigidbody2DHelper : BaseRigidbodyHelper {
    public Rigidbody2D rigidbody2DReference = null;
    
    // // Use this for initialization
    // protected override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    // // LateUpdate is called once per frame
    // protected override void LateUpdate() {
    //     base.LateUpdate();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(rigidbody2DReference == null) {
            Debug.LogError(this.gameObject.name + " needs its 'rigidbody2DReference' reference to be set in the 'Rigidbody2DHelper' Script");
        }
    }
    
    public override void ClampVelocity() {
        Vector3 newVelocity = rigidbody2DReference.velocity;
        if(clampXVelocity) {
            if(newVelocity.x < minVelocity.x) {
                newVelocity.x = minVelocity.x;
            }
            if(newVelocity.x > maxVelocity.x) {
                newVelocity.x = maxVelocity.x;
            }
        }
        
        if(clampYVelocity) {
            if(newVelocity.y < minVelocity.y) {
                newVelocity.y = minVelocity.y;
            }
            if(newVelocity.y > maxVelocity.y) {
                newVelocity.y = maxVelocity.y;
            }
        }
        
        if(clampZVelocity) {
            if(newVelocity.z < minVelocity.z) {
                newVelocity.z = minVelocity.z;
            }
            if(newVelocity.z > maxVelocity.z) {
                newVelocity.z = maxVelocity.z;
            }
        }
        
        rigidbody2DReference.velocity = newVelocity;
        
        // Clamps Velocity Min
        // rigidbody2DReference.velocity = Vector3.ClampMagnitude(rigidbody2DReference.velocity, minVelocity);
        // Clamps Velocity Max
        // rigidbody2DReference.velocity = Vector3.ClampMagnitude(rigidbody2DReference.velocity, maxVelocity);
    }
}