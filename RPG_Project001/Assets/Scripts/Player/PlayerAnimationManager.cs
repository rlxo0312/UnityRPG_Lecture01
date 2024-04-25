using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;
    [Header("애니메이션 제어 변수")]
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }
    /*public void SetAttack()
    {
        animator.SetTrigger("doAttack");
    }*/
    public void PlayerTargetActionAnimation(string targetAnimation, bool IsPerformingAction, bool applayRootmotion = true,   
                                            bool canRotate = false, bool canMove = false)
    {   //애니메니션 클립에 이름을 호출하여 각 palyermanager에서 애니메이션을 쉽게 호출 할 수 있게 캡슐화 한 함수                                                
        animator.CrossFade(targetAnimation, 0.2f);
        player.IsPerformingAction = IsPerformingAction;
        player.applyRootMotion = applayRootmotion;
        player.canRotate = canRotate;
        player.canMove = canMove;   
        
    } 
    public void AnimationTest() //Attack에 있는 animation 이벤트를 삭제한 후에 함수를 삭제
    {
        Debug.Log("공격 첫번째 애니메이션이 실행되었다.");
        
    }
}
