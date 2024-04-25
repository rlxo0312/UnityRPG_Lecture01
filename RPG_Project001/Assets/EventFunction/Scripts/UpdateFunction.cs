using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update�Լ��� ȣ��");
    }
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate�Լ��� ȣ��");
    }
    private void LateUpdate()
    {
        Debug.Log("LateUpdate�Լ��� ȣ��");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(0, 0, 5), 10f);

        Gizmos.color= Color.green;
        Gizmos.DrawRay(new Vector3(0, 0, -5), new Vector3(1, 2, 5));
    }

    private void OnApplicationQuit()
    {
        Debug.Log("������");
    }
    private void OnDestroy()
    {
        Debug.Log("������Ʈ �ı�");
    }
    private void OnDisable()
    {
        Debug.Log("������Ʈ ��Ȱ��ȭ");
    }
}
