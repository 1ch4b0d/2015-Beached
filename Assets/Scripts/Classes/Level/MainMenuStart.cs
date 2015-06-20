using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {

    // Use this for initialization
    void Start() {
        CameraManager.Instance.MainCamera().GetComponent<CameraFollow>().enabled = false;
    }
    
    // Update is called once per frame
    void Update() {
    }
}
