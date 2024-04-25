using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerControl : MonoBehaviour
{
    //Feild를 직렬화 
    [SerializeField] private float moveSpeed;           //플레이어 이동속도
    [SerializeField] private GameObject powerIndicator; //파워업 상태를 확인시켜 주는 중요 오브젝트
    [SerializeField] private float powerUpSpeed;
    
    private Rigidbody rigidbody;//플레이어 물리 구현을 위한 컴포넌트 

    
    //[SerializeField] private GameObject powerIndicator;     // 파워업 상태를 확인시켜 주기 위한 게임 오브젝트

    public bool IsPowerUp = false;
    [SerializeField] private float powerUpDuration = 7f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    [SerializeField] private float pushPower = 20f;
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (h, 0, v).normalized;
        rigidbody.AddForce(direction * moveSpeed);
        if (IsPowerUp)
        {
            StartCoroutine(PlayerPowerUp());
            /*Invoke("PowerUpTimeover", powerUpDucation);*/ // 7f = 파워업이 지속되기를 원하는 시간 -> 변수화 시켜서 데이터를 조합할 수 있다. 
            //moveSpeed += powerUpSpeed;
        }
    }
    IEnumerator PlayerPowerUp()
    {
        IsPowerUp = true;
        powerIndicator.SetActive(true);

        yield return new WaitForSeconds(powerUpDuration);

        IsPowerUp = false;
        powerIndicator.SetActive(false);
    }
    void PowerUpTimeover()
    {
        IsPowerUp = false;
        powerIndicator.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Debug.Log($"{other.gameObject}");
            Destroy(other.gameObject);       //오브젝트를 먹었으므로 해당 오브젝트 파괴
            IsPowerUp = true;                //오브젝트를 먹었을 때 기능을 구현
            powerIndicator?.SetActive(true); //오브젝트가 활성화 되는 코드
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ICollisionable col = collision.gameObject.GetComponent<ICollisionable>();

        if(col != null)
        {
            col.CollideWithPlayer(transform, pushPower);
        }
    }


}
