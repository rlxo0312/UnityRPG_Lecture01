using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUi : MonoBehaviour, ISaveManager
{
    [SerializeField] TMP_Text timeText;

    private float timeValue;
    private int min;
    private int sec;

    private void Update()
    {
        //�ð��� ���� ��Ű�� text�� ��½�Ű�� ���
        SetTimeUi();
    }
    private void SetTimeUi()
    {
        timeValue += Time.deltaTime;

        min = (int)timeValue / 60;
        sec = ((int)(timeValue - min) % 60);

        timeText.text = string.Format("{0:00} : {1:00}", min, sec);
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.timeValue = timeValue;
    }

    public void LoadData(GameData gameData)
    {
        timeValue = gameData.timeValue;
    }

    
}
