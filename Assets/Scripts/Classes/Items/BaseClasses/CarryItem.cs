using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarryItem : MonoBehaviour {
    public GameObject objectCarryingItem = null;
    public GameObject itemBeingCarried = null;
    public GameObject carryItemAnchor = null;
    
    public List<CustomEventsManager> onPickupItemEvents = null;
    public List<CustomEventsManager> onDropItemEvents = null;
    public List<CustomEventsManager> onThrowItemEvents = null;
    // Use this for initialization
    void Start() {
        Initializate();
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public void Initializate() {
        if(objectCarryingItem == null) {
            objectCarryingItem = this.gameObject;
            if(objectCarryingItem == null) {
                Debug.LogError(" The component 'objectCarryingItem' has not been assigned. Please assign it before using this script.");
            }
        }
        if(carryItemAnchor == null) {
            carryItemAnchor = this.gameObject;
            if(carryItemAnchor == null) {
                Debug.LogError(" The component 'carryItemAnchor' has not been assigned. Please assign it before using this script.");
            }
        }
    }
    
    public GameObject GetItemBeingCarried() {
        return itemBeingCarried;
    }
    
    public bool IsCarryingItem() {
        if(itemBeingCarried != null) {
            return true;
        }
        else {
            return false;
        }
    }
    
    // TODO: Consider removing this and placing it in the item class so that each
    //       item handles its own logic for picking itself up. Make a method signature
    //       that handles it in terms of player and so forth
    public void PickUpItem(GameObject itemToPickUp) {
        Item item = itemToPickUp.GetComponent<Item>();
        if(item != null) {
            // Debug.Log("picking up");
            // item.DisableColliders();
            item.DisableTriggers();
            
            itemBeingCarried = itemToPickUp;
            
            if(objectCarryingItem.transform.localScale.x < 0) {
                itemBeingCarried.transform.localScale = new Vector3(itemBeingCarried.transform.localScale.x * -1, itemBeingCarried.transform.localScale.y, itemBeingCarried.transform.localScale.z);
            }
            // Track the item's scale before it's set up
            Vector3 previousScale = itemToPickUp.transform.localScale;
            itemBeingCarried.transform.parent = carryItemAnchor.transform;
            itemBeingCarried.transform.position = carryItemAnchor.transform.position;
            itemBeingCarried.GetComponent<Rigidbody2D>().isKinematic = true;
            itemBeingCarried.transform.localScale = previousScale;
            ItemManager.Instance.Remove(itemToPickUp); // wtf is this doing? What was I thinking?
            
            FirePickUpItemEvents();
        }
    }
    
    public void DropItem(Vector3 itemVelocity) {
        Item item = itemBeingCarried.GetComponent<Item>();
        if(item != null) {
            // Debug.Log("dropping");
            // item.EnableColliders();
            item.EnableTriggers();
            
            // Flips the item on drop so it looks the right way
            if(this.gameObject.transform.localScale.x < 0) {
                itemBeingCarried.transform.localScale = new Vector3(itemBeingCarried.transform.localScale.x * -1, itemBeingCarried.transform.localScale.y, itemBeingCarried.transform.localScale.z);
            }
            itemBeingCarried.transform.eulerAngles = Vector3.zero;
            
            ItemManager.Instance.Add(itemBeingCarried);
            itemBeingCarried.GetComponent<Rigidbody2D>().isKinematic = false;
            itemBeingCarried.GetComponent<Rigidbody2D>().velocity = itemVelocity;
            itemBeingCarried = null;
            
            FireDropItemEvents();
        }
    }
    
    public void ThrowItem(Vector3 itemVelocity) {
        itemVelocity.z = 0;
        Item item = itemBeingCarried.GetComponent<Item>();
        if(item != null) {
            // Debug.Log("throwing");
            // item.EnableColliders();
            item.EnableTriggers();
            
            // Flips the item on drop so it looks the right way
            if(objectCarryingItem.transform.localScale.x < 0) {
                itemBeingCarried.transform.localScale = new Vector3(itemBeingCarried.transform.localScale.x * -1, itemBeingCarried.transform.localScale.y, itemBeingCarried.transform.localScale.z);
            }
            itemBeingCarried.transform.eulerAngles = Vector3.zero;
            
            ItemManager.Instance.Add(itemBeingCarried);
            itemBeingCarried.GetComponent<Rigidbody2D>().isKinematic = false;
            itemBeingCarried.GetComponent<Rigidbody2D>().velocity = itemVelocity;
            itemBeingCarried = null;
            
            FireThrowItemEvents();
        }
    }
    
    protected void FirePickUpItemEvents() {
        foreach(CustomEventsManager customEventsManager in onPickupItemEvents) {
            customEventsManager.Execute();
        }
    }
    
    protected void FireDropItemEvents() {
        foreach(CustomEventsManager customEventsManager in onDropItemEvents) {
            customEventsManager.Execute();
        }
    }
    
    protected void FireThrowItemEvents() {
        foreach(CustomEventsManager customEventsManager in onThrowItemEvents) {
            customEventsManager.Execute();
        }
    }
}