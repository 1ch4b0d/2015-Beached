using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigureGameOverEvent : CustomEventObject {
    public GameObject playerGameObject = null;
    public GameObject ghostPlayerGameObject = null;
    public GameObject georgeThorntonGameObject = null;
    public GameObject georgeThorntonSpeechBubbleTriggerGameObject = null;
    public GameObject tryAgainColliderGameObject = null;
    public GameObject spotlightGameObject = null;
    
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
            Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'playerGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(ghostPlayerGameObject == null) {
            Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'ghostPlayerGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(georgeThorntonGameObject == null) {
            Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'georgeThorntonGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(georgeThorntonSpeechBubbleTriggerGameObject == null) {
            Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'georgeThorntonSpeechBubbleTriggerGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(tryAgainColliderGameObject == null) {
            Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'tryAgainColliderGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
        }
        if(spotlightGameObject == null) {
            Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'spotlightGameObject' reference to be set in the 'ConfigureGameOverEvent' Script");
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
        Vector3 georgeThorntonScreenPoint = Camera.main.WorldToViewportPoint(georgeThorntonGameObject.transform.position);
        // set the position if thornton is off screen, otherwise, do not set his position
        bool isThortonOnScreen = (georgeThorntonScreenPoint.x > 0
                                  && georgeThorntonScreenPoint.x < 1
                                  && georgeThorntonScreenPoint.y > 0
                                  && georgeThorntonScreenPoint.y < 1) ? true : false;
                                  
        // TODO: make it do a check if thornton is on screen. If he is on screen, don't set his position, and instead just tween to the game over position
        Vector3 georgeThorntonStartPosition = new Vector3(CameraManager.Instance.GetMainCamera().GetRightBoundWorldPosition() + (georgeThorntonGameObject.transform.localScale.x + 2),
                                                          playerGameObject.transform.position.y,
                                                          georgeThorntonGameObject.transform.position.z);
                                                          
        Vector3 ghostPlayerStartPosition = playerGameObject.transform.position;
        
        Vector3 spotlightStartPosition = new Vector3(CameraManager.Instance.GetMainCamera().GetLeftBoundWorldPosition() + (spotlightGameObject.transform.localScale.x),
                                                     playerGameObject.transform.position.y,
                                                     spotlightGameObject.transform.position.z);
        // Acrocatic.Player acrocaticPlayer = playerGameObject.GetComponent<Acrocatic.Player>();
        // Player player = playerGameObject.GetComponent<Player>();
        GhostPlayer ghostPlayer = ghostPlayerGameObject.GetComponent<GhostPlayer>();
        
        if(!isThortonOnScreen) {
            georgeThorntonGameObject.transform.position = georgeThorntonStartPosition;
        }
        georgeThorntonSpeechBubbleTriggerGameObject.SetActive(false);
        //----------------------------------------------
        ghostPlayerGameObject.SetActive(true);
        ghostPlayerGameObject.transform.position = ghostPlayerStartPosition;
        ghostPlayer.Unpause();
        //----------------------------------------------
        // acrocaticPlayer.Pause();
        //----------------------------------------------
        // player.Pause();
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
        GoTweenConfig thorntonMoveTweenConfig = new GoTweenConfig()
        .position(georgeThorntonEndPosition)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            tryAgainColliderGameObject.SetActive(true);
            georgeThorntonAnimator.Play("GameOverEncouragement");
            FireFinishEvents();
        });
        georgeThorntonGameObject.AddGoTween(Go.to(georgeThorntonGameObject.transform,
                                                  duration,
                                                  thorntonMoveTweenConfig));
        //----------------------------
        // Spotlight
        GoTweenConfig spotlightTweenConfig = new GoTweenConfig()
        .scale(spotlightEndScale)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {});
        spotlightGameObject.AddGoTween(Go.to(spotlightGameObject.transform,
                                             duration,
                                             spotlightTweenConfig));
    }
}
