using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: REMOVE NGUI TRIGGER FUNCTIONALITY OR GIVE THIS A
//      NAMESPACE AND RENAME IT TO "TRIGGER", BECAUSE THE
//      NAME CUSTOM TRIGGER IS DUMB
public class CustomCollision : MonoBehaviour {
    // Executes these as standard unity hooks
    public bool loopEnter = true;
    public int enterIteration = 0;
    public List<CustomEventsManager> onEnter = null;
    
    public bool loopStay = true;
    public int stayIteration = 0;
    public List<CustomEventsManager> onStay = null;
    
    public bool loopExit = true;
    public int exitIteration = 0;
    public List<CustomEventsManager> onExit = null;
    
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
    }
    
    protected virtual void Initialize() {
    }
    //--------------------------------------------------------------------------
    public virtual void Entered(GameObject gameObjectEntering) {
        // By default the trigger is executed on enter, but in other classes
        // you can override this method in order to determine where you would
        // actually like to execute it
        // FireEnterEvents should occur last in the event logic, but it should
        // occur before the execute logic is run
        
        // Perform only if it's the first iteration, or it should loop
        if(enterIteration < 1
            || loopEnter) {
            EnterLogic(gameObjectEntering);
            FireEnterEvents();
            enterIteration++;
        }
    }
    
    public virtual void EnterLogic(GameObject gameObjectEntering) {
    }
    //--------------------------------------------------------------------------
    public virtual void Stay(GameObject gameObjectStaying) {
        if(stayIteration < 1
            || loopStay) {
            StayLogic(gameObjectStaying);
            FireStayEvents();
            stayIteration++;
        }
    }
    
    public virtual void StayLogic(GameObject gameObjectStaying) {
    }
    //--------------------------------------------------------------------------
    public virtual void Exited(GameObject gameObjectExiting) {
        if(exitIteration < 1
            || loopExit) {
            ExitLogic(gameObjectExiting);
            FireExitEvents();
            exitIteration++;
        }
    }
    
    public virtual void ExitLogic(GameObject gameObjectExiting) {
    }
    //--------------------------------------------------------------------------
    public virtual void FireEnterEvents() {
        ExecuteEvents(onEnter);
    }
    
    public virtual void FireStayEvents() {
        ExecuteEvents(onStay);
    }
    
    public virtual void FireExitEvents() {
        ExecuteEvents(onExit);
    }
    
    public virtual void ExecuteEvents(List<CustomEventsManager> customEventsManagers) {
        if(customEventsManagers != null) {
            foreach(CustomEventsManager customEventsManager in customEventsManagers) {
                customEventsManager.Execute();
            }
        }
    }
    //--------------------------------------------------------------------------
    // This is deprecated. Do not disable collider.
    public virtual void DisableCollider() {
        Collider2D attachedCollider = this.GetComponent<Collider2D>();
        if(attachedCollider != null) {
            attachedCollider.enabled = false;
        }
    }
}