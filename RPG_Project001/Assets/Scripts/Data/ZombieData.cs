using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName ="Data/ZombieData", order = int.MaxValue)]
                                                                       //order: ¼ø¼­
public class ZombieData : ScriptableObject
{
    public int Hp;
    public int Attack;
    public float attackRange;
}
