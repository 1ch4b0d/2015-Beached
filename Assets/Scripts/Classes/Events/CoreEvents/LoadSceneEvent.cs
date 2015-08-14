using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadSceneEvent : CustomEventObject {
    public string sceneToLoad = "";
    
    // Use this for initialization
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
        if(sceneToLoad.Equals("")) {
            Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'sceneToLoad' reference to be set to a value other that \"\" in the 'LoadSceneEvent' Script");
        }
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        LoadScene();
        FireExecuteEvents();
    }
    
    public void LoadScene() {
        Application.LoadLevel(sceneToLoad);
    }
}