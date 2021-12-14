using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : StateMachineBehaviour
{
    GameObject playerObject;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Renderer>().material.color = Color.red;
        playerObject = GameObject.Find("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CanSeePlayer(animator))
            LookTowardsPlayer(animator);
        else
            animator.SetInteger("EnemyState", 2);

    }

    void LookTowardsPlayer(Animator animator)
    {
        Quaternion lookRotation = Quaternion.LookRotation((playerObject.transform.position - animator.transform.position).normalized);
        lookRotation.z = 0;
        lookRotation.x = 0;
        animator.transform.rotation = lookRotation;
    }

    bool CanSeePlayer(Animator animator)
    {
        Vector3 vectorForward = animator.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(animator.transform.position, vectorForward, out hit, Mathf.Infinity))
            if (hit.transform.gameObject.name == "Player")
                return true;

        return false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
