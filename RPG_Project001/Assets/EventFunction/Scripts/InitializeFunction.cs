using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        //start���� ����� ���� ����� -> ���� x 
        Debug.Log("Awake�Լ��� �����");
    }
    // Update is called once per frame
    void Start()
    {
        //���� ����
        Debug.Log("Start�Լ��� �����");
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable�Լ��� �����");
    }
}
