using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : SingletonMonoBehaviour<SaveManager>
{
    //public static SaveManager Instance; //static���� �����ϸ� Ŭ������ü���� ���ٰ���
                                        //��Ŭ�� ������ ���� �ν��Ͻ� ����
                                        //Singleton Patern : �ν��Ͻ��� ���� ��� �ν��Ͻ��� �����ϰ�, �̹� ������ ��� �ν��Ͻ��� ��ȯ
    
    GameData gameData;         //�÷��̾��� ������ ������ ����
    DataHandler dataHandler;
    List<ISaveManager> saveManagers;  //���̾��Űâ�� �ִ� ISaveManager�� ����ϴ� Ŭ������ ������ ����Ʈ
    [Header("������ ������ ���� ����")]
    public string fileName;
    public string originName;
    public SaveGameSlot nowSlot; //���� ���õ� ������ �����ϴ� ����
    //PlayerManager�� �޾ƿ��� ����Ƽâ�� �����Ű�� ���
    //public PlayerManager playerManager; //�̹���� ������� �ʴ� ����: ������ �ʿ��� ��� Ŭ������ ���δ� �ϳ��� �߰��ؼ� ����ؾ� ��
    protected override void Awake() 
    {/*
        if(Instance == null)  //���� �����Ͱ� �ΰ� ���� �� ���� �����͸� �ι� ó���� �ϱ� ������ �ϳ��� �����ϰ� ���ִ� �ڵ�
        {
            Instance = this; //���� ��� �ڱ� �ڽ��� ����
        }
        else
        {
            Destroy(gameObject);
            //Debug.Log(Instance.gameObject);
        }
        DontDestroyOnLoad(gameObject); //���� ����Ǿ �ı����� �ʵ��� ���ִ� ���� */
        base.Awake();
        originName = fileName;
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //�ݹ����� ȣ���ϰ� �Լ��� ����Ѵ�.
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //�Ű������� scene�� scene ȣ�� ����� �Ű������� �ۼ��������
    {
        dataHandler = new DataHandler(Application.persistentDataPath, fileName); //����: �÷����� ������� �����͸� ���� ������ �� �ִ�.
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }
    /*private void Start()
    {
          //����: Ư�� �÷����� ��쿡�� ����� �����͸� Ȯ���� �� ����.
        dataHandler = new DataHandler(Application.persistentDataPath, fileName); //����: �÷����� ������� �����͸� ���� ������ �� �ִ�.
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }*/
    public void ChangeSaveFileNameBySelectSlot(SaveGameSlot slot) //SaveSlot Ŭ�������� ȣ���ϴ� �Լ�
    {
        nowSlot = slot;
        fileName = originName;

        /* switch (nowSlot) //�������� ���� �ʾ��� ���
         {
             case SaveGameSlot._00:
                 fileName = "SavaData_00.json";
                 break;
             case SaveGameSlot._01:
                 fileName = "SavaData_01.json";
                 break;
             case SaveGameSlot._02:
                 fileName = "SavaData_02.json";
                 break;
             case SaveGameSlot._03:
                 fileName = "SavaData_03.json";
                 break;
             case SaveGameSlot.No_slot:
                 fileName = "SavaData.json";
                 break;
         }*/
    }
    public void SaveGame()
    {
        //1. ������ �����͸� �� �Լ����� ȣ���� �ڿ� gameData�� �����Ѵ�. 
        foreach(var saveManager in saveManagers)
        {           //saveManager���� ISaveManager(interface)�� ��� ���� �׷��� SaveData�� LoadData�� ������ ����
            saveManager.SaveData(ref gameData);
        }
        //2. ������ gameData�� �ܺ� ������ �����Ѵ�. 
        dataHandler.DataSave(gameData);
        Debug.Log("������ ����Ǿ����ϴ�.");
    }
    public void LoadGame()
    {

        //�ܺο� ����� ���� ������ ������ �ҷ��´�. 
        gameData = dataHandler.DataLoad(); //dataHandler.DataLoad() �Լ��� ȣ���Ͽ� �����Ͱ� ������ �ش� �����͸� gameData �������� ��ȯ�ϰ�, ������ null�� ��ȯ 
        if(gameData == null) //���� �ܺο� ���� �����Ͱ� ���ٸ� ���ο� ������ ȣ�� 
        {
            NewGame();
        }
        //���� �ܺο� ���� �����Ͱ� ���ٸ� ���ο� ������ ȣ���Ѵ�. 

        //�������� �ܺ��� �����͸� ������ ���� �ε��ϴ� ����
        //------------------------------------------------------------------------------------------
        //���� �Ʒ��� �ٸ� Ŭ������ �ִ� �����Ϳ� �� ���ӵ����Ϳ� �ִ� ������ �ѹ��� �־��ִ� ����
        //GameData Ŭ������ �ִ� �����͸� ���ӿ� �ʿ��� Ŭ������ ���� �����͸� �������ش�.
        Debug.Log("������ �ҷ� �ɴϴ�.");
        foreach(var saveManager in saveManagers) 
        {
            saveManager.LoadData(gameData); 
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        //MonoBehaviour�� ������ �ִ� Ÿ���� ISaveManager�� �ִ� Ÿ�Ը� ����  
        //FindObjectsOfType - ������ ������ FindObjectOfType - �ܼ��� ������
        return new List<ISaveManager>(saveManagers);
    }
}
