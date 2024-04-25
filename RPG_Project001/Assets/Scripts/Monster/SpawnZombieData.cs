using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZoMbieType { small, medium, big }
public class Zombie 
{
    public int HP;
    public int Attack;
    public float attackRange;
    public Zombie(ZombieData zombieData)
    {
        HP = zombieData.Hp;
        Attack = zombieData.Attack;
        attackRange = zombieData.attackRange;
    }
}

public class SpawnZombieData : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;
    public ZoMbieType zombieType;

    private void Update()
    {        
        SpawnRandomZombie(zombieData);
    }
    public void SpawnRandomZombie(ZombieData zombieData)
    {
        Zombie newZombie = new Zombie(zombieData); //생성자로 불러옴, newZombie는 hp, attack, attackRange의 데이터를 
                                                   //전부 포함해서 가짐 
                                                   //가져오기 떄문에 반복을 줄 수 있다.
                                                   //위 정보들을 좀비 각자가 가진것이 아닌 ScriptableObject에서 데이터를 
        Debug.Log("좀비의 체력: " + newZombie.HP);
        Debug.Log("좀비의 체력: " + newZombie.Attack);
        Debug.Log("좀비의 체력: " + newZombie.attackRange);
    }
}
