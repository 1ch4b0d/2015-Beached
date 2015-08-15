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
    
    public void AddEvent(System.Action newOnDelegateEvent, bool loop = false) {
        CustomEvent newEvent = CustomEvent.Create()
                               .SetEvent(newOnDelegateEvent)
                               .SetLoop(loop);
        AddEvent(newEvent);
    }
    
    public void AddEvent(CustomEvent newDelegateEvent) {
        allDelegateEvents.Add(newDelegateEvent);
    }
    
    public List<CustomEvent> GetEvents() {
        return allDelegateEvents;
    }
    
    public bool Contains(System.Action delegateEvent) {
        bool foundDelegate = false;
        foreach(CustomEvent customEvent in allDelegateEvents) {
            if(customEvent.GetEvent() == delegateEvent) {
                foundDelegate = true;
                // because why waste more time in the loop
                break;
            }
        }
        return foundDelegate;
    }
    
    public bool Contains(CustomEvent customEvent) {
        return allDelegateEvents.Contains(customEvent);
    }
    
    public int Count() {
        return allDelegateEvents.Count;
    }
    
    public void Execute() {
        // This creates a copy of all the delegates to perform,
        // then it creates a new list of delegates that will get repeated
        // and it uses that new list to set it to the list of this object
        // then it executes the events that needed to perform.
        // This is done in order to prevent stack overflows from occurring
        // when the event some how is recursively triggered. This way only
        // the events to repeat occur, and won't be called on subsequent
        // recursive calls.
        CustomEvent[] currentDelegates = allDelegateEvents.ToArray();
        List<CustomEvent> loopingDelegates = new List<CustomEvent>();
        foreach(CustomEvent delegateEvent in allDelegateEvents) {
            if(delegateEvent.ShouldLoop()) {
                loopingDelegates.Add(delegateEvent);
            }
        }
        allDelegateEvents = loopingDelegates;
        
        foreach(CustomEvent delegateEvent in currentDelegates) {
            delegateEvent.Execute();
        }
    }
}
