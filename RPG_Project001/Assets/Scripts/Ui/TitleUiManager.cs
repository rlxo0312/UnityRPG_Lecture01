using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TitleUiManager : MonoBehaviour
{
    public void GameStart()
    {
        //저장 Application.persistantDataPath 경로에 데이터를 List화 해서 maxCount를 지정해야함
        //00~마지막 번호 탐색을, 비어 있는 Slot이 있으면 해당 Slot 버튼의 이름으로 변경
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
