using UnityEngine;
using System.Collections;

public class DestroyObjectEvent : CustomEventObject {
    public GameObject objectToDestroy = null;
    
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
        if(objectToDestroy == null) {
            this.gameObject.LogComponentError("objectToDestroy", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        DestroyObject();
    }
    
    public void DestroyObject() {
        Destroy(objectToDestroy);
    }
}
