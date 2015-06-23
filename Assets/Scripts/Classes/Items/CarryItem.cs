using UnityEngine;
using System.Collections;

public class CarryItem : MonoBehaviour {
    public GameObject itemBeingCarried = null;
    public GameObject carryItemAnchor = null;
    
    // Use this for initialization
    void Start() {
        if(carryItemAnchor == null) {
            carryItemAnchor = this.gameObject;
            Debug.LogError(" The component 'carryItemAnchor' has not been assigned. Please assign it before using this script.");
        }
    }
    
    // Update is called once per frame
    void Update() {
    
    }
    
    public bool IsCarryingItem() {
        if(itemBeingCarried != null) {
            return true;
        }
        else {
            return false;
        }
    }
    
    public void PickUpItem(GameObject itemToPickUp) {
        Item item = itemToPickUp.GetComponent<Item>();
        if(item != null) {
            Debug.Log("picking up");
            itemBeingCarried = itemToPickUp;
            
            // Track the item's scale before it's set up
            Vector3 previousScale = itemToPickUp.transform.localScale;
            itemBeingCarried.transform.parent = carryItemAnchor.transform;
            itemBeingCarried.transform.position = carryItemAnchor.transform.position;
            itemBeingCarried.GetComponent<Rigidbody2D>().isKinematic = true;
            itemBeingCarried.transform.localScale = previousScale;
            ItemManager.Instance.Remove(itemToPickUp);
        }
    }
    
    public void DropItem() {
        Debug.Log("dropping");
        ItemManager.Instance.Add(itemBeingCarried);
        itemBeingCarried.GetComponent<Rigidbody2D>().isKinematic = false;
        itemBeingCarried = null;
    }
}
