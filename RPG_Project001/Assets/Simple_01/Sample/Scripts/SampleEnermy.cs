using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICollisionable
{
    public void CollideWithPlayer(Transform player, float push_Power); // Player(공)과 부딪힌 객체가 특정 방향으로 날아가는 기능을 인터페이스로 구현
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
            Debug.Log("적과 충돌함");
            //적과 충돌했을 때 적이 밖으로 더 잘 날아가게 해주는 기능 추가 
                    
            Vector3 powerVector = (transform.position - collision.transform.position).normalized;

            
                                                                                        //Enermy가 갖고 있는 Rigidbody를 참조해서 Enermy의 물리 효과를 구현할 수 있다. 
                                                                                        //EnermyRigidbody.AddForce 함수를 이용해서 Enermy가 충돌할 때 더 크게 날아가도록 변경
            rigidBody.AddForce(powerVector * pushPower, ForceMode.Impulse);
        }
    }*/
    public void CollideWithPlayer(Transform player, float push_Power)
    {
        //throw new System.NotImplementedException(); -> 예외 처리가 됨 
        //플레이어와 충돌했을 때 객체가 날아가는 로직을 작성 
        Debug.Log("CollideWithPlayer인터페이스가 호출됨");
        Vector3 awayVector = (transform.position - player.position).normalized;
        //날아가는 방향 : 출발할 위치(Player)
        rigidBody.AddForce(awayVector * push_Power, ForceMode.Impulse);
        //Player에서 날아가는 힘을 매개변수로 전달해줌 
    }

}
