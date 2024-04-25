using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    //�������̽��� ����ϴ� ����
    //1.������ �ʿ��� Ŭ������ ISaveManger�� ��ӽ��� �� ���� ������ �� ����
    //2.���� �̸��� �Լ��� ����Ͽ� �� Ŭ�������� �ٸ� ������� �����͸� ����Ǵ� �ε� �� �� �ִ�.
    public void SaveData(ref GameData gameData);
    public void LoadData(GameData gameData);
}
