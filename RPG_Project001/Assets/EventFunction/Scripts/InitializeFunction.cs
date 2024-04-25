using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        //start에서 선언된 변수 실행시 -> 실행 x 
        Debug.Log("Awake함수가 실행됨");
    }
    // Update is called once per frame
    void Start()
    {
        //변수 선언
        Debug.Log("Start함수가 실행됨");
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable함수가 실행됨");
    }
}
