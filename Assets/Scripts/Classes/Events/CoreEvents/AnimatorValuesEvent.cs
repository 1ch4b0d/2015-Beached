using UnityEngine;
using System.Collections;

///<summary>
/// Used to toggle an animator's values
///<summary>
public class AnimatorValuesEvent : CustomEventObject {
    [Tooltip("The animator whose values will be changed.")]
    public Animator animator = null;
    [Tooltip("Used to indicate an Animation State to play. If empty, no animation state is played")]
    public string animationToPlay = ""; // if empty no animation plays
    
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
        if(animator == null) {
            Debug.LogError("The 'animator' reference needs to be set in the 'AnimatorValuesEvent' Script on " + this.gameObject.name);
        }
    }
    
    public override void ExecuteLogic() {
        SetAnimatorValues();
    }
    
    public void SetAnimatorValues() {
        //------------------------------------------------------------------
        // Animation to Play
        //------------------------------------------------------------------
        if(!animationToPlay.Equals("")) {
        }
    }
}
