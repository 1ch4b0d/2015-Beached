using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigureGameOverEvent : CustomEventObject {
    public GameObject playerGameObject = null;
    public GameObject ghostPlayerGameObject = null;
    public GameObject georgeThorntonGameObject = null;
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
                                                     
        georgeThorntonGameObject.transform.position = georgeThorntonStartPosition;
        ghostPlayerGameObject.transform.position = ghostPlayerStartPosition;
        spotlightGameObject.transform.position = spotlightStartPosition;
    }
    
    public void StartGameOverScreen() {
        float duration = 1f;
        
        Vector3 georgeThorntonEndPosition = new Vector3(CameraManager.Instance.GetMainCamera().GetRightBoundWorldPosition() - georgeThorntonGameObject.transform.localScale.x,
                                                        playerGameObject.transform.position.y,
                                                        georgeThorntonGameObject.transform.position.z);
                                                        
        Vector3 spotlightEndScale = new Vector3(spotlightGameObject.transform.localScale.x,
                                                spotlightGameObject.transform.localScale.y + 10,
                                                spotlightGameObject.transform.localScale.z);
                                                
                                                
        // Thornton
        GoTweenConfig thorntonMoveTweenConfig = new GoTweenConfig()
        .position(georgeThorntonEndPosition)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            FireFinishEvents();
        });
        GoTween thorntonRunIn = Go.to(georgeThorntonGameObject.transform,
                                      duration,
                                      thorntonMoveTweenConfig);
                                      
        // Spotlight
        GoTweenConfig spotlightTweenConfig = new GoTweenConfig()
        .scale(spotlightEndScale)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            FireFinishEvents();
        });
        GoTween spotlightExpandIn = Go.to(spotlightGameObject.transform,
                                          duration,
                                          spotlightTweenConfig);
    }
}
