using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour {
    public GameObject prefabToGenerate = null;
    public List<GameObject> availableGameObjects = null;
    public List<GameObject> inUseGameObjects = null;
    
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
        if(prefabToGenerate == null) {
            Debug.LogError(this.gameObject.name + " game object requires the 'prefabToGenerate' property to be set before OnAwake for the 'GameObjectPool' class. Please fix this.");
        }
        
        if(availableGameObjects == null) {
            availableGameObjects = new List<GameObject>();
        }
        
        if(inUseGameObjects == null) {
            inUseGameObjects = new List<GameObject>();
        }
    }
    
    protected virtual void AddAvailable(GameObject gameObjectToAdd) {
        if(!availableGameObjects.Contains(gameObjectToAdd)) {
            availableGameObjects.Add(gameObjectToAdd);
        }
        gameObjectToAdd.transform.parent = this.gameObject.transform;
    }
    
    protected virtual GameObject Create() {
        Debug.LogError("You are calling the base 'Create' method of the 'GameObjectPool' class. Please fix this so that their is an extended class that overwrites this functionality.");
        return null;
    }
    
    public virtual void Decomission(GameObject gameObjectToDecomission) {
        inUseGameObjects.Remove(gameObjectToDecomission);
        AddAvailable(gameObjectToDecomission);
        gameObjectToDecomission.SetActive(false);
    }
    
    // The will request a new game object from the available game object pool.
    // If no free object is available it creates a new game object.
    public virtual GameObject Issue() {
        GameObject gameObjectBeingIssued = null;
        if(availableGameObjects.Count > 0) {
            int indexToReturn = 0;
            gameObjectBeingIssued = availableGameObjects[indexToReturn];
            availableGameObjects.RemoveAt(indexToReturn);
        }
        else {
            gameObjectBeingIssued = Create();
        }
        
        inUseGameObjects.Add(gameObjectBeingIssued);
        gameObjectBeingIssued.SetActive(true); // just in case it's not enabled
        
        return gameObjectBeingIssued;
    }
    
    protected virtual void Destroy() {
    }
}