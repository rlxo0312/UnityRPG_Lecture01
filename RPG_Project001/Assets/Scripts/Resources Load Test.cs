using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesLoadTest : MonoBehaviour
{
    public Image testImage; //Cavus -> BG 와 연결
    private int currentNumber;

    // Start is called before the first frame update
    void Start()
    {
        testImage.sprite = Resources.Load<Sprite>("Album/Album_02") as Sprite; //Resource.Load 호출하는 데이터 -> Aasset에 저장이 된다 <T> 제네릭 형변환 구현
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
         currentNumber = Random.Range(0, 4); // 0 ~ 3까지 반환하는 함수
         //클래스 - 정보를 담아 둔 클래스
     }*/
    private int ChangeCurrentNumber()
    {
        currentNumber = Random.Range(1, 4); // 1 ~ 3까지 반환하는 함수
        //클래스 - 정보를 담아 둔 클래스
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

        path += "0" + imageNumber.ToString(); //01 Format형식
        Debug.Log(path);

        testImage.sprite = Resources.Load<Sprite>(path) as Sprite; //Resources.Load로 동적으로 이미지 변환
    }
    private void GETCurrentImagerNumber()
    {
        currentNumber = 0;

    }
}
