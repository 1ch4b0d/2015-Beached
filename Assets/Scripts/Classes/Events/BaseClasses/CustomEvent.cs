using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class CustomEvent {
    public bool loop = false;
    
    public System.Action customDelegate;
    
    public static CustomEvent Create() {
        CustomEvent newDelegateEvent = new CustomEvent();
        return newDelegateEvent;
    }
    
    public CustomEvent SetLoop(bool newLoopState) {
        loop = newLoopState;
        return this;
    }
    public bool ShouldLoop() {
        return loop;
    }
    
    public CustomEvent SetEvent(System.Action newDelegateEvent) {
        customDelegate = newDelegateEvent;
        return this;
    }
    
    public System.Action GetEvent() {
        return customDelegate;
    }
    
    // Executes without any parameters
    public void Execute() {
        if(customDelegate != null) {
            customDelegate();
        }
    }
}