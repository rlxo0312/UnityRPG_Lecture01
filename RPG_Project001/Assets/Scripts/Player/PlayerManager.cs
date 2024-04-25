using CameraSetting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    [Header("Common Player Data")]
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [Header("플레이어 제약 조건")]
    public bool IsPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool canCombo = false;

    [Header("플레이어 매니저 스크립트")]
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public PlayerMoveMentManager playerMovementManager;
    [HideInInspector] public PlayerAudioManager playerAudioManager;
    private void Awake()
    {
        playerAnimationManager = GetComponent<PlayerAnimationManager>();  
        playerMovementManager = GetComponent<PlayerMoveMentManager>();  
        characterController = GetComponent<CharacterController>();
        playerAudioManager = GetComponent<PlayerAudioManager>();
        animator = GetComponent<Animator>();
    }
    
    public void SaveData(ref GameData gameData) //GameData 클래스의 플레이어의 현재 좌표를 저장
    {
        gameData.x = transform.position.x;
        gameData.y = transform.position.y;  
        gameData.z = transform.position.z; 
    }
    public void LoadData(GameData gameData) //GameData 클래스에 저장된 정보를 플레이어 데이터로 호출
    {
        Vector3 loadPlayerPos = new Vector3(gameData.x, gameData.y, gameData.z);

        transform.position = loadPlayerPos;
    }
}
