using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomEvents {
    public List<CustomEvent> allDelegateEvents;
    
    public CustomEvents() {
        allDelegateEvents = new List<CustomEvent>();
    }
    
    public static CustomEvents Create() {
        CustomEvents newCustomEvents = new CustomEvents();
        return newCustomEvents;
    }
    
    public void Add(System.Action newOnDelegateEvent, bool loop = false) {
        CustomEvent newEvent = CustomEvent.Create()
                               .SetEvent(newOnDelegateEvent)
                               .SetLoop(loop);
        Add(newEvent);
    }
    
    public void Add(CustomEvent newDelegateEvent) {
        allDelegateEvents.Add(newDelegateEvent);
    }
    
    public int Count() {
        return allDelegateEvents.Count;
    }
    
    public void Execute() {
        List<CustomEvent> loopingDelegates = new List<CustomEvent>();
        foreach(CustomEvent delegateEvent in allDelegateEvents) {
            delegateEvent.Execute();
            if(delegateEvent.ShouldLoop()) {
                loopingDelegates.Add(delegateEvent);
            }
        }
        allDelegateEvents = loopingDelegates;
    }
}
