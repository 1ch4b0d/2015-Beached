using UnityEngine;
using System.Collections;

public class CreditPanel : MonoBehaviour {
    public UILabel header = null;
    public UILabel text = null;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public UILabel GetHeader() {
        return header;
    }
    
    public UILabel GetCredit() {
        return text;
    }
}
