using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Shit... I don't think that needs to be a dictionary if you're relying on it to detect objects arbitrarily on the layer
// However if you're concerned about specifying layers it should be kept...
public class ColliderManager : MonoBehaviour {
    public LayerMask layersToCheck;
    public Dictionary<int, List<GameObject>> gameObjectsCollidingWith = null;
    
    // Use this for initialization
    protected void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected void Start() {
    }
    
    // Update is called once per frame
    protected void Update() {
    }
    
    protected void LateUpdate() {
    }
    
    protected void Initialize() {
        if(layersToCheck == 0) {
            Debug.LogError("You've selected no layers to be checked for a collision. Is this intended? If so this component is being redundantly assigned.");
        }
        
        if(gameObjectsCollidingWith == null) {
            gameObjectsCollidingWith = new Dictionary<int, List<GameObject>>();
        }
        
        // Debug.Log("Layers To Check: " + layersToCheck.value);
    }
    
    private Dictionary<int, List<GameObject>> GetGameObjectsCollidingWith() {
        return gameObjectsCollidingWith;
    }
    
    public void Add(LayerMask objectLayer, GameObject gameObjectToAdd) {
        Add(objectLayer, gameObjectToAdd, gameObjectsCollidingWith);
    }
    
    private void Add(LayerMask objectLayer, GameObject gameObjectToAdd, Dictionary<int, List<GameObject>> dictionaryToAddTo) {
        // Determines if the layer of the object to add is part of the layers to
        // check for
        int objectLayerMask = (1 << objectLayer.value);
        if((objectLayerMask & layersToCheck.value) == objectLayerMask) {
            if(!dictionaryToAddTo.ContainsKey(objectLayer)
                || dictionaryToAddTo[objectLayer] == null) {
                dictionaryToAddTo[objectLayer] = new List<GameObject>();
            }
            
            if(!IsObjectInDictionary(objectLayer, gameObjectToAdd, dictionaryToAddTo)) {
                // Debug.Log("Adding object: " + gameObjectToAdd.name);
                dictionaryToAddTo[objectLayer].Add(gameObjectToAdd);
            }
        }
    }
    
    public void Remove(int objectLayer, GameObject gameObjectToRemove) {
        Remove(objectLayer, gameObjectToRemove, gameObjectsCollidingWith);
    }
    
    private void Remove(int objectLayer, GameObject gameObjectToRemove, Dictionary<int, List<GameObject>> dictionaryToRemoveFrom) {
        // if layer isn't in dictionary, can't remove what's not there, so just skip and ignore remove operation
        if(dictionaryToRemoveFrom.ContainsKey(objectLayer)) {
            // Debug.Log("Remove object: " + gameObjectToRemove.name);
            gameObjectsCollidingWith[objectLayer].Remove(gameObjectToRemove);
        }
    }
    
    // This name sucks.... Fix it... Or don't....
    private bool IsObjectInDictionary(LayerMask objectLayer, GameObject gameObjectToSearchFor, Dictionary<int, List<GameObject>> dictionaryToSearch) {
        List<GameObject> currentLayerGameObjects = dictionaryToSearch[objectLayer];
        bool foundGameObject = false;
        foreach(GameObject gameObject in currentLayerGameObjects) {
            if(gameObject.GetInstanceID() == gameObjectToSearchFor.GetInstanceID()) {
                foundGameObject = true;
                break; // exit early for 'performance'
            }
        }
        
        return foundGameObject;
    }
    
    public int CollidingObjectsSize(Dictionary<int, List<GameObject>> dictionaryToSearch) {
        int totalCount = 0;
        foreach(KeyValuePair<int, List<GameObject>> keyValuePair in dictionaryToSearch) {
            List<GameObject> currentList = dictionaryToSearch[keyValuePair.Key];
            if(currentList != null) {
                // done this way because of access level
                totalCount += currentList.Count;
            }
        }
        return totalCount;
    }
    
    public int CollidingObjectsSize(int layer, Dictionary<int, List<GameObject>> dictionaryToSearch) {
        return dictionaryToSearch[layer].Count;
    }
    
    public bool IsColliding() {
        return IsColliding(gameObjectsCollidingWith);
    }
    public bool IsColliding(Dictionary<int, List<GameObject>> dictionaryToSearch) {
        // to optimize performance iterate in this method and break on first item encountered
        return (CollidingObjectsSize(dictionaryToSearch) > 0) ? true : false;
    }
    
    public bool IsColliding(int layer) {
        return IsColliding(layer, gameObjectsCollidingWith);
    }
    public bool IsColliding(int layer, Dictionary<int, List<GameObject>> dictionaryToSearch) {
        // the parameter can be some bitshifted integer
        // returns true if colliding with any game objects on that layer
        return (CollidingObjectsSize(layer, dictionaryToSearch) > 0) ? true : false;
    }
    
    public List<GameObject> GetObjectsCollidingWith() {
        // Returns the list of gameObjects that are currently being collided with
        // This list is built from the Collision2DDetectors
        return null;
    }
    public List<GameObject>  GetObjectsCollidingWith(int layer) {
        // Returns the list of gameObjects that are currently being collided with, but only the ones for that layer
        // Return a copy of the list using
        return null;
    }
}