using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///<summary>
/// This is used to set the value of multiple colliders at once. If the colliders
/// have separate values then use multiple versions of this script to modify each
/// collider group as needed.
///<summary>
public class Collider2DValuesEvent : CustomEventObject {
    [Tooltip("These are the colliders to modify.")]
    public List<Collider2D> collidersToModify = null;
    public bool isEnabled = false;
    public bool isTrigger = false;
    
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
        if(collidersToModify == null) {
            collidersToModify = new List<Collider2D>();
        }
        else {
            foreach(Collider2D colliderToModify in collidersToModify) {
                if(colliderToModify == null) {
                    Debug.LogError(this.gameObject.transform.GetFullPath() + " has a NULL value in the 'colliderToModify' on initialization. Please remove this element or fix its reference in order to start correctly.");
                }
            }
        }
    }
    
    public override void ExecuteLogic() {
        SetColliderValues();
    }
    
    public void SetColliderValues() {
        foreach(Collider2D colliderToModify in collidersToModify) {
            colliderToModify.enabled = isEnabled;
            colliderToModify.isTrigger = isTrigger;
        }
    }
}
