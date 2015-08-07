using UnityEngine;
using System.Collections;

public class GameObjectValuesEvent : CustomEventObject {
    public GameObject gameObjectToModify = null;
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
        if(gameObjectToModify == null) {
            Debug.LogError(this.gameObject.name + " needs its 'gameObjectToModify' reference to be set in the 'TransformValuesEvent' Script");
        }
    }
    
    public override void Execute() {
        base.Execute();
    }
    
    public override void ExecuteLogic() {
        SetGameObjectValues();
    }
    
    public void SetGameObjectValues() {
        gameObjectToModify.SetActive(isEnabled);
    }
}
