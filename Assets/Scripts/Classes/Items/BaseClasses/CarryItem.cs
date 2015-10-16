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
            // Debug.Log("----------");
            // Debug.Log("picking up");
            
            item.DisableTriggers();
            
            itemBeingCarried = itemToPickUp;
            
            // Track the item's scale before it's set up
            Vector3 previousScale = itemToPickUp.transform.localScale;
            itemBeingCarried.transform.parent = carryItemAnchor.transform;
            itemBeingCarried.transform.position = carryItemAnchor.transform.position;
            itemBeingCarried.GetComponent<Rigidbody2D>().isKinematic = true;
            itemBeingCarried.transform.localScale = previousScale;
            ItemManager.Instance.Remove(itemToPickUp); // wtf is this doing? What was I thinking?
            
            // Always update last
            UpdatePickUpItemFacing(objectCarryingItem, item.gameObject);
            
            FirePickUpItemEvents();
        }
    }
    
    public void DropItem(Vector3 itemVelocity) {
        Item item = itemBeingCarried.GetComponent<Item>();
        if(item != null) {
            // Debug.Log("----------");
            // Debug.Log("dropping");
            
            item.EnableTriggers();
            
            itemBeingCarried.transform.eulerAngles = Vector3.zero;
            
            ItemManager.Instance.Add(itemBeingCarried);
            itemBeingCarried.GetComponent<Rigidbody2D>().isKinematic = false;
            itemBeingCarried.GetComponent<Rigidbody2D>().velocity = itemVelocity;
            itemBeingCarried = null;
            
            // Always update last
            UpdateReleaseItemFacing(objectCarryingItem, item.gameObject);
            
            FireDropItemEvents();
        }
    }
    
    public void ThrowItem(Vector3 itemVelocity) {
        itemVelocity.z = 0;
        Item item = itemBeingCarried.GetComponent<Item>();
        if(item != null) {
            // Debug.Log("----------");
            // Debug.Log("throwing");
            
            item.EnableTriggers();
            
            itemBeingCarried.transform.eulerAngles = Vector3.zero;
            
            ItemManager.Instance.Add(itemBeingCarried);
            itemBeingCarried.GetComponent<Rigidbody2D>().isKinematic = false;
            itemBeingCarried.GetComponent<Rigidbody2D>().velocity = itemVelocity;
            itemBeingCarried = null;
            
            // Always update last
            UpdateReleaseItemFacing(objectCarryingItem, item.gameObject);
            
            FireThrowItemEvents();
        }
    }
    
    // THIS IS SOFA KING DUMB
    protected void UpdatePickUpItemFacing(GameObject objectCarryingItem, GameObject itemBeingCarried) {
        Item item = itemBeingCarried.GetComponent<Item>();
        
        bool objectCarryingFacingRight = (objectCarryingItem.transform.localScale.x > 0) ? true : false;
        bool itemFacingRight = (itemBeingCarried.transform.localScale.x > 0) ? true : false;
        
        // Debug.Log("-------------------");
        if(objectCarryingFacingRight) {
            // Debug.Log("Object carrying facing right");
            if(itemFacingRight) {
                // Debug.Log("item facing right");
                item.SetFacing(true);
                bool itemSpeechBubbleAnchorOnRight = (item.speechBubbleAnchor.transform.localPosition.x > 0) ? true : false;
                if(itemSpeechBubbleAnchorOnRight) {
                    // Debug.Log("item speech bubble anchor on right");
                }
                else {
                    // Debug.Log("item speech bubble anchor on left");
                }
            }
            else {
                // Debug.Log("item facing left");
                item.SetFacing(false);
                bool itemSpeechBubbleAnchorOnRight = (item.speechBubbleAnchor.transform.localPosition.x > 0) ? true : false;
                if(itemSpeechBubbleAnchorOnRight) {
                    // Debug.Log("item speech bubble anchor on right");
                }
                else {
                    // Debug.Log("item speech bubble anchor on left");
                }
            }
        }
        else {
            // Debug.Log("Object carrying facing left");
            if(itemFacingRight) {
                // Debug.Log("item facing right");
                item.SetFacing(false);
                bool itemSpeechBubbleAnchorOnRight = (item.speechBubbleAnchor.transform.localPosition.x > 0) ? true : false;
                if(itemSpeechBubbleAnchorOnRight) {
                    // Debug.Log("item speech bubble anchor on right");
                }
                else {
                    // Debug.Log("item speech bubble anchor on left");
                }
            }
            else {
                // Debug.Log("item facing left");
                item.SetFacing(true);
                bool itemSpeechBubbleAnchorOnRight = (item.speechBubbleAnchor.transform.localPosition.x > 0) ? true : false;
                if(itemSpeechBubbleAnchorOnRight) {
                    // Debug.Log("item speech bubble anchor on right");
                }
                else {
                    // Debug.Log("item speech bubble anchor on left");
                }
            }
        }
    }
    
    // THIS IS SOFA KING DUMB
    protected void UpdateReleaseItemFacing(GameObject objectCarryingItem, GameObject itemBeingCarried) {
        Item item = itemBeingCarried.GetComponent<Item>();
        
        bool objectCarryingFacingRight = (objectCarryingItem.transform.localScale.x > 0) ? true : false;
        bool itemFacingRight = (itemBeingCarried.transform.localScale.x > 0) ? true : false;
        
        // Debug.Log("-------------------");
        if(objectCarryingFacingRight) {
            // Debug.Log("Object carrying facing right");
            if(itemFacingRight) {
                // Debug.Log("item facing right");
                bool itemSpeechBubbleAnchorOnRight = (item.speechBubbleAnchor.transform.localPosition.x > 0) ? true : false;
                if(itemSpeechBubbleAnchorOnRight) {
                    // Debug.Log("item speech bubble anchor on right");
                }
                else {
                    // Debug.Log("item speech bubble anchor on left");
                    item.speechBubbleAnchor.transform.localPosition = new Vector3(item.speechBubbleAnchor.transform.localPosition.x * -1, item.speechBubbleAnchor.transform.localPosition.y * 1, item.speechBubbleAnchor.transform.localPosition.z * 1);
                }
            }
            else {
                // Debug.Log("item facing left");
                bool itemSpeechBubbleAnchorOnRight = (item.speechBubbleAnchor.transform.localPosition.x > 0) ? true : false;
                if(itemSpeechBubbleAnchorOnRight) {
                    // Debug.Log("item speech bubble anchor on right");
                    item.speechBubbleAnchor.transform.localPosition = new Vector3(item.speechBubbleAnchor.transform.localPosition.x * -1, item.speechBubbleAnchor.transform.localPosition.y * 1, item.speechBubbleAnchor.transform.localPosition.z * 1);
                }
                else {
                    // Debug.Log("item speech bubble anchor on left");
                }
            }
        }
        else {
            // Debug.Log("Object carrying facing left");
            if(itemFacingRight) {
                // Debug.Log("item facing right");
                item.SetFacing(false);
                bool itemSpeechBubbleAnchorOnRight = (item.speechBubbleAnchor.transform.localPosition.x > 0) ? true : false;
                if(itemSpeechBubbleAnchorOnRight) {
                    // Debug.Log("item speech bubble anchor on right");
                    item.speechBubbleAnchor.transform.localPosition = new Vector3(item.speechBubbleAnchor.transform.localPosition.x * -1, item.speechBubbleAnchor.transform.localPosition.y * 1, item.speechBubbleAnchor.transform.localPosition.z * 1);
                }
                else {
                    // Debug.Log("item speech bubble anchor on left");
                }
            }
            else {
                // Debug.Log("item facing left");
                item.SetFacing(true);
                bool itemSpeechBubbleAnchorOnRight = (item.speechBubbleAnchor.transform.localPosition.x > 0) ? true : false;
                if(itemSpeechBubbleAnchorOnRight) {
                    // Debug.Log("item speech bubble anchor on right");
                }
                else {
                    // Debug.Log("item speech bubble anchor on left");
                    item.speechBubbleAnchor.transform.localPosition = new Vector3(item.speechBubbleAnchor.transform.localPosition.x * -1, item.speechBubbleAnchor.transform.localPosition.y * 1, item.speechBubbleAnchor.transform.localPosition.z * 1);
                }
            }
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