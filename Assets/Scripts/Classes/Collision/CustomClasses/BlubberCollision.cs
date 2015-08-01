using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlubberCollision : CustomCollision {
    public bool loopPlayerEnter = true;
    public int playerEnterIteration = 0;
    public List<CustomEventsManager> onPlayerEnter = null;
    
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
    public virtual void Entered(GameObject gameObjectEntering) {
        if(enterIteration < 1
            || loopEnter) {
            // filters blubber from responding
            if(gameObjectEntering.GetComponent<Blubber>() == null) {
                EnterLogic(gameObjectEntering);
                FireEnterEvents();
                enterIteration++;
            }
        }
    }
    public override void EnterLogic(GameObject gameObjectEntering) {
        Player playerReference = gameObjectEntering.GetComponent<Player>();
        if(playerReference != null) {
            FireOnPlayerEnterEvents();
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
    public virtual void FireOnPlayerEnterEvents() {
        ExecuteEvents(onPlayerEnter);
    }
}
