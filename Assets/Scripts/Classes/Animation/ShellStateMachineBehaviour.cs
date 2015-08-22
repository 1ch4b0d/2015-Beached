using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: Create an AnimatorHelper cache variable that validates the passed in animator is equivalent to the cached animator, this way both are cached and both are updated only when the animator != animatorPassedIn
public class ShellStateMachineBehaviour : StateMachineBehaviour {

    public string stateName = "";
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AnimatorHelper animatorHelper = animator.gameObject.GetComponent<AnimatorHelper>();
        if(animatorHelper != null) {
            List<CustomEventsManager> stateEvents = animatorHelper.GetOnStateEnter(stateName);
            if(stateEvents != null) {
                foreach(CustomEventsManager customEventsManager in stateEvents) {
                    customEventsManager.Execute();
                }
            }
        }
    }
    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AnimatorHelper animatorHelper = animator.gameObject.GetComponent<AnimatorHelper>();
        if(animatorHelper != null) {
            List<CustomEventsManager> stateEvents = animatorHelper.GetOnStateUpdate(stateName);
            if(stateEvents != null) {
                foreach(CustomEventsManager customEventsManager in stateEvents) {
                    customEventsManager.Execute();
                }
            }
            
            //------------------------------------------------------------------
            // OnAnimationFinish
            //------------------------------------------------------------------
            // Checks to see if animation finished
            // TODO: FIX IT SO THAT ANIMATION FINISH CAN BE DETECTED CORRECTLY
            // THIS IS REALLY BROKEN YOU NEED TO FIX THIS
            if(stateInfo.normalizedTime >= 0.99) {
                Debug.Log(stateName + ": Animation finished");
                List<CustomEventsManager> animationFinishStateEvents = animatorHelper.GetOnAnimationFinish(stateName);
                if(animationFinishStateEvents != null) {
                    foreach(CustomEventsManager customEventsManager in animationFinishStateEvents) {
                        customEventsManager.Execute();
                    }
                }
                // Destroys object
                if(animatorHelper.GetDestroyOnFinish(stateName)) {
                    // Debug.Log("Destroying: " + animator.gameObject.name);
                    Destroy(animator.gameObject);
                }
            }
        }
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AnimatorHelper animatorHelper = animator.gameObject.GetComponent<AnimatorHelper>();
        if(animatorHelper != null) {
            List<CustomEventsManager> stateEvents = animatorHelper.GetOnStateExit(stateName);
            if(stateEvents != null) {
                foreach(CustomEventsManager customEventsManager in stateEvents) {
                    customEventsManager.Execute();
                }
            }
        }
    }
    
    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AnimatorHelper animatorHelper = animator.gameObject.GetComponent<AnimatorHelper>();
        if(animatorHelper != null) {
            List<CustomEventsManager> stateEvents = animatorHelper.GetOnStateMove(stateName);
            if(stateEvents != null) {
                foreach(CustomEventsManager customEventsManager in stateEvents) {
                    customEventsManager.Execute();
                }
            }
        }
    }
    
    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AnimatorHelper animatorHelper = animator.gameObject.GetComponent<AnimatorHelper>();
        if(animatorHelper != null) {
            List<CustomEventsManager> stateEvents = animatorHelper.GetOnStateIK(stateName);
            if(stateEvents != null) {
                foreach(CustomEventsManager customEventsManager in stateEvents) {
                    customEventsManager.Execute();
                }
            }
        }
    }
}