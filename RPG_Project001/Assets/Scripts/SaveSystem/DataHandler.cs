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

            Directory.CreateDirectory(Path.GetDirectoryName(dataPath)); //�� ���ϸ� ������ �����͸� �־�����Ѵ�.
            string serializeData = JsonUtility.ToJson(_data, true); //true : �鿩���� + �ٹٲ��� �� ���·� json�� ����

            using(FileStream stream = new FileStream(dataPath, FileMode.Create)) //����Ƽ �����͸� ����ȭ, ������ȭ�� �ϱ����� ���� �������� ���� ������ �پ
            {                                                                    //using�� ������� - stream�� �����Ű�� Ŭ������ ����� ���� �� close()�� �����
                                                                                 //                   �ϴµ� using���� ����ϸ� ����� ������ �ڵ����� �ݾ��� 
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(serializeData);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("���� �߻�" + dataPath + "\n" + e);
        }
    }
    public GameData DataLoad()
    {
        string dataPath = Path.Combine(directoryPath, fileName);
        GameData loadData = null;

        if (File.Exists(dataPath)) //�ش� ��ο� ������ ������ null�� ��ȯ
        {
            try
            {
                string dataToLoad = "";

                using(FileStream stream = new FileStream(dataPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); //�����͸� �����ϴ� ��Ʈ�� ���� �ϳ��� �� �ҷ����� 
                    }
                }
                loadData = JsonUtility.FromJson<GameData>(dataToLoad); //�� ������ loadData�� GameData�������� stream���� ������ȭ 
            }
            catch (Exception e) 
            {
                Debug.LogError("���� �߻�" + dataPath + "\n" + e);
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

