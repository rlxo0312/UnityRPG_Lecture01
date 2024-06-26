using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update함수를 호출");
    }
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate함수를 호출");
    }
    private void LateUpdate()
    {
        Debug.Log("LateUpdate함수를 호출");
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
        Debug.Log("앱종료");
    }
    private void OnDestroy()
    {
        Debug.Log("오브젝트 파괴");
    }
    private void OnDisable()
    {
        Debug.Log("오브젝트 비활성화");
    }
}
