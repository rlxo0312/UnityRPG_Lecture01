using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCameraController : MonoBehaviour
{
    [SerializeField] private Transform ViewPort;
    //ī�޶� ȸ�� ��� ���� ���� 
    [SerializeField] private float mouseSensivity = 1f;
    [SerializeField] private int limitAngle = 60; //���콺�� ���� ȸ���� �����ϴ� �ڵ�
    private float verticalRot;  //���Ϸ� ȸ���ϱ� ���� ��ġ�� �����صα� ���� ��
    private Vector2 mouseInput;

    [SerializeField] private bool inverseLook; //true�̸� ���콺 ���Ϲ��� false�̸� ����
    //1��Ī ī�޶� �÷��̾��� �ڽ����� �ͼӽ�Ű�� �ʰ� �÷��̾ ��������ϱ� ���� ī�޶� ������ ������
    [SerializeField] private Camera firstCamera; //1��Ī ī�޶� ���ӿ�����Ʈ�� ����ϱ� ���� ���� 
    //[SerializeField] private Transform playerTr; //1��Ī ī�޶� �÷��̾ �ѱ� ���� �÷��̾��� ��ġ�� �޾ƿ��� ����
    //-> �� �ڵ带 �ۼ����� �ʴ� ���� playerControll�� firstCamColltroller�� player�� ���� ��ġ�� ������ 
    // player�� �����̸� firstCamColltroller�� ��ġ�� ���� �����̰� �� �׷��� �Ȱ����͵� ��
    // Start is called before the first frame update
    void Start()
    {     
        //���콺 Ŀ���� �����ϴ� �ڵ� 
        Cursor.visible = false; //false�� �Ⱥ��̰� true�� ����
        //�޴�, �ɼǹ�ư�� Ŭ���� ���콺 ��ư�� ���̰��Ѵ� 
        Cursor.lockState = CursorLockMode.Locked; //���콺�� ���� �� ������ �������� ���ش�. 
    }

    // Update is called once per frame
    void Update()
    {
        float inverseValue = inverseLook ? -1 : 1; //inverseValue inverseLook�� bool���� ���� ���콺 ȸ���� ������ �� �ִ�.

        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y") * inverseValue; 

        mouseInput = new Vector2(rotationX, rotationY) * mouseSensivity;

        //�¿� ȸ�� 
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + mouseInput.x,
            transform.rotation.eulerAngles.z);
        //Quaternion.Euler �Լ� (�Ű������� x, y, z�� �� ���� 0 ~ 180ȸ�� ��ġ�� �Է��ϸ�) �� ���Ǹ�ŭ ȸ���� ���ʹϾ�
        //������ ��ȯ�Ѵ�. 

        //����ȸ�� 
        verticalRot -= mouseInput.y * inverseValue;
        verticalRot = Mathf.Clamp(verticalRot, -limitAngle, limitAngle); //ù��° ���ڷ� �� ���� �ּ� �ִ��� �Ѿ�� �ʰ� ���ش�.

        ViewPort.rotation = Quaternion.Euler(verticalRot,
            ViewPort.rotation.eulerAngles.y ,
            ViewPort.rotation.eulerAngles.z);
    }
    private void LateUpdate() //playerController�� update������ �÷��̾��� �̵��� ���� FIrstCamerController ī�޶��� ȸ���� ����
    {
        firstCamera.transform.position = ViewPort.position; //1��Ī ī�޶��� ȸ����Ȱ�� �ϴ� viewPort�� position�� ��ġ�� �����ش�.
        firstCamera.transform.rotation = ViewPort.rotation; 

        //���� ���������� ȸ���� �ε巴�� �����غ���
    }
}
