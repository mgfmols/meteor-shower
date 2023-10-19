using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCheck : MonoBehaviour
{
    [SerializeField] GameObject player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.Equals(player.GetComponent<MeshCollider>()))
        {
            player.GetComponent<PlayerMovement>().Die();
        }
    }
}
