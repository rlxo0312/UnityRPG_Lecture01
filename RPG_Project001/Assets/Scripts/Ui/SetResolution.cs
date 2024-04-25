using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetResolution : MonoBehaviour
{
    FullScreenMode screenMode;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle FullScreenBtn;

    [Header("해상도를 저장할 콜렉션")] 
    List<Resolution> resolutions= new List<Resolution>();
    int currentResolutionNum; //resolutionDropDown에서 선택된 Value를 저장하는 변수 이곳에 저장된 변수로부터 해상도를 호출한다.

    private void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        for(int i = 0; i< Screen.resolutions.Length; i++)
        {
            resolutions.Add(Screen.resolutions[i]);
        }
        resolutionDropdown.options.Clear(); 
        int optionNum = 0;
        foreach(var resolution in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = resolution.width + "x" + resolution.height + " " + resolution.refreshRateRatio + "HZ";

            resolutionDropdown.options.Add(option);

            //설정 변경시 다음 설정을 불러 왔을 때 변경된 옵션으로 숫자를 띄우는 구문
            if (resolution.width == Screen.width && resolution.height == Screen.height)
                resolutionDropdown.options.Add(option);

            optionNum++; //옵션 넘버가 같을 때 까지 계속 반복해서 더해주는 구문
        }

        resolutionDropdown.RefreshShownValue(); //resolutionDropdown의 보여지는 값이 초기화가 되어 보여지도록 함

        FullScreenBtn.isOn = 
            Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
        
    }

    public void DropBoxOptionChange()
    {
        currentResolutionNum = resolutionDropdown.value;
    }
    public void FullScreenButton()
    {
        screenMode = FullScreenBtn.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        //isOn - 체크가 되어있을 떄 트루(FullScreenMode.FullScreenWindow ) 반환
    }
    public void ChangeResolution()
    {
        Screen.SetResolution(resolutions[currentResolutionNum].width, resolutions[currentResolutionNum].height,
                             screenMode);
    }
}
