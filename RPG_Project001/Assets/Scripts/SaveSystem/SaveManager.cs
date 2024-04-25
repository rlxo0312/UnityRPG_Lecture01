using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : SingletonMonoBehaviour<SaveManager>
{
    //public static SaveManager Instance; //static으로 선언하면 클래스자체에서 접근가능
                                        //싱클톤 패턴을 위한 인스턴스 변수
                                        //Singleton Patern : 인스턴스가 없을 경우 인스턴스를 생성하고, 이미 존재할 경우 인스턴스를 반환
    
    GameData gameData;         //플레이어의 정보를 저장할 정보
    DataHandler dataHandler;
    List<ISaveManager> saveManagers;  //하이어라키창에 있는 ISaveManager를 상속하는 클래스를 저장할 리스트
    [Header("저장할 데이터 변수 정보")]
    public string fileName;
    public string originName;
    public SaveGameSlot nowSlot; //현재 선택된 슬롯을 저장하는 변수
    //PlayerManager을 받아오고 유니티창에 연결시키는 방식
    //public PlayerManager playerManager; //이방식을 사용하지 않는 이유: 저장이 필요한 모든 클래스를 전부다 하나씩 추가해서 사용해야 함
    protected override void Awake() 
    {/*
        if(Instance == null)  //동일 데이터가 두개 있을 때 같은 데이터를 두번 처리를 하기 떄문에 하나만 존재하게 해주는 코드
        {
            Instance = this; //없을 경우 자기 자신을 넣음
        }
        else
        {
            Destroy(gameObject);
            //Debug.Log(Instance.gameObject);
        }
        DontDestroyOnLoad(gameObject); //씬이 변경되어도 파괴되지 않도록 해주는 설정 */
        base.Awake();
        originName = fileName;
    }

    public void NewGame()
    {
        gameData = new GameData();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //콜백으로 호출하게 함수를 등록한다.
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //매개변수로 scene과 scene 호출 방식을 매개변수로 작성해줘야함
    {
        dataHandler = new DataHandler(Application.persistentDataPath, fileName); //장점: 플랫폼에 상관없이 데이터를 쉽게 저장할 수 있다.
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }
    /*private void Start()
    {
          //단점: 특정 플랫폼의 경우에는 저장된 데이터를 확인할 수 없다.
        dataHandler = new DataHandler(Application.persistentDataPath, fileName); //장점: 플랫폼에 상관없이 데이터를 쉽게 저장할 수 있다.
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }*/
    public void ChangeSaveFileNameBySelectSlot(SaveGameSlot slot) //SaveSlot 클래스에서 호출하는 함수
    {
        nowSlot = slot;
        fileName = originName;

        /* switch (nowSlot) //열거형을 쓰지 않았을 경우
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
        //1. 저장할 데이터를 이 함수에서 호출한 뒤에 gameData에 저장한다. 
        foreach(var saveManager in saveManagers)
        {           //saveManager들은 ISaveManager(interface)을 들고 있음 그래서 SaveData와 LoadData를 가지고 있음
            saveManager.SaveData(ref gameData);
        }
        //2. 저장할 gameData를 외부 폴더에 저장한다. 
        dataHandler.DataSave(gameData);
        Debug.Log("게임임 저장되었습니다.");
    }
    public void LoadGame()
    {

        //외부에 저장된 게임 데이터 파일을 불러온다. 
        gameData = dataHandler.DataLoad(); //dataHandler.DataLoad() 함수를 호출하여 데이터가 있으면 해당 데이터를 gameData 형식으로 반환하고, 없으면 null을 반환 
        if(gameData == null) //만약 외부에 게임 데이터가 없다면 새로운 게임을 호출 
        {
            NewGame();
        }
        //만약 외부에 게임 데이터가 없다면 새로운 게임을 호출한다. 

        //점선위로 외부의 데이터를 저장한 것을 로드하는 내용
        //------------------------------------------------------------------------------------------
        //점선 아래로 다른 클래스에 있는 데이터에 이 게임데이터에 있는 내용을 한번에 넣어주는 내용
        //GameData 클래스에 있는 데이터를 게임에 필요한 클래스에 각각 데이터를 전달해준다.
        Debug.Log("게임을 불러 옵니다.");
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
        //MonoBehaviour가 가지고 있는 타입중 ISaveManager가 있는 타입만 저장  
        //FindObjectsOfType - 복수의 데이터 FindObjectOfType - 단수의 데이터
        return new List<ISaveManager>(saveManagers);
    }
}
