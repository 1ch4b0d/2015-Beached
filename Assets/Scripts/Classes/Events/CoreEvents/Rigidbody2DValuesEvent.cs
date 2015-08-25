using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///<summary>
/// This is used to set the value of multiple colliders at once. If the colliders
/// have separate values then use multiple versions of this script to modify each
/// collider group as needed.
///<summary>
public class Rigidbody2DValuesEvent : CustomEventObject {
    public List<Rigidbody2D> rigidbodiesToModify = null;
    public float gravityScale = 1f;
    public bool isKinematic = false;
    
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
    
    protected override void Initialize() {
        base.Initialize();
        if(rigidbodiesToModify == null) {
            // rigidbodiesToModify = new List<Rigidbody2D>();
            this.gameObject.LogComponentError("rigidbodiesToModify", this.GetType());
        }
        else {
            foreach(Rigidbody2D rigidbodyToModify in rigidbodiesToModify) {
                if(rigidbodyToModify == null) {
                    Debug.LogError(this.gameObject.transform.GetFullPath() + " has a NULL value in the 'rigidbodiesToModify' on initialization. Please remove this element or fix its reference in order to start correctly.");
                }
            }
        }
    }
    
    public override void ExecuteLogic() {
        SetRigidbody2DValues();
    }
    
    public void SetRigidbody2DValues() {
        foreach(Rigidbody2D rigidbody2d in rigidbodiesToModify) {
            rigidbody2d.gravityScale = gravityScale;
            rigidbody2d.isKinematic = isKinematic;
        }
    }
}