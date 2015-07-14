using UnityEngine;
using System.Collections;

public class PlayerValuesEvent : CustomEventObject {
    public Player player = null;
    public bool enableController = false;
    
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
        if(player == null) {
            Debug.LogError("The 'player' reference needs to be set in the 'playerValuesEvent' Script on " + this.gameObject.name);
        }
    }
    
    public override void ExecuteLogic() {
        player.ToggleController(player.gameObject, enableController);
    }
}