using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectValuesEvent : CustomEventObject {
    public List<GameObject> gameObjectsToModify = null;
    public bool isEnabled = true;
    
    protected override void Awake() {
        base.Awake();
    }
    
    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
    
    protected override void Initialize() {
        base.Initialize();
        if(gameObjectsToModify == null) {
            this.gameObject.LogComponentError("gameObjectsToModify", this.GetType());
        }
    }
    
    public override void Execute() {
        base.Execute();
    }
    
    public override void ExecuteLogic() {
        SetGameObjectValues();
    }
    
    public void SetGameObjectValues() {
        foreach(GameObject gameObj in gameObjectsToModify) {
            gameObj.SetActive(isEnabled);
        }
    }
}
