using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigureGameOverEvent : CustomEventObject {
    public GameObject playerGameObject = null;
    public GameObject ghostPlayerGameObject = null;
    public GameObject georgeThorntonGameObject = null;
    public Collider2D georgeThorntonGameOverCollider = null;
    public GameObject spotlightGameObject = null;
    private GoTween thorntonRunIn = null;
    private GoTween spotlightExpandIn = null;
    
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
    }
    
    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
    
    protected override void Initialize() {
        base.Initialize();
        
        if(playerGameObject == null) {
            Debug.LogError(this.gameObject.name + " needs its 'playerGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(ghostPlayerGameObject == null) {
            Debug.LogError(this.gameObject.name + " needs its 'ghostPlayerGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(georgeThorntonGameObject == null) {
            Debug.LogError(this.gameObject.name + " needs its 'georgeThorntonGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(georgeThorntonGameOverCollider == null) {
            Debug.LogError(this.gameObject.name + " needs its 'georgeThorntonGameOverCollider' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(spotlightGameObject == null) {
            Debug.LogError(this.gameObject.name + " needs its 'spotlightGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        ConfigureGameOverScreen();
        StartGameOverScreen();
        FireExecuteEvents();
    }
    
    public void ConfigureGameOverScreen() {
        // TODO: make it do a check if thornton is on screen. If he is on screen, don't set his position, and instead just tween to the game over position
        Vector3 georgeThorntonStartPosition = new Vector3(CameraManager.Instance.GetMainCamera().GetRightBoundWorldPosition() + (georgeThorntonGameObject.transform.localScale.x + 2),
                                                          playerGameObject.transform.position.y,
                                                          georgeThorntonGameObject.transform.position.z);
                                                          
        Vector3 ghostPlayerStartPosition = playerGameObject.transform.position;
        
        Vector3 spotlightStartPosition = new Vector3(CameraManager.Instance.GetMainCamera().GetLeftBoundWorldPosition() + (spotlightGameObject.transform.localScale.x),
                                                     playerGameObject.transform.position.y,
                                                     spotlightGameObject.transform.position.z);
        Player player = playerGameObject.GetComponent<Player>();
        GhostPlayer ghostPlayer = ghostPlayerGameObject.GetComponent<GhostPlayer>();
        
        
        georgeThorntonGameObject.transform.position = georgeThorntonStartPosition;
        georgeThorntonGameOverCollider.enabled = true;
        //----------------------------------------------
        ghostPlayerGameObject.SetActive(true);
        ghostPlayerGameObject.transform.position = ghostPlayerStartPosition;
        ghostPlayer.Unpause();
        //----------------------------------------------
        player.Pause();
        //----------------------------------------------
        spotlightGameObject.SetActive(true);
        spotlightGameObject.transform.position = spotlightStartPosition;
    }
    
    public void StartGameOverScreen() {
        float duration = 1f;
        
        Animator georgeThorntonAnimator = georgeThorntonGameObject.GetComponent<Animator>();
        Vector3 georgeThorntonEndPosition = new Vector3(CameraManager.Instance.GetMainCamera().GetRightBoundWorldPosition() - georgeThorntonGameObject.transform.localScale.x * 2,
                                                        playerGameObject.transform.position.y,
                                                        georgeThorntonGameObject.transform.position.z);
                                                        
        Vector3 spotlightEndScale = new Vector3(spotlightGameObject.transform.localScale.x,
                                                spotlightGameObject.transform.localScale.y + 20,
                                                spotlightGameObject.transform.localScale.z);
                                                
                                                
        georgeThorntonAnimator.Play("Running");
        //----------------------------
        if(thorntonRunIn != null) {
            thorntonRunIn.destroy();
            thorntonRunIn = null; // because I'm cautious like that
        }
        GoTweenConfig thorntonMoveTweenConfig = new GoTweenConfig()
        .position(georgeThorntonEndPosition)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            FireFinishEvents();
            georgeThorntonAnimator.Play("GameOverEncouragement");
        });
        thorntonRunIn = Go.to(georgeThorntonGameObject.transform,
                              duration,
                              thorntonMoveTweenConfig);
        //----------------------------
        if(spotlightExpandIn != null) {
            spotlightExpandIn.destroy();
            spotlightExpandIn = null; // because I'm cautious like that
        }
        // Spotlight
        GoTweenConfig spotlightTweenConfig = new GoTweenConfig()
        .scale(spotlightEndScale)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            FireFinishEvents();
        });
        spotlightExpandIn = Go.to(spotlightGameObject.transform,
                                  duration,
                                  spotlightTweenConfig);
    }
}
