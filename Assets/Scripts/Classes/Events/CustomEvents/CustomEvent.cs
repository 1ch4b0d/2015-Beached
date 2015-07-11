using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class CustomEvent<T> {
    public bool loop = false;
    
    public T customDelegate;
    
    public static CustomEvent<T> Create() {
        CustomEvent<T> newDelegateEvent = new CustomEvent<T>();
        return newDelegateEvent;
    }
    
    public CustomEvent<T> SetLoop(bool newLoopState) {
        loop = newLoopState;
        return this;
    }
    public bool ShouldLoop() {
        return loop;
    }
    
    public CustomEvent<T> SetEvent(T newDelegateEvent) {
        customDelegate = newDelegateEvent;
        return this;
    }
    
    public T GetEvent() {
        return customDelegate;
    }
    
    // Executes without any parameters
    public void Execute() {
        MethodInfo method = typeof(CustomEvent<T>).GetMethod("Execute");
        method.Invoke(this, null);
        // MethodInfo generic = method.MakeGenericMethod(T);
        // customDelegate();
    }
}