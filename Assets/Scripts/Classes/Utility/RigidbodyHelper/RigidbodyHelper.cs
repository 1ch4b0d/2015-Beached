using UnityEngine;
using System.Collections;

public class RigidbodyHelper : BaseRigidbodyHelper {
    public Rigidbody rigidbodyReference = null;
    
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
        if(rigidbodyReference == null) {
            Debug.LogError(this.gameObject.name + " needs its 'rigidbodyReference' reference to be set in the 'RigidbodyHelper' Script");
        }
    }
    
    public override void ClampVelocity() {
        // Clamps Velocity Min
        // rigidbodyReference.velocity = Vector3.ClampMagnitude(rigidbodyReference.velocity, minVelocity);
        // Clamps Velocity Max
        // rigidbodyReference.velocity = Vector3.ClampMagnitude(rigidbodyReference.velocity, maxVelocity);
    }
}
