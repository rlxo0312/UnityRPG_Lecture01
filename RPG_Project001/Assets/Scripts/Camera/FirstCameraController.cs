using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraController : MonoBehaviour
{
    [SerializeField] private Transform ViewPort;
    //카메라 회전 제어를 위한 변수 
    [SerializeField] private float mouseSensivity = 1f;
    [SerializeField] private int limitAngle = 60; //마우스의 상하 회전을 제한하는 코드
    private float verticalRot;  //오일러 회전하기 위한 수치를 저장해두기 위한 값
    private Vector2 mouseInput;

    [SerializeField] private bool inverseLook; //true이면 마우스 상하반전 false이면 정상
    //1인칭 카메라를 플레이어의 자식으로 귀속시키지 않고 플레이어를 따라오게하기 위해 카메라 변수를 가져옴
    [SerializeField] private Camera firstCamera; //1인칭 카메라 게임오브젝트를 사용하기 위한 변수 
    //[SerializeField] private Transform playerTr; //1인칭 카메라가 플레이어를 쫓기 위해 플레이어의 위치를 받아오는 변수
    //-> 위 코드를 작성하지 않는 이유 playerControll과 firstCamColltroller는 player에 같은 위치에 존재함 
    // player를 움직이면 firstCamColltroller의 위치도 같이 움직이게 됨 그래서 안가져와도 됨
    // Start is called before the first frame update
    void Start()
    {     
        //마우스 커서를 제한하는 코드 
        Cursor.visible = false; //false는 안보이고 true는 보임
        //메뉴, 옵션버튼을 클릭시 마우스 버튼이 보이게한다 
        Cursor.lockState = CursorLockMode.Locked; //마우스가 게임 상 밖으로 못나가게 해준다. 
    }

    // Update is called once per frame
    void Update()
    {
        float inverseValue = inverseLook ? -1 : 1; //inverseValue inverseLook를 bool값에 따라 마우스 회전을 변경할 수 있다.

        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y") * inverseValue; 

        mouseInput = new Vector2(rotationX, rotationY) * mouseSensivity;

        //좌우 회전 
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + mouseInput.x,
            transform.rotation.eulerAngles.z);
        //Quaternion.Euler 함수 (매개변수로 x, y, z에 각 축의 0 ~ 180회전 수치를 입력하면) 그 수피만큼 회전한 쿼터니언
        //각도를 반환한다. 

        //상하회전 
        verticalRot -= mouseInput.y * inverseValue;
        verticalRot = Mathf.Clamp(verticalRot, -limitAngle, limitAngle); //첫번째 인자로 들어간 값이 최소 최댓값을 넘어가지 않게 해준다.

        ViewPort.rotation = Quaternion.Euler(verticalRot,
            ViewPort.rotation.eulerAngles.y ,
            ViewPort.rotation.eulerAngles.z);
    }
    private void LateUpdate() //playerController의 update문에서 플레이어의 이동이 적용 FIrstCamerController 카메라의 회전이 적용
    {
        firstCamera.transform.position = ViewPort.position; //1인칭 카메라의 회전역활을 하는 viewPort의 position과 위치르 맞춰준다.
        firstCamera.transform.rotation = ViewPort.rotation; 

        //선형 보간법으로 회전을 부드럽게 적용해보기
    }
}
