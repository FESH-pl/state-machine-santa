using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostBehaviour : StateMachineBehaviour
{
    public float alertDuration = 5f;
    float toPatrolStateTime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        toPatrolStateTime = Time.time + alertDuration;
        animator.GetComponent<Renderer>().material.color = Color.yellow;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CanSeePlayer(animator))
            animator.SetInteger("EnemyState", 1);
        if (Time.time > toPatrolStateTime)
            animator.SetInteger("EnemyState", 0);
    }

    public bool CanSeePlayer(Animator animator)
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
