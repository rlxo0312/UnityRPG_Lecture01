using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUiManager : MonoBehaviour
{
    [SerializeField] private GameObject UiGameObject;
    
    
    private bool IsOpen; //true �̸� uimanager�� ȣ�� false�� ����
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //ESC ��ư�� ������ �� Setting ȭ���� ȣ���ϰ� �ݴ� �ڵ�
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
