using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICollisionable
{
    public void CollideWithPlayer(Transform player, float push_Power); // Player(��)�� �ε��� ��ü�� Ư�� �������� ���ư��� ����� �������̽��� ����
}

public class SampleEnermy : MonoBehaviour, ICollisionable
{
    [SerializeField] private GameObject centerPoint;
    [SerializeField] private float EnermySpeed;
    private Rigidbody rigidBody;
    [SerializeField] private float pushPower;

    private Vector3 targetDirection;

    public GameObject CenterPoint { get => centerPoint; set => centerPoint = value; }
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();        
        CenterPoint = GameObject.Find("CenterPivot");
                
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = (CenterPoint.transform.position - transform.position).normalized;
        rigidBody.AddForce(targetDirection * EnermySpeed, ForceMode.Force);

        if(transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyZone"))
        {
            Destroy(other.gameObject);
        }
    }
   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("���� �浹��");
            //���� �浹���� �� ���� ������ �� �� ���ư��� ���ִ� ��� �߰� 
                    
            Vector3 powerVector = (transform.position - collision.transform.position).normalized;

            
                                                                                        //Enermy�� ���� �ִ� Rigidbody�� �����ؼ� Enermy�� ���� ȿ���� ������ �� �ִ�. 
                                                                                        //EnermyRigidbody.AddForce �Լ��� �̿��ؼ� Enermy�� �浹�� �� �� ũ�� ���ư����� ����
            rigidBody.AddForce(powerVector * pushPower, ForceMode.Impulse);
        }
    }*/
    public void CollideWithPlayer(Transform player, float push_Power)
    {
        //throw new System.NotImplementedException(); -> ���� ó���� �� 
        //�÷��̾�� �浹���� �� ��ü�� ���ư��� ������ �ۼ� 
        Debug.Log("CollideWithPlayer�������̽��� ȣ���");
        Vector3 awayVector = (transform.position - player.position).normalized;
        //���ư��� ���� : ����� ��ġ(Player)
        rigidBody.AddForce(awayVector * push_Power, ForceMode.Impulse);
        //Player���� ���ư��� ���� �Ű������� �������� 
    }

}
