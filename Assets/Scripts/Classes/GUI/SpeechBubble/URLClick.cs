using UnityEngine;
using System.Collections;

public class URLClick : MonoBehaviour {
    // Use this for initialization
    protected virtual void Awake() {
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
    }
    
    protected virtual void OnClick() {
        UILabel lbl = GetComponent<UILabel>();
        string url = lbl.GetUrlAtPosition(UICamera.lastWorldPosition);
        // Debug.Log("Clicked on: " + url);
        if(url != null
            && url.Length > 0) {
            Application.OpenURL(url);
        }
    }
}
