using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour, ICollisionable
{
    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void CollideWithPlayer(Transform player, float push_Power)
    {
        Vector3 awayVector = (transform.position - player.position).normalized;
        rigid.AddForce(awayVector * push_Power, ForceMode.Impulse);
    }

   
}
