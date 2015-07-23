using UnityEngine;
using System.Collections;

public class MobileInterfaceButton : MonoBehaviour {
    public string inputName = "";
    public bool isPressed  = false;
    
    // Use this for initialization
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
        if(this.inputName.Equals("")) {
            Debug.LogError(this.gameObject.name + " has an invalid 'inputName' name in its script 'MobileInterfaceButton'. Please assign a name to it");
        }
        
    }
    
    public void PressButton() {
        ToggleIsPressed(true);
    }
    
    public void DepressButton() {
        ToggleIsPressed(false);
    }
    
    public void ToggleIsPressed(bool newIsPressed) {
        isPressed = newIsPressed;
        // onKeyDown(inputName);
    }
    
    public bool IsPressed() {
        return isPressed;
    }
}
