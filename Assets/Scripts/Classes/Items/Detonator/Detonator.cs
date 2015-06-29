using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detonator : Item {
    public List<DetonatorPrimer> detonatorPrimers = null;
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public override void Initialize() {
        base.Initialize();
    }
}
