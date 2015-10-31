using UnityEngine;
using System.Collections;

public class AcrocaticPlayerJumpValuesEvent : CustomEventObject {

    public GameObject playerGameObject = null;
    public Acrocatic.PlayerJump acrocaticPlayerJump = null;
    public bool pause = false;
    public bool unpause = false;
    
    // // Use this for initialization
    // protected override void Awake() {
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(playerGameObject == null) {
            playerGameObject = PlayerManager.Instance.GetPlayerGameObject();
            if(playerGameObject == null) {
                Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'playerGameObject' reference to be set in the 'AcrocaticPlayerJumpValuesEvent' Script");
            }
        }
        
        if(acrocaticPlayerJump == null) {
            acrocaticPlayerJump = playerGameObject.GetComponent<Acrocatic.PlayerJump>();
            if(acrocaticPlayerJump == null) {
                Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'acrocaticPlayerJump' reference to be set in the 'AcrocaticPlayerJumpValuesEvent' Script");
            }
        }
    }
    
    public override void ExecuteLogic() {
        if(pause) {
            acrocaticPlayerJump.Pause();
        }
        
        if(unpause) {
            acrocaticPlayerJump.Unpause();
        }
    }
}