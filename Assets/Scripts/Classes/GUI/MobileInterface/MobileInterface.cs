using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobileInterface : MonoBehaviour {

    public List<MobileInterfaceButton> buttons = null;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    
    public MobileInterfaceButton GetButtons(string buttonName) {
        List<MobileInterfaceButton> mobileInterfaceButtonsToReturn = new List<MobileInterfaceButton>();
        foreach(MobileInterfaceButton mobileInterfaceButton in buttons) {
            if(mobileInterfaceButton.name.Equals(buttonName)) {
                mobileInterfaceButtonsToReturn.Add(mobileInterfaceButton);
            }
        }
        
        if(mobileInterfaceButtonsToReturn.Count > 1) {
            Debug.LogError(this.gameObject.name + " is requesting a single button from it received multiple buttons back instead of a single button. Please verify this was intentional.");
        }
        else if(mobileInterfaceButtonsToReturn.Count == 1) {
            return  mobileInterfaceButtonsToReturn[0];
        }
        
        // return null if nothing else worked
        return  null;
    }
}
