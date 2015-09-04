using UnityEngine;
using System.Collections;

public class DestroyAllDynamiteEvent : CustomEventObject {
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
    }
    
    public override void ExecuteLogic() {
        DestroyObjects();
    }
    
    public void DestroyObjects() {
        Dynamite[] dynamites = FindObjectsOfType(typeof(Dynamite)) as Dynamite[];
        foreach(Dynamite dynamite in dynamites) {
            Destroy(dynamite.gameObject);
        }
    }
}
