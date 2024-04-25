using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesLoadTest : MonoBehaviour
{
    public Image testImage; //Cavus -> BG �� ����
    private int currentNumber;

    // Start is called before the first frame update
    void Start()
    {
        testImage.sprite = Resources.Load<Sprite>("Album/Album_02") as Sprite; //Resource.Load ȣ���ϴ� ������ -> Aasset�� ������ �ȴ� <T> ���׸� ����ȯ ����
    }

    private void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.Alpha1))
         {
             testImage.sprite = Resources.Load<Sprite>("Album/Album_01") as Sprite;
         }
         else if (Input.GetKeyDown(KeyCode.Alpha2))
         {
             testImage.sprite = Resources.Load<Sprite>("Album/Album_02") as Sprite;
         }
         else if (Input.GetKeyDown(KeyCode.Alpha3))
         {
             testImage.sprite = Resources.Load<Sprite>("Album/Album_03") as Sprite;
         }
         */
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCurrentNumber();
            ChangeTestImageDynamic(GETCurrentImagerNumber());
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //GETCurrentImagerNumber();
            ChangeTestImageDynamic(ChangeCurrentNumber());
        }
    }
    /* private void ChangeCurrentNumber()
     {
         currentNumber = Random.Range(0, 4); // 0 ~ 3���� ��ȯ�ϴ� �Լ�
         //Ŭ���� - ������ ��� �� Ŭ����
     }*/
    private int ChangeCurrentNumber()
    {
        currentNumber = Random.Range(1, 4); // 1 ~ 3���� ��ȯ�ϴ� �Լ�
        //Ŭ���� - ������ ��� �� Ŭ����
        return currentNumber;
    }
    /*private int GETCurrentImagerNumber()
    {
        currentNumber = 0;
        return currentNumber;
    }*/
    public void ChangeTestImageDynamic(int imageNumber)
    {
        /*testImage.sprite = Resources.Load<Sprite>("Album/Aibum_01") as Sprite;*/
        string path = "Album/Album_";

        path += "0" + imageNumber.ToString(); //01 Format����
        Debug.Log(path);

        testImage.sprite = Resources.Load<Sprite>(path) as Sprite; //Resources.Load�� �������� �̹��� ��ȯ
    }
    private void GETCurrentImagerNumber()
    {
        currentNumber = 0;

    }
}
