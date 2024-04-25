using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TitleUiManager : MonoBehaviour
{
    public void GameStart()
    {
        //���� Application.persistantDataPath ��ο� �����͸� Listȭ �ؼ� maxCount�� �����ؾ���
        //00~������ ��ȣ Ž����, ��� �ִ� Slot�� ������ �ش� Slot ��ư�� �̸����� ����
        LoadingUi.LoadScene("MakeCameraSettingScene");
    }
    public void GameQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
