using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
    public List<GameObject> items = null;
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile ItemManager _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static ItemManager() {
    }
    
    public static ItemManager Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    if(_instance == null) {
                        GameObject ItemManagerGameObject = new GameObject("ItemManager");
                        _instance = (ItemManagerGameObject.AddComponent<ItemManager>()).GetComponent<ItemManager>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private ItemManager() {
    }
    
    public void Awake() {
        Initialize();
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    public void Initialize() {
        _instance = GameObject.FindObjectOfType<ItemManager>();
        
        // Eeehhhhh this is probably dumb to do, but on initialization this will
        // grab all the items in the scene and add them to the item manager.
        // Really though this should be done from the get go in each of the item
        // classes, so that they call this manager themselves directly.
        Item[] foundItems = GameObject.FindObjectsOfType(typeof(Item)) as Item[];
        items = new List<GameObject>();
        foreach(Item item in foundItems) {
            items.Add(item.gameObject);
        }
    }
    
    public void Add(GameObject itemToAdd) {
        if(!items.Contains(itemToAdd)) {
            items.Add(itemToAdd);
            
            Vector3 previousScale = itemToAdd.transform.localScale;
            itemToAdd.transform.parent = this.gameObject.transform;
            itemToAdd.transform.localScale = previousScale;
        }
    }
    
    public void Remove(GameObject itemToRemove) {
        items.Remove(itemToRemove);
    }
}