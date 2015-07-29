using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlubberPoolValuesEvent : CustomEventObject {
    public List<Blubber> blubberToDecomission = null;
    
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
        if(blubberToDecomission == null) {
            blubberToDecomission = new List<Blubber>();
        }
    }
    
    public override void ExecuteLogic() {
        SetBlubberPoolValues();
    }
    
    public void SetBlubberPoolValues() {
        foreach(Blubber blubber in blubberToDecomission) {
            BlubberPool.Instance.Decomission(blubber.gameObject);
        }
    }
}
