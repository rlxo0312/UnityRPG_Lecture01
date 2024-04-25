using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public Button ComfilmButton;
    public Button BackButton; 
    public Button ReturnToRoddy;
    [SerializeField] private GamePlayUiManager gamePlayUiManager;

    public void Awake()
    {
        BackButton.onClick.AddListener(() => gamePlayUiManager.CloseUiManager());
        ReturnToRoddy.onClick.AddListener(() => gamePlayUiManager.RetrunToTitleScene());
        ComfilmButton.onClick.AddListener(() => SaveManager.Instance.SaveGame());
    }
    
}
