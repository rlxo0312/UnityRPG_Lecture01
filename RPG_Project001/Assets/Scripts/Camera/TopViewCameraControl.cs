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
        //�÷��̾ ī�޶� ���� ����� ũ�� �÷��̾ ���ϸ� -> ī�޶� ��ġ
    }

    // Update is called once per frame
    void LateUpdate()
    //PlayerController Update���� �̵���Ű�� LaterUpdate�� ī�޶� �������ش�.
    {
        #region ī�޶� �÷��̾�� ���� �ӵ��� �̵���
        /*transform.position = playerTr.position + offset;*/
        //ī�޶��� ��ġ�� = �÷��̾ �̵��� ��ġ + ī�޶�� �÷��̾ �����Ǿ���� ����� �Ÿ� 
        /*offset = transform.position - playerTr.position;*/
        //�÷��̾ �̵��Կ� ���� ��ȭ�� offset�� �ٽ� �������ش�. 
        #endregion

        //���� ������ �̿��� ī�޶��̵�  

        #region ���� ������ ���� �ε巯�� ī�޶� �̵�
        //(update �� ������)ī�޶� ���������� �����ؾ��ϴ� ��ġ 
        Vector3 targetCampos = playerTr.position + offset;
        //Vector��� ����� ũ�⸦ ���� �����͸� ������ �ִµ� ������ ������ ä�� ũ�⸸ ���ݾ� õõ�� �̵�
        transform.position = Vector3.Lerp(transform.position, targetCampos, smoothValue * Time.deltaTime);
        //Vector.Lerp(1�� �Ű� ����: ī�޶� �̵��ϱ� �� ��ġ, 2�� �Ű� ����: update�� ���� �� ���������� �̵��� ��ġ,
        //            3�� �Ű� ����: a�� b�� �Ÿ� ������ percent�� ��Ÿ�� ��) 
        #endregion


    }
}
