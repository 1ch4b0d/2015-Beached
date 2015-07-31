using UnityEngine;
using System.Collections;

public class BlubberCollision : CustomCollision {
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
    
    // protected override void Initialize() {
    //     base.Initialize();
    // }
    
    //--------------------------------------------------------------------------
    public override void EnterLogic(GameObject gameObjectEntering) {
        // Debug.Log("Blubber was entered by something else");
        Player playerReference = gameObjectEntering.GetComponent<Player>();
        if(playerReference != null) {
            Debug.Log("Blubber was entered by player!");
        }
    }
    //--------------------------------------------------------------------------
    public override void StayLogic(GameObject gameObjectStaying) {
    }
    //--------------------------------------------------------------------------
    public override void ExitLogic(GameObject gameObjectExiting) {
    }
    //--------------------------------------------------------------------------
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
    }
}
