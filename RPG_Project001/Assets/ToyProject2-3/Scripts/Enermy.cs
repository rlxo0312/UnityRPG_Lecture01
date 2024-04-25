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
            Debug.Log("적과 충돌함");
            //적과 충돌했을 때 적이 밖으로 더 잘 날아가게 해주는 기능 추가 

            Vector3 powerVector = (transform.position - collision.transform.position).normalized;

            Rigidbody enermyRigidbody = collision.gameObject.GetComponent<Rigidbody>(); // 충돌(플레이어)과 Enermy의 방향을 구한다. (nomalized를 통해 힘의 크기를 뺀 방향만 구하 수 있다)
                                                                                        //Enermy가 갖고 있는 Rigidbody를 참조해서 Enermy의 물리 효과를 구현할 수 있다. 
                                                                                        //EnermyRigidbody.AddForce 함수를 이용해서 Enermy가 충돌할 때 더 크게 날아가도록 변경
            enermyRigidbody.AddForce(powerVector * pushPower, ForceMode.Impulse);
        }
    }

   
}
