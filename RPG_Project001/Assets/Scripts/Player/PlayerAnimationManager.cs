using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    PlayerManager player;
    [Header("�ִϸ��̼� ���� ����")]
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
    {   //�ִϸ޴ϼ� Ŭ���� �̸��� ȣ���Ͽ� �� palyermanager���� �ִϸ��̼��� ���� ȣ�� �� �� �ְ� ĸ��ȭ �� �Լ�                                                
        animator.CrossFade(targetAnimation, 0.2f);
        player.IsPerformingAction = IsPerformingAction;
        player.applyRootMotion = applayRootmotion;
        player.canRotate = canRotate;
        player.canMove = canMove;   
        
    } 
    public void AnimationTest() //Attack�� �ִ� animation �̺�Ʈ�� ������ �Ŀ� �Լ��� ����
    {
        Debug.Log("���� ù��° �ִϸ��̼��� ����Ǿ���.");
        
    }
}
