using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverThorntonChosenEvent : CustomEventObject {
    public GameObject ghostPlayerGameObject = null;
    public GameObject georgeThorntonGameObject = null;
    public GameObject spotlightGameObject = null;
    
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
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        ExecuteThorntonChosenActions();
        FireExecuteEvents();
    }
    
    public void ExecuteThorntonChosenActions() {
        // Stop the player so they're standing there without control
        // Have the ghost avatar animate such that it plays the head sigh and shake animation
        // At the same time have thornton celebrate that the player chose him
        // OnFinish of the head shake animation have thornton run off screen
        // Have the ghost slowly move off screen to a location
        Debug.Log("ThortonChosen");
        FireFinishEvents();
    }
}
