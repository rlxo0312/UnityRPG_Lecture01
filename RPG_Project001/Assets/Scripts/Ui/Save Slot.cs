using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum SaveGameSlot //하나하나의 문자열들로 구성
{
    _00,
    _01,
    _02,
    _03,
    No_slot
}


public class SaveSlot : MonoBehaviour
{
    [Header("저장 슬롯 정보")]
    public SaveGameSlot saveGameSlot;
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playTime;
    private string userName;

    [Header("플레이 시간 설정")]
    private float timeValue;
    private int min;
    private int sec;

    [Header("데이터 핸들러")]
    private DataHandler dataHandler;
    private GameData gameData;

    private void OnEnable() 
    {
        //Load 버튼 그룹이 활성화 됬을 때 실행 시키는 함수
        //데이터 핸들러를 사용하여 외부에 저장한 파일이 존재하려면 데이터르 갱신하고, 그렇지 않으면 기본 값을 출력한다.
        LoadSaveSlotData();
    }

    private void LoadSaveSlotData() //슬롯의 데이터 정보를 읽고 출력한다.
    {
        if (!SaveManager.Instance) 
        {
            return;
        }
        string mySlotName = SaveManager.Instance.originName + saveGameSlot.ToString() + ".txt";
        Debug.Log(mySlotName);
        dataHandler = new DataHandler(Application.persistentDataPath, mySlotName); //savaGameSlot 열거형 값에 따라 저장되는 파일 이름이 변경됨

        if (dataHandler.CheckFileExists(Application.persistentDataPath,mySlotName))
        {
            //해당 데이터에 있는 gameData를 gameData에 저장하고 데이터를 적용시켜준다.
            gameData = dataHandler.DataLoad();
            LoadData();
        }
    }
    private void LoadData()
    {
        playerName.text = gameData.playerName;
        if(userName == "")
        {
            playerName.text = "이름 없음";
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
        //씬을 로드 하는 기능
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
