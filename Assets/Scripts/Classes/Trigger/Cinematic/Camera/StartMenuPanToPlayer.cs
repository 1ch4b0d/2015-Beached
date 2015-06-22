using UnityEngine;
using System.Collections;

public class StartMenuPanToPlayer: CustomTrigger {
    public Transform startTransform = null;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    // public override void Entered(GameObject gameObjectEntering) {
    //     base.Entered(gameObjectEntering);
    // }
    // public override void Exited(GameObject gameObjectExiting) {
    //     base.Exited(gameObjectExiting);
    // }
    // public override void Execute(GameObject gameObjectExecuting) {
    //     base.Exited(gameObjectExecuting);
    // }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        gameObjectExecuting.GetComponent<Player>().ToggleAcrocatic(gameObjectExecuting, false);
        if(startTransform != null) {
            gameObjectExecuting.transform.position = startTransform.position;
        }
        
        Vector3 targetPosition = PlayerManager.Instance.transform.position;
        targetPosition.z = CameraManager.Instance.GetCamera().transform.position.z;
        
        // offset padding
        targetPosition.y += 2;
        
        Go.to(CameraManager.Instance.GetCamera().transform,
              1f,
              new GoTweenConfig().position(targetPosition).setEaseType(GoEaseType.BackIn)
        .onComplete(complete => {
            CameraFollow mainCameraFollow = CameraManager.Instance.CameraFollow();
            
            GameObject blockingColliderGameObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Colliders/BlockingCollider") as GameObject);
            BoxCollider2D blockingCollider = blockingColliderGameObject.GetComponent<BoxCollider2D>();
            
            gameObjectExecuting.GetComponent<Player>().ToggleAcrocatic(gameObjectExecuting, true);
            
            mainCameraFollow.enabled = true;
            mainCameraFollow.minXAndY = new Vector2(targetPosition.x, mainCameraFollow.minXAndY.y);
            
            float leftCameraBoundWorldPosition = CameraManager.Instance.GetLeftBoundWorldPosition();
            float bottomCameraBoundWorldPosition = CameraManager.Instance.GetBottomBoundWorldPosition();
            Debug.Log("Player x: " + PlayerManager.Instance.transform.position.x);
            Debug.Log("Left bound: " + leftCameraBoundWorldPosition);
            Debug.Log("Left: " + CameraManager.Instance.GetLeftBoundWorldPosition());
            
            // Create a collider that blocks the player from moving left
            blockingColliderGameObject.GetComponent<BoxCollider2D>().size = new Vector2(1f, 1000f);
            blockingColliderGameObject.transform.position = new Vector3(leftCameraBoundWorldPosition + blockingCollider.size.x / 4, bottomCameraBoundWorldPosition, 0);
        }));
    }
}