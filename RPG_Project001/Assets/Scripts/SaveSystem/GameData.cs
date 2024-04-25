using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    [Header("Player Data")]
    //�÷��̾��� ��ġ ������ x, y, z������ �����Ͽ� ���� 
    public float x;
    public float y;
    public float z;

    public float timeValue; //�÷��� Ÿ���� �����ϴ� ���� 

    public string playerName;

    public SerializableDictionary<string, float> volumeSettings;  //���� ������ Dictionary�� ����

}
