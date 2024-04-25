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

        //한개의 적만 특정 위치에서 생성되는 코드 
        SpawnEnermy(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {       
        enermyCount = FindObjectsOfType<SampleEnermy>().Length; //하이어라키에서 sampleEnermy 스크립트를 갖고 있는 오브젝트를 찾아서 그 갯수를 반환하는 코드

        if (enermyCount == 0)    //sampleEnermy 스크립트를 갖고 있는 오브젝트 0 "모든 Enermy가 죽었을 때 적을 생성한다.
        {
            waveNumber++;
            SpawnEnermy(waveNumber);
        }
    }
    private void SpawnEnermy(int spawnNumber) //적을 전부 처치할 때 마다 Wave의 수가 1씩 증가 하고 증가한 wave 수 만큼 다음 웨이브(enermy)를 생성한다. 
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
