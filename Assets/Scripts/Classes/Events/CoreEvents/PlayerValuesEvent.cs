using UnityEngine;
using System.Collections;

public class PlayerValuesEvent : CustomEventObject {
    // GameObject is used because the unity inspector wouldn't just let me maintain
    // a reference to the Player script. Freaking Unity, I seriously don't know
    // why it doesn't work by assigning just the player component.
    public GameObject playerGameObject = null;
    public Player player = null;
    public bool enableController = false;
    public bool zeroOutVelocity = false;
    
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
            Debug.LogError(this.gameObject.name + " needs its 'playerGameObject' reference to be set in the 'PlayerValuesEvent' Script");
        }
        else {
            if(player == null) {
                player = playerGameObject.GetComponent<Player>();
                if(player == null) {
                    Debug.LogError(this.gameObject.name + " needs its 'player' reference to be set in the 'PlayerValuesEvent' Script");
                }
            }
        }
    }
    
    public override void ExecuteLogic() {
        player.ToggleController(player.gameObject, enableController);
        if(zeroOutVelocity) {
            player.ZeroOutVelocity();
        }
    }
}