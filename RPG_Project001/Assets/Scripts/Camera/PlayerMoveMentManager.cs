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
        [Header("플레이어 매니저 스크립트")]
        [HideInInspector] public PlayerAnimationManager animatorManager;
        [Header("플레이어 입력 제어 변수")]
        [SerializeField] private float moveSpeed; //플레이어의 기본속도
        [SerializeField] private float runSpeed;  //플레이어가 달릴 때의 속도 
        [SerializeField] private float jumpForce; //플레이어의 점프력 
        //private Rigidbody playerRb;
        private CharacterController cCon;   //RigidBody 대신 Character에 물리 충돌 기능과 이동을 위한 컴포넌트

        [Header("카메라 제어 변수")]
        [SerializeField] ThirdCameraContrioller thirdCamera; //3인칭 카메라 컨트롤러가 있는 게임 오브젝트를 연결시켜줘야 한다.
        [SerializeField] float smoothRotation = 100f; //카메라의 자연스러운 회전을 위한 가중치
        Quaternion targetRotation; //키보드 입력을 하지 않았을 때 카메라 방향으로 회전하기 위해서 회전 각도를 저장하는 변수

        [Header("점프 제어 변수")]
        [SerializeField] private float gravityModifier = 3f; //플레이어가 땅에 떨어지는 속도를 제어할 변수 
        [SerializeField] private Vector3 groundCheckPoint; //땅을 판별하기 위한 체크 포인트 
        [SerializeField] private float groundCheckRadius; //땅 체크하는 구의 크기 반지름
        [SerializeField] private LayerMask groundLayer;  //체크할 레이어가 땅인지 판별하는 변수
        private bool isGrounded;  //true이면 점프가능. false이면 점프 제한


        private float activeMoveSpeed; //실제로 플레이어가 이동할 속력을 저장할 변수
        private Vector3 moveMent; //플레이어가 움직이는 방향과 거리가 포함된 최종 vector값 
        [Header("애니메이터")]
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

        // Update is called once per frame 컴퓨터가 좋을 수록 frame이 많이 생성되고 Update가 많이 호풀 된다
        void Update()
        {
            HandleMovement();
            HandleComboAttack();
            HandleActionInput();
        }

        private void GroundCheck() //플레이어가 땅인지 아닌지 판별하는 함수
        {
            isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius, groundLayer); //레이어가 ground인 groundCheckRadius이고 위치 시작점이
            playerAnimaitor.SetBool("IsGround", isGrounded);
                                                                                                       //checkPoint인 물리 충돌이 발생하면 true, false
        }
        private void OnDrawGizmos() //눈에 보이지 않는 땅체크 함수를 가시화 하기 위해 선언
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius);
        }
        private void HandleMovement() 
        {
            if (player.IsPerformingAction) return;

            //1. Input 클래스를 이용하여 키보드 입력을 제어 

            float horzontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            /*playerAnimaitor.SetFloat("Horizontal", horzontal, 0.2f, Time.deltaTime);
            playerAnimaitor.SetFloat("Vertical", vertical, 0.2f, Time.deltaTime);*/ //2d freeform directional 관련
            //2. 키보드 input과 입력 값을 확인하기 위한 변수 선언
            Vector3 moveInput = new Vector3(horzontal, 0, vertical).normalized; //키보드 입력값을 저장하는 벡터

            //RigidBody 초기화한 변수를 사용하고 AddForce 메소드를 이용해 플레이어를 움직이기
            //playerRb.AddForce(moveDir * moveSpeed * Time.deltaTime);
            //물리현상을 받음

            //현재위치 + 속도 * 가속도 = 이동하는 거리 
            //transform.position += moveDir * moveSpeed; // 이동거리만큼 매 프레임마다 움직임 
            //transform.position = 스크립트가 가지고 있는x ,y ,z의 좌표가 월드상에 어디있는지 

            //transform.position += moveDir * moveSpeed * Time.deltaTime;
            //물리현상을 받지 않음 
            //이동거리만큭 (매 프레임마다 움직인다 * TIme,deltaTime)  => 프레임수와 상관없이 같은 시간 같은 거리를 움직임

            //CharterControll로 움직임을 제어하는 코드 
            //cCon.Move( moveDir * moveSpeed * Time.deltaTime);
            //SimpleMove는 Move에서 Time,deltaTime을 뺀 수치를 입력하면 됨
            //cCon.SimpleMove(moveDir * moveSpeed); 

            //Vector3 firstMovement = transform.forward * moveDir.z + transform.right * moveDir.x;

            float moveAmount = Mathf.Clamp01(Mathf.Abs(horzontal) + Mathf.Abs(vertical)); //키보드로 상하좌우 키 한개만 입력을 하면 0보다 큰 값을 moveAmount에 저장

            //3. 플레이어 캐릭터가 이동할 방향을 지정할 변수 선언 
            //Vector3 moveDirection = thirdCamera.camLookRotation * moveInput; //플레이어가 이동할 방향을 저장하는 변수 moveDirection
            Vector3 moveDirection = thirdCamera.transform.forward * moveInput.z + thirdCamera.transform.right * moveInput.x;
            moveDirection.y = 0;

            //4.플레이어의 이동속도를 다르게 해주는 코드 (달리기 기능) - 
            if (Input.GetKey(KeyCode.LeftShift)) // key down : 누를 때 한번  key : key 버튼을 떼기 전까지 계속 
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

            //5.점프를 하기 위한 계산식 - 중력계산 필요

            float yValue = moveMent.y; //중력을 처음 받았을 때의 속력이 여기 저장됨, 떨어지고 있는 y의 크기를 저장

            moveMent = moveDirection * activeMoveSpeed; //좌표에 이동할 x, 0 ,z 벡터 값을 저장, 이코드를 만나면 y떨어지고 있는 값이 0으로 초기화 

            moveMent.y = yValue; //중력에 힘이 계속 받도록 잃어버린 변수를 다시 불러온다.

            //다중 점프가 되는 문제점이 발생 -> 현재 상태가 공중인 상태인지 아닌지 판단할 필요가 있다. 
            GroundCheck();

            if (cCon.isGrounded)
            {
                moveMent.y = 0; //공증인 상태일때 y가 -인 값으로 저장됨
                //Debug.Log("현재 플레이어가 땅에 있는 상태입니다.");
            }

            //점프키를 입력하여 점프 구현 
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                playerAnimaitor.CrossFade("Jump", 0.2f); //두 번째 매개변수는 : 현재 State에서 실행하고 싶은 애니메이션을 자동으로 Blend해주는 시간
                                                         //Jump와 Idle의 동작을 맞춰줌 
                moveMent.y = jumpForce;
                //점프 오디오가 들어갈 코드
            }

            moveMent.y += Physics.gravity.y * gravityModifier * Time.deltaTime;


            //6. CharacterController를 사용하여 캐릭터를 움직인다. 
            if (moveAmount > 0)//moveDir = 0일 때 moveMent가 0이 된다.
            {
                targetRotation = Quaternion.LookRotation(moveDirection);
                //playerAnimaitor.SetBool("IsRun", true);
                player.playerAudioManager.playFootStepSFX();
            }
            else
            {
                player.playerAudioManager.playAllStop();
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smoothRotation); //Tine.deltaTime을 곱해줄 이유가 없었음
                                                                                                               //smoothRotation의 값이 작게만 만들어져 돌아오는 속도가 느려졌음
            cCon.Move(moveMent * Time.deltaTime);
            playerAnimaitor.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
            //dampTime: 1번째 변수(이전 값), 2번째 변수(변화 시키고 싶은 값)
        }
        private void HandleActionInput()
        {
            if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 클릭시 적용됨
            {
                HandleAttackAction();
            }
        }
        
        private void HandleAttackAction()
        {
            player.playerAnimationManager.PlayerTargetActionAnimation("ATK0",true);
            player.canCombo = true; //canCombo가 true 일떄만 콤보 공격을 사용이 가능
        }
        
        private void HandleComboAttack()
        {
            if (!player.canCombo) return; //player가 콤보상태가 아니라면 return하라

            //콤보 어캑을 사용할 입력 키 설정 

            if (Input.GetMouseButtonDown(0))//1은 마우스 오른쪽
            {
                player.animator.SetTrigger("DoAttack");
            }


        }
    }

}