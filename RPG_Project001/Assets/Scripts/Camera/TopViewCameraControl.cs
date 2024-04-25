using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCameraControl : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] Transform playerTr;
    [SerializeField] float smoothValue = 5f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTr.position;
        //플레이어가 카메라를 보는 방향과 크기 플레이어를 더하면 -> 카메라 위치
    }

    // Update is called once per frame
    void LateUpdate()
    //PlayerController Update에서 이동시키고 LaterUpdate를 카메라를 움직여준다.
    {
        #region 카메라가 플레이어와 같은 속도로 이동함
        /*transform.position = playerTr.position + offset;*/
        //카메라의 위치는 = 플레이어가 이동한 위치 + 카메라와 플레이어가 고정되어야할 방향과 거리 
        /*offset = transform.position - playerTr.position;*/
        //플레이어가 이동함에 따라 변화한 offset을 다시 갱신해준다. 
        #endregion

        //선형 보간을 이용한 카메라이동  

        #region 선형 보간을 통해 부드러운 카메라 이동
        //(update 될 때마다)카메라가 최종적으로 도착해야하는 위치 
        Vector3 targetCampos = playerTr.position + offset;
        //Vector라는 방향과 크기를 가진 데이터를 가지고 있는데 방향을 유지한 채로 크기만 조금씩 천천히 이동
        transform.position = Vector3.Lerp(transform.position, targetCampos, smoothValue * Time.deltaTime);
        //Vector.Lerp(1번 매개 변수: 카메라가 이동하기 전 위치, 2번 매개 변수: update가 끝날 때 최종적으로 이동할 위치,
        //            3번 매개 변수: a와 b의 거리 비율을 percent로 나타낸 값) 
        #endregion


    }
}
