using UnityEngine;
using System.Collections;

public class PlayerFailedGameEvent : CustomEventObject {
    public Player player = null;
    
    // // Use this for initialization
    // protected override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        
        player = PlayerManager.Instance.GetPlayer();
        if(player == null) {
            Debug.LogError(this.gameObject.name + " needs its 'playerGameObject' reference to be set in the 'PlayerFailedGameEvent' Script");
        }
    }
    
    public override void ExecuteLogic() {
        player.Pause();
    }
}