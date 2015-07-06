using UnityEngine;
using System.Collections;

public class PanFromTriggerToMarker : CustomTrigger {
    public Transform objectFinalPosition = null;
    public bool cameraFollowOnFinished = false;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        gameObjectExecuting.transform.position = objectFinalPosition.position;
        
        Player player = gameObjectExecuting.GetComponent<Player>();
        player.ToggleAcrocatic(gameObjectExecuting, false);
        player.ZeroOutVelocity();
        
        // Target Position including the camera's follow offset so that it doesn't jutter
        Vector3 targetPosition = player.gameObject.transform.position;
        targetPosition += CameraManager.Instance.CameraFollow().GetFollowOffsetVector3();
        targetPosition.z = CameraManager.Instance.GetMainCamera().transform.position.z;
        
        OnPanStart();
        
        //On Tween Finish
        Go.to(CameraManager.Instance.GetMainCamera().transform,
              1f,
              new GoTweenConfig().position(targetPosition).setEaseType(GoEaseType.BackIn)
        .onComplete(complete => {
            CameraFollow mainCameraFollow = CameraManager.Instance.CameraFollow();
            
            player.ToggleAcrocatic(gameObjectExecuting, true);
            
            mainCameraFollow.enabled = cameraFollowOnFinished;
            
            OnPanFinish();
        }));
    }
    
    public virtual void OnPanStart() {
    }
    
    public virtual void OnPanFinish() {
    }
}