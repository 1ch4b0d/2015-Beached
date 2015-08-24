using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionPoolValuesEvent : CustomEventObject {
    public List<GameObject> objectsToDecomission = null;
    
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
        if(objectsToDecomission == null) {
            objectsToDecomission = new List<GameObject>();
        }
    }
    
    public override void ExecuteLogic() {
        SetGameObjectPoolValues();
    }
    
    public void SetGameObjectPoolValues() {
        foreach(GameObject gameObject in objectsToDecomission) {
            ExplosionPool.Instance.Decomission(gameObject);
        }
    }
}
