using CameraSetting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    [Header("Common Player Data")]
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [Header("�÷��̾� ���� ����")]
    public bool IsPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool canCombo = false;

    [Header("�÷��̾� �Ŵ��� ��ũ��Ʈ")]
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
    
    public void SaveData(ref GameData gameData) //GameData Ŭ������ �÷��̾��� ���� ��ǥ�� ����
    {
        gameData.x = transform.position.x;
        gameData.y = transform.position.y;  
        gameData.z = transform.position.z; 
    }
    public void LoadData(GameData gameData) //GameData Ŭ������ ����� ������ �÷��̾� �����ͷ� ȣ��
    {
        Vector3 loadPlayerPos = new Vector3(gameData.x, gameData.y, gameData.z);

        transform.position = loadPlayerPos;
    }
}
