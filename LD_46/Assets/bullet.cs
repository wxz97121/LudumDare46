using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Transform Tracker;
    public float Speed = 2.5f;
    public float DestroyDist = 25;
    public float RespawnTime = 12;
    public bool isDead = false;
    private Collider2D m_Collider;
    private void Awake()
    {
        Tracker = GameObject.FindGameObjectWithTag("Tracker").transform;
        m_Collider = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        if (isDead) return;
        if ((transform.position - Tracker.position).magnitude > DestroyDist) Destroy(gameObject);
        transform.position += (Tracker.position - transform.position).normalized * Speed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDead = true;
            m_Collider.enabled = false;
            transform.position = new Vector3(-1000, -1000, -1000);
            StartCoroutine(Respawn());
        }
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnTime);
        float Deg = Tracker.GetComponent<Rigidbody2D>().rotation;
        float Rand = Random.Range(-90, 90);
        Vector3 Dir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (Deg + Rand)), Mathf.Sin(Mathf.Deg2Rad * (Deg + Rand)), 0);
        transform.position = Tracker.position + Dir * 12.5f;
        if (GameController.Instance.HasWin) yield break;
        m_Collider.enabled = true;
        isDead = false;
    }
}
