using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCameraContrioller : MonoBehaviour
{
    [Header("ī�޶� ���� ����")]
    [SerializeField] private Transform target; //ī�޶� ���� ���
    [SerializeField] private float cameraDistance; //���� ī�޶���� �Ÿ�                                               
    [SerializeField] private float rotSpeed; //ī�޶� ȸ���ϴ� �ӵ� ũ�� 
    [SerializeField] private int limitAngle; //ī�޶��� �ִ� ���� ���� 

    [SerializeField] private bool InverseX;  //���콺 ���Ʒ� ���� ����
    [SerializeField] private bool InverseY;  //���콺 �¿� ���� ���� 

    float rotationX;
    float rotationY;

    public Quaternion camLookRotation => Quaternion.Euler(0, rotationY, 0);
    // Update is called once per frame
    void Update()
    {
        float invertXValue = (InverseX) ? -1 : 1;
        float invertYValue = (InverseY) ? -1 : 1;  //���� �̵��� ���� ���� ��� üũ 
        //���콺�� �Է� ���� �޾ƿ´�. 

        rotationX -= Input.GetAxis("Mouse Y") * invertYValue * rotSpeed; //���� ȸ���� ���� ���콺 �Է� �� 
        
        rotationX = Mathf.Clamp(rotationX, -limitAngle, limitAngle); //���콺�� �� �Ʒ��� ������ �� ���� rotationX�� ���� ��ȭ �Ǿ� �����

        rotationY += Input.GetAxis("Mouse X") * invertXValue * rotSpeed; //�¿� ȸ���� ���� ���콺 �Է� ��\

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0); //���� ȸ���� ���� ���Ϸ���ġ, �˿� ȸ���� ���� ���Ϸ� ��ġ�� �ݿ��� ȸ�� ����
                                                                        //targetRotion�� ����
        transform.rotation = targetRotation;

        //ī�޶� �÷��̾ �ѾƼ� �̵��ϴ� ����
        Vector3 focusPosition = target.position;  //���� �÷��̾��� ��ġ 
        transform.position = focusPosition - (targetRotation * new Vector3(0, 0, cameraDistance)); //�ڿ� �ִ� Vector���� �÷��̾ �ٶ󺸴� ���⺤�͸� ��ȯ�Ѵ�.

    }

}
