using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerControl : MonoBehaviour
{
    //Feild�� ����ȭ 
    [SerializeField] private float moveSpeed;           //�÷��̾� �̵��ӵ�
    [SerializeField] private GameObject powerIndicator; //�Ŀ��� ���¸� Ȯ�ν��� �ִ� �߿� ������Ʈ
    [SerializeField] private float powerUpSpeed;
    
    private Rigidbody rigidbody;//�÷��̾� ���� ������ ���� ������Ʈ 

    
    //[SerializeField] private GameObject powerIndicator;     // �Ŀ��� ���¸� Ȯ�ν��� �ֱ� ���� ���� ������Ʈ

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
            /*Invoke("PowerUpTimeover", powerUpDucation);*/ // 7f = �Ŀ����� ���ӵǱ⸦ ���ϴ� �ð� -> ����ȭ ���Ѽ� �����͸� ������ �� �ִ�. 
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
            Destroy(other.gameObject);       //������Ʈ�� �Ծ����Ƿ� �ش� ������Ʈ �ı�
            IsPowerUp = true;                //������Ʈ�� �Ծ��� �� ����� ����
            powerIndicator?.SetActive(true); //������Ʈ�� Ȱ��ȭ �Ǵ� �ڵ�
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
