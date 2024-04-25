using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Enermy : MonoBehaviour
{
    public GameObject centerPoint;
    public float enermySpeed;
    public Rigidbody rigidbody;
    private Vector3 targetDirection;
    [SerializeField] private float pushPower;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(targetDirection * enermySpeed);
        targetDirection = (centerPoint.transform.position - transform.position).normalized;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyZone"))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("���� �浹��");
            //���� �浹���� �� ���� ������ �� �� ���ư��� ���ִ� ��� �߰� 

            Vector3 powerVector = (transform.position - collision.transform.position).normalized;

            Rigidbody enermyRigidbody = collision.gameObject.GetComponent<Rigidbody>(); // �浹(�÷��̾�)�� Enermy�� ������ ���Ѵ�. (nomalized�� ���� ���� ũ�⸦ �� ���⸸ ���� �� �ִ�)
                                                                                        //Enermy�� ���� �ִ� Rigidbody�� �����ؼ� Enermy�� ���� ȿ���� ������ �� �ִ�. 
                                                                                        //EnermyRigidbody.AddForce �Լ��� �̿��ؼ� Enermy�� �浹�� �� �� ũ�� ���ư����� ����
            enermyRigidbody.AddForce(powerVector * pushPower, ForceMode.Impulse);
        }
    }

   
}
