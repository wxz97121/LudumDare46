using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float DamageValue = -1000;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.name);
        if (collision.CompareTag("it"))
        {
            Character.Instance.GetDamage(DamageValue);
        }
    }
}
