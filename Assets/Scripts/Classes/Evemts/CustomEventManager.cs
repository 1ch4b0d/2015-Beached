using UnityEngine;
using System.Collections;

/// <summary>
/// The CustomEventManager is used as the primary interface for connecting to
/// all the custom events that may want to be configured ahead of time statically
/// What this entails is creating a top level GameObject that is named for the
/// EventsTrying to be captured, I.E. OnCameraPanStart, OnCameraPanExecuting,
/// OnCameraPanFinish.
///
/// The CustomEventManager needs a child object under it that serves as the
/// parent for each of the events that needto fire when it finishes.
///
/// Each of these event parent objects are assumed to contain all the scripts
/// that will have the custom scripts attached to it. This means that all of the
/// event scripts that need to execute are attached to one GameObject, and each
/// script is retrieved by getting a list of all of the base class type.
/// </summary>
public class CustomEventManager : MonoBehaviour {
    public GameObject onStartEventsGameObject = null;
    public GameObject onExecuteEventsGameObject = null;
    public GameObject onFinishEventsGameObject = null;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public void Initialize() {
        if(onStartEventsGameObject == null) {
        }
    }
    
    public static GameObject CreateEventsGameObject(string gameObjectName) {
        return null;
    }
}
