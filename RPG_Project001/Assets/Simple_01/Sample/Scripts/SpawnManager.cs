using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enermy;
    [SerializeField] private GameObject powerupObject;
    [SerializeField] private Transform enermySpawnPosition;

    public int enermyCount = 0;
    public int waveNumber = 1;

    public float limitwidth = 5;
    public float limitheight = 11;
    // Start is called before the first frame update
    void Start()
    {

        //�Ѱ��� ���� Ư�� ��ġ���� �����Ǵ� �ڵ� 
        SpawnEnermy(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {       
        enermyCount = FindObjectsOfType<SampleEnermy>().Length; //���̾��Ű���� sampleEnermy ��ũ��Ʈ�� ���� �ִ� ������Ʈ�� ã�Ƽ� �� ������ ��ȯ�ϴ� �ڵ�

        if (enermyCount == 0)    //sampleEnermy ��ũ��Ʈ�� ���� �ִ� ������Ʈ 0 "��� Enermy�� �׾��� �� ���� �����Ѵ�.
        {
            waveNumber++;
            SpawnEnermy(waveNumber);
        }
    }
    private void SpawnEnermy(int spawnNumber) //���� ���� óġ�� �� ���� Wave�� ���� 1�� ���� �ϰ� ������ wave �� ��ŭ ���� ���̺�(enermy)�� �����Ѵ�. 
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject enermyObj = Instantiate(enermy, RandomSpawnPosition(), Quaternion.identity);
        }

        
    }
    private Vector3 RandomSpawnPosition()
    {
        float randomX = UnityEngine.Random.Range(-limitwidth, limitwidth);
        float randomZ = UnityEngine.Random.Range(-limitheight, limitheight);
        Vector3 randomPos = new Vector3(randomX, 0, randomZ);

        return randomPos;
    }
}
