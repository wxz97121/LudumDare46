using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float LinearDrag = 0.05f;
    public float AngularDrag = 0.1f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("it"))
        {
            collision.GetComponent<Rigidbody2D>().drag = LinearDrag;
            collision.GetComponent<Rigidbody2D>().angularDrag = AngularDrag;
        }
    }
}
