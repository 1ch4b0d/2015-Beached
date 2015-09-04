using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour {
    public GameObject prefabToGenerate = null;
    protected GameObject parentGameObject = null;
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
        PerformRecycleCheck();
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
        
        if(parentGameObject == null) {
            parentGameObject = this.gameObject;
        }
    }
    
    protected virtual GameObject Create() {
        GameObject newGameObject = (GameObject)GameObject.Instantiate(prefabToGenerate);
        newGameObject.transform.parent = parentGameObject.transform;
        return newGameObject;
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
    
    public virtual void Decomission(GameObject gameObjectToDecomission) {
        inUseGameObjects.Remove(gameObjectToDecomission);
        AddToAvailable(gameObjectToDecomission);
        gameObjectToDecomission.SetActive(false);
    }
    
    protected virtual void Destroy(GameObject gameObjectToDestroy) {
        availableGameObjects.Remove(gameObjectToDestroy);
        inUseGameObjects.Remove(gameObjectToDestroy);
        Destroy(gameObjectToDestroy);
    }
    
    protected virtual void AddToAvailable(GameObject gameObjectToAdd) {
        if(!availableGameObjects.Contains(gameObjectToAdd)) {
            availableGameObjects.Add(gameObjectToAdd);
        }
        gameObjectToAdd.transform.parent = parentGameObject.transform;
    }
    
    protected virtual void AddToInUse(GameObject gameObjectToAdd) {
        if(!inUseGameObjects.Contains(gameObjectToAdd)) {
            inUseGameObjects.Add(gameObjectToAdd);
        }
        gameObjectToAdd.transform.parent = parentGameObject.transform;
    }
    
    public virtual void PerformRecycleCheck() {
        // do nothing
    }
}