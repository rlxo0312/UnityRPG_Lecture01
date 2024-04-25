using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSetting
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMoveMentManager : MonoBehaviour
    {
        PlayerManager player;
        [Header("�÷��̾� �Ŵ��� ��ũ��Ʈ")]
        [HideInInspector] public PlayerAnimationManager animatorManager;
        [Header("�÷��̾� �Է� ���� ����")]
        [SerializeField] private float moveSpeed; //�÷��̾��� �⺻�ӵ�
        [SerializeField] private float runSpeed;  //�÷��̾ �޸� ���� �ӵ� 
        [SerializeField] private float jumpForce; //�÷��̾��� ������ 
        //private Rigidbody playerRb;
        private CharacterController cCon;   //RigidBody ��� Character�� ���� �浹 ��ɰ� �̵��� ���� ������Ʈ

        [Header("ī�޶� ���� ����")]
        [SerializeField] ThirdCameraContrioller thirdCamera; //3��Ī ī�޶� ��Ʈ�ѷ��� �ִ� ���� ������Ʈ�� ���������� �Ѵ�.
        [SerializeField] float smoothRotation = 100f; //ī�޶��� �ڿ������� ȸ���� ���� ����ġ
        Quaternion targetRotation; //Ű���� �Է��� ���� �ʾ��� �� ī�޶� �������� ȸ���ϱ� ���ؼ� ȸ�� ������ �����ϴ� ����

        [Header("���� ���� ����")]
        [SerializeField] private float gravityModifier = 3f; //�÷��̾ ���� �������� �ӵ��� ������ ���� 
        [SerializeField] private Vector3 groundCheckPoint; //���� �Ǻ��ϱ� ���� üũ ����Ʈ 
        [SerializeField] private float groundCheckRadius; //�� üũ�ϴ� ���� ũ�� ������
        [SerializeField] private LayerMask groundLayer;  //üũ�� ���̾ ������ �Ǻ��ϴ� ����
        private bool isGrounded;  //true�̸� ��������. false�̸� ���� ����


        private float activeMoveSpeed; //������ �÷��̾ �̵��� �ӷ��� ������ ����
        private Vector3 moveMent; //�÷��̾ �����̴� ����� �Ÿ��� ���Ե� ���� vector�� 
        [Header("�ִϸ�����")]
        private Animator playerAnimaitor;
        

        private void Awake()
        {
               player = GetComponent<PlayerManager>();
        }
        // Start is called before the first frame update
        void Start()
        {
            cCon = GetComponent<CharacterController>();
            playerAnimaitor = GetComponentInChildren<Animator>();
            cCon.enabled = true;
        } 

        // Update is called once per frame ��ǻ�Ͱ� ���� ���� frame�� ���� �����ǰ� Update�� ���� ȣǮ �ȴ�
        void Update()
        {
            HandleMovement();
            HandleComboAttack();
            HandleActionInput();
        }

        private void GroundCheck() //�÷��̾ ������ �ƴ��� �Ǻ��ϴ� �Լ�
        {
            isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius, groundLayer); //���̾ ground�� groundCheckRadius�̰� ��ġ ��������
            playerAnimaitor.SetBool("IsGround", isGrounded);
                                                                                                       //checkPoint�� ���� �浹�� �߻��ϸ� true, false
        }
        private void OnDrawGizmos() //���� ������ �ʴ� ��üũ �Լ��� ����ȭ �ϱ� ���� ����
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius);
        }
        private void HandleMovement() 
        {
            if (player.IsPerformingAction) return;

            //1. Input Ŭ������ �̿��Ͽ� Ű���� �Է��� ���� 

            float horzontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            /*playerAnimaitor.SetFloat("Horizontal", horzontal, 0.2f, Time.deltaTime);
            playerAnimaitor.SetFloat("Vertical", vertical, 0.2f, Time.deltaTime);*/ //2d freeform directional ����
            //2. Ű���� input�� �Է� ���� Ȯ���ϱ� ���� ���� ����
            Vector3 moveInput = new Vector3(horzontal, 0, vertical).normalized; //Ű���� �Է°��� �����ϴ� ����

            //RigidBody �ʱ�ȭ�� ������ ����ϰ� AddForce �޼ҵ带 �̿��� �÷��̾ �����̱�
            //playerRb.AddForce(moveDir * moveSpeed * Time.deltaTime);
            //���������� ����

            //������ġ + �ӵ� * ���ӵ� = �̵��ϴ� �Ÿ� 
            //transform.position += moveDir * moveSpeed; // �̵��Ÿ���ŭ �� �����Ӹ��� ������ 
            //transform.position = ��ũ��Ʈ�� ������ �ִ�x ,y ,z�� ��ǥ�� ����� ����ִ��� 

            //transform.position += moveDir * moveSpeed * Time.deltaTime;
            //���������� ���� ���� 
            //�̵��Ÿ���Ū (�� �����Ӹ��� �����δ� * TIme,deltaTime)  => �����Ӽ��� ������� ���� �ð� ���� �Ÿ��� ������

            //CharterControll�� �������� �����ϴ� �ڵ� 
            //cCon.Move( moveDir * moveSpeed * Time.deltaTime);
            //SimpleMove�� Move���� Time,deltaTime�� �� ��ġ�� �Է��ϸ� ��
            //cCon.SimpleMove(moveDir * moveSpeed); 

            //Vector3 firstMovement = transform.forward * moveDir.z + transform.right * moveDir.x;

            float moveAmount = Mathf.Clamp01(Mathf.Abs(horzontal) + Mathf.Abs(vertical)); //Ű����� �����¿� Ű �Ѱ��� �Է��� �ϸ� 0���� ū ���� moveAmount�� ����

            //3. �÷��̾� ĳ���Ͱ� �̵��� ������ ������ ���� ���� 
            //Vector3 moveDirection = thirdCamera.camLookRotation * moveInput; //�÷��̾ �̵��� ������ �����ϴ� ���� moveDirection
            Vector3 moveDirection = thirdCamera.transform.forward * moveInput.z + thirdCamera.transform.right * moveInput.x;
            moveDirection.y = 0;

            //4.�÷��̾��� �̵��ӵ��� �ٸ��� ���ִ� �ڵ� (�޸��� ���) - 
            if (Input.GetKey(KeyCode.LeftShift)) // key down : ���� �� �ѹ�  key : key ��ư�� ���� ������ ��� 
            {
                activeMoveSpeed = runSpeed;
                moveAmount++;
                playerAnimaitor.SetBool("IsRun", true);
            }
            else
            {
                activeMoveSpeed = moveSpeed;
                playerAnimaitor.SetBool("IsRun", false);
            }

            //5.������ �ϱ� ���� ���� - �߷°�� �ʿ�

            float yValue = moveMent.y; //�߷��� ó�� �޾��� ���� �ӷ��� ���� �����, �������� �ִ� y�� ũ�⸦ ����

            moveMent = moveDirection * activeMoveSpeed; //��ǥ�� �̵��� x, 0 ,z ���� ���� ����, ���ڵ带 ������ y�������� �ִ� ���� 0���� �ʱ�ȭ 

            moveMent.y = yValue; //�߷¿� ���� ��� �޵��� �Ҿ���� ������ �ٽ� �ҷ��´�.

            //���� ������ �Ǵ� �������� �߻� -> ���� ���°� ������ �������� �ƴ��� �Ǵ��� �ʿ䰡 �ִ�. 
            GroundCheck();

            if (cCon.isGrounded)
            {
                moveMent.y = 0; //������ �����϶� y�� -�� ������ �����
                //Debug.Log("���� �÷��̾ ���� �ִ� �����Դϴ�.");
            }

            //����Ű�� �Է��Ͽ� ���� ���� 
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                playerAnimaitor.CrossFade("Jump", 0.2f); //�� ��° �Ű������� : ���� State���� �����ϰ� ���� �ִϸ��̼��� �ڵ����� Blend���ִ� �ð�
                                                         //Jump�� Idle�� ������ ������ 
                moveMent.y = jumpForce;
                //���� ������� �� �ڵ�
            }

            moveMent.y += Physics.gravity.y * gravityModifier * Time.deltaTime;


            //6. CharacterController�� ����Ͽ� ĳ���͸� �����δ�. 
            if (moveAmount > 0)//moveDir = 0�� �� moveMent�� 0�� �ȴ�.
            {
                targetRotation = Quaternion.LookRotation(moveDirection);
                //playerAnimaitor.SetBool("IsRun", true);
                player.playerAudioManager.playFootStepSFX();
            }
            else
            {
                player.playerAudioManager.playAllStop();
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smoothRotation); //Tine.deltaTime�� ������ ������ ������
                                                                                                               //smoothRotation�� ���� �۰Ը� ������� ���ƿ��� �ӵ��� ��������
            cCon.Move(moveMent * Time.deltaTime);
            playerAnimaitor.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
            //dampTime: 1��° ����(���� ��), 2��° ����(��ȭ ��Ű�� ���� ��)
        }
        private void HandleActionInput()
        {
            if (Input.GetMouseButtonDown(0)) // ���� ���콺 Ŭ���� �����
            {
                HandleAttackAction();
            }
        }
        
        private void HandleAttackAction()
        {
            player.playerAnimationManager.PlayerTargetActionAnimation("ATK0",true);
            player.canCombo = true; //canCombo�� true �ϋ��� �޺� ������ ����� ����
        }
        
        private void HandleComboAttack()
        {
            if (!player.canCombo) return; //player�� �޺����°� �ƴ϶�� return�϶�

            //�޺� ��Ĵ�� ����� �Է� Ű ���� 

            if (Input.GetMouseButtonDown(0))//1�� ���콺 ������
            {
                player.animator.SetTrigger("DoAttack");
            }


        }
    }

}