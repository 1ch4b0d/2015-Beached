using UnityEngine;
using System.Collections;

// This is useless without a way to return a game object....
// TODO: Configure Execute and ExecuteLogic to return a list of game objects...
//       and make it so that downstream events can receive these objects
public class CreateGameObjectEvent : CustomEventObject {
    public GameObject prefabToCreate = null;
    public Transform startTransform = null;
    
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
        if(prefabToCreate == null) {
            this.gameObject.LogComponentError("prefabToCreate", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        CreateObject();
    }
    
    public void CreateObject() {
        GameObject newGameObject = Factory.NewReference(prefabToCreate);
        if(startTransform != null) {
            newGameObject.transform.position = startTransform.position;
            newGameObject.transform.rotation = startTransform.rotation;
            newGameObject.transform.localScale = startTransform.localScale;
        }
    }
}
