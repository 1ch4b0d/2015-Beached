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
            this.gameObject.LogComponentError("playerGameObject", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        player.Pause();
        GameOverManager.Instance.TriggerGameOver();
    }
}