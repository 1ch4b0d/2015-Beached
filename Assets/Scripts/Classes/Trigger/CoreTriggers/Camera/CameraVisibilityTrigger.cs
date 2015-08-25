using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This is a base trigger class that is intended to be used for detecting when something
// has entered the camera's view. This script REQUIRES that some type of renderer
// is attached to the same game object with this script. These events are only
// triggered on objects that have renderers.
public class CameraVisibilityTrigger : MonoBehaviour {
    public bool loopOnBecameVisible = false;
    public int onBecameVisibleIterations = 0;
    public List<CustomEventsManager> onBecameInvisibleEvents = null;
    public bool loopOnBecameInvisible = false;
    public int onBecameInvisibleIterations = 0;
    public List<CustomEventsManager> onBecameVisibleEvents = null;
    
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
    
    // OnBecameInvisible is called when the object is no longer visible by any camera.
    public virtual void OnBecameInvisible() {
        // Debug.Log("On Became Invisible Occurred");
        FireOnBecameInvisibleEvents();
    }
    
    // OnBecameVisible is called when the object became visible by any camera.
    public virtual void OnBecameVisible() {
        // Debug.Log("On Became Visible Occurred");
        FireOnBecameVisibleEvents();
    }
    
    public virtual void FireOnBecameInvisibleEvents() {
        if(onBecameInvisibleEvents != null) {
            if(onBecameInvisibleIterations < 1
                || loopOnBecameInvisible) {
                foreach(CustomEventsManager customEventsManager in onBecameInvisibleEvents) {
                    customEventsManager.Execute();
                }
            }
            onBecameInvisibleIterations++;
        }
    }
    
    public virtual void FireOnBecameVisibleEvents() {
        if(onBecameVisibleEvents != null) {
            // Debug.Log("1");
            if(onBecameVisibleIterations < 1
                || loopOnBecameVisible) {
                // Debug.Log("2");
                foreach(CustomEventsManager customEventsManager in onBecameVisibleEvents) {
                    // Debug.Log("3");
                    customEventsManager.Execute();
                }
                onBecameVisibleIterations++;
            }
        }
    }
}