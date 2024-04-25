using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum SaveGameSlot //�ϳ��ϳ��� ���ڿ���� ����
{
    _00,
    _01,
    _02,
    _03,
    No_slot
}


public class SaveSlot : MonoBehaviour
{
    [Header("���� ���� ����")]
    public SaveGameSlot saveGameSlot;
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playTime;
    private string userName;

    [Header("�÷��� �ð� ����")]
    private float timeValue;
    private int min;
    private int sec;

    [Header("������ �ڵ鷯")]
    private DataHandler dataHandler;
    private GameData gameData;

    private void OnEnable() 
    {
        //Load ��ư �׷��� Ȱ��ȭ ���� �� ���� ��Ű�� �Լ�
        //������ �ڵ鷯�� ����Ͽ� �ܺο� ������ ������ �����Ϸ��� �����͸� �����ϰ�, �׷��� ������ �⺻ ���� ����Ѵ�.
        LoadSaveSlotData();
    }

    private void LoadSaveSlotData() //������ ������ ������ �а� ����Ѵ�.
    {
        if (!SaveManager.Instance) 
        {
            return;
        }
        string mySlotName = SaveManager.Instance.originName + saveGameSlot.ToString() + ".txt";
        Debug.Log(mySlotName);
        dataHandler = new DataHandler(Application.persistentDataPath, mySlotName); //savaGameSlot ������ ���� ���� ����Ǵ� ���� �̸��� �����

        if (dataHandler.CheckFileExists(Application.persistentDataPath,mySlotName))
        {
            //�ش� �����Ϳ� �ִ� gameData�� gameData�� �����ϰ� �����͸� ��������ش�.
            gameData = dataHandler.DataLoad();
            LoadData();
        }
    }
    private void LoadData()
    {
        playerName.text = gameData.playerName;
        if(userName == "")
        {
            playerName.text = "�̸� ����";
        }
        timeValue = gameData.timeValue;

        min = (int)timeValue / 60;
        sec = (int)(timeValue - min) % 60 ;

        playTime.text = string.Format("{0:00} : {1:00}", min, sec);
    }
    private void Reset()
    {
        playerName = transform.Find("Character Name").GetComponent<TMP_Text>();
        playTime = transform.Find("Play Time Text").GetComponent<TMP_Text>();
        
    }
    public void LoadGameData()
    {
        SaveManager.Instance.ChangeSaveFileNameBySelectSlot(saveGameSlot);
        //���� �ε� �ϴ� ���
        LoadingUi.LoadScene("MakeCameraSettingScene");
    }
    public void DeleteGameData()
    {
        dataHandler.DataDelete();
        playerName.text = "No Data";
        playTime.text = "00 : 00";
        gameObject.SetActive(false);
    }
}
