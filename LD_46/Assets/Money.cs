using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public float Force = 3;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("it"))
        {
            var Dir = (transform.position - collision.transform.position);
            Dir.z = 0;
            if (Dir.magnitude < 0.02f) return;
            Dir.Normalize();
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(Dir.x, Dir.y) * Force);
        }

    }
}
