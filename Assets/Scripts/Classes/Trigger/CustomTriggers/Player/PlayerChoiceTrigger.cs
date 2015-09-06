using UnityEngine;
using System.Collections;

public class PlayerChoiceTrigger : CustomTrigger {
    protected override void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        // Debug.Log(this.gameObject.name + " entered was triggered.");
        
        // By default the trigger is executed on enter, but in other classes
        // you can override this method in order to determine where you would
        // actually like to execute it
        // FireEnterEvents should occur last in the event logic, but it should
        // occur before the execute logic is run
        Player playerRef = gameObjectEntering.GetComponent<Player>();
        GhostPlayer ghostPlayerRef = gameObjectEntering.GetComponent<GhostPlayer>();
        if(playerRef != null
            || ghostPlayerRef != null) {
            FireEnterEvents();
            Execute(gameObjectEntering);
        }
    }
    
    public override void Stay(GameObject gameObjectStaying) {
        Player playerRef = gameObjectStaying.GetComponent<Player>();
        GhostPlayer ghostPlayerRef = gameObjectStaying.GetComponent<GhostPlayer>();
        if(playerRef != null
            || ghostPlayerRef != null) {
            FireStayEvents();
        }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        Player playerRef = gameObjectExiting.GetComponent<Player>();
        GhostPlayer ghostPlayerRef = gameObjectExiting.GetComponent<GhostPlayer>();
        if(playerRef != null
            || ghostPlayerRef != null) {
            FireExitEvents();
        }
    }
}
