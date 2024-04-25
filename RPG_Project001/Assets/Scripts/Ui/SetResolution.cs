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

    [Header("�ػ󵵸� ������ �ݷ���")] 
    List<Resolution> resolutions= new List<Resolution>();
    int currentResolutionNum; //resolutionDropDown���� ���õ� Value�� �����ϴ� ���� �̰��� ����� �����κ��� �ػ󵵸� ȣ���Ѵ�.

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

            //���� ����� ���� ������ �ҷ� ���� �� ����� �ɼ����� ���ڸ� ���� ����
            if (resolution.width == Screen.width && resolution.height == Screen.height)
                resolutionDropdown.options.Add(option);

            optionNum++; //�ɼ� �ѹ��� ���� �� ���� ��� �ݺ��ؼ� �����ִ� ����
        }

        resolutionDropdown.RefreshShownValue(); //resolutionDropdown�� �������� ���� �ʱ�ȭ�� �Ǿ� ���������� ��

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
        //isOn - üũ�� �Ǿ����� �� Ʈ��(FullScreenMode.FullScreenWindow ) ��ȯ
    }
    public void ChangeResolution()
    {
        Screen.SetResolution(resolutions[currentResolutionNum].width, resolutions[currentResolutionNum].height,
                             screenMode);
    }
}
