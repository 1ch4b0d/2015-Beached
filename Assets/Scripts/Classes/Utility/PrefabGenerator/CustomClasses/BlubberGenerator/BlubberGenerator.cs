using UnityEngine;
using System.Collections;

public class BlubberGenerator : PrefabGenerator {
    // // Use this for initialization
    // protected override void Awake() {
    //     Initialize();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    // protected override void Initialize() {
    //     base.Awake();
    // }
    
    // protected override void PerformLogic() {
    //     base.PerformLogic();
    // }
    
    public override GameObject GenerateGameObject() {
        return BlubberPool.Instance.Issue();
    }
}
