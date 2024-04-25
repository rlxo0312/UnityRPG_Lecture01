using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUiManager : MonoBehaviour
{
    [SerializeField] private GameObject UiGameObject;
    
    
    private bool IsOpen; //true 이면 uimanager를 호출 false면 닫음
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //ESC 버튼을 눌렀을 떄 Setting 화면을 호출하고 닫는 코드
        {
            IsOpen = !IsOpen;
            CallSettingUi(IsOpen);
        }
        
    }
    private void CallSettingUi(bool isOpen)
    {
        UiGameObject.SetActive(isOpen);
    }
    public void CloseUiManager()
    {
        UiGameObject.SetActive(false);
    }
    public void RetrunToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void GameQuit()
    {

        Application.Quit();
    }
}
