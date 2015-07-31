using UnityEngine;
using System.Collections;

public abstract class BaseSnapshot<T> {
    public abstract void Capture(T newGameObjectToPause);
    
    public abstract void Restore(T newGameObjectToPause);
}
