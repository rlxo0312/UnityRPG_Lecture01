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
        Zombie newZombie = new Zombie(zombieData); //�����ڷ� �ҷ���, newZombie�� hp, attack, attackRange�� �����͸� 
                                                   //���� �����ؼ� ���� 
                                                   //�������� ������ �ݺ��� �� �� �ִ�.
                                                   //�� �������� ���� ���ڰ� �������� �ƴ� ScriptableObject���� �����͸� 
        Debug.Log("������ ü��: " + newZombie.HP);
        Debug.Log("������ ü��: " + newZombie.Attack);
        Debug.Log("������ ü��: " + newZombie.attackRange);
    }
}
