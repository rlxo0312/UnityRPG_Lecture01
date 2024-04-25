using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class DataHandler 
{
    private string directoryPath = "";
    private string fileName = "";

    public DataHandler(string directoryPath, string fileName)
    {
        this.directoryPath = directoryPath;
        this.fileName = fileName;
    }

    public void DataSave(GameData _data)
    {
        string dataPath = Path.Combine(directoryPath, fileName);

        try
        {
            Debug.Log(dataPath);

            Directory.CreateDirectory(Path.GetDirectoryName(dataPath)); //빈 파일만 생성됨 데이터를 넣어줘야한다.
            string serializeData = JsonUtility.ToJson(_data, true); //true : 들여쓰기 + 줄바꿈이 된 상태로 json을 저장

            using(FileStream stream = new FileStream(dataPath, FileMode.Create)) //유니티 데이터를 직렬화, 역직렬화를 하기위해 만든 데이터중 가장 성능이 뛰어남
            {                                                                    //using문 사용이유 - stream을 연결시키는 클래스는 사용이 끝난 후 close()를 해줘야
                                                                                 //                   하는데 using문을 사용하면 사용이 끝나면 자동으로 닫아줌 
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(serializeData);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("에러 발생" + dataPath + "\n" + e);
        }
    }
    public GameData DataLoad()
    {
        string dataPath = Path.Combine(directoryPath, fileName);
        GameData loadData = null;

        if (File.Exists(dataPath)) //해당 경로에 파일이 없으면 null을 반환
        {
            try
            {
                string dataToLoad = "";

                using(FileStream stream = new FileStream(dataPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); //데이터를 저장하는 스트림 값에 하나씩 다 불러오기 
                    }
                }
                loadData = JsonUtility.FromJson<GameData>(dataToLoad); //다 읽으면 loadData에 GameData형식으로 stream값을 역직렬화 
            }
            catch (Exception e) 
            {
                Debug.LogError("에러 발생" + dataPath + "\n" + e);
            }
        }

        return loadData;
    }
    public void DataDelete()
    {
        string delePath = Path.Combine(directoryPath, fileName);
        if(File.Exists(delePath))
            File.Delete(delePath);
    }

    public bool CheckFileExists(string dir, string file)
    {
        if(File.Exists(Path.Combine(dir, file)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

