using UnityEngine;
using System.Collections;

public class PanToObject : CustomTrigger {
    public float xMoveSpeed = 1f;
    public float yMoveSpeed = 1f;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    // public override void Entered(GameObject gameObjectEntering) {
    //     base.Entered(gameObjectEntering);
    // }
    // public override void Exited(GameObject gameObjectExiting) {
    //     base.Exited(gameObjectExiting);
    // }
    // public override void Execute(GameObject gameObjectExecuting) {
    //     base.Exited(gameObjectExecuting);
    // }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        Debug.Log("Executing Logic!!!");
        CameraManager.Instance.PanTo(Vector3.zero);
    }
}
