using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTracker : MonoBehaviour
{
    //public int DelayFrame = 300;
    [HideInInspector]
    public int NowIndex = 0;
    bool isDead = false;
    Rigidbody2D m_Rigid;
    Player m_Player;
    public float MaxRotateSpeed = 60;
    public float MoveSpeed = 5;
    public float TimeToBegin = 2f;
    private void Start()
    {
        m_Player = Player.Instance;
        m_Rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (GameController.Instance.HasWin) return;
        if (isDead) return;
        Vector2 NowDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * m_Rigid.rotation), Mathf.Sin(Mathf.Deg2Rad * m_Rigid.rotation));
        var TarDir = Player.Instance.m_Rigid.position - m_Rigid.position;
        var TarDeg = Vector2.SignedAngle(TarDir, Vector2.right);
        if (Vector2.Angle(NowDir, TarDir) < MaxRotateSpeed * Time.fixedDeltaTime)
        {
            //m_Rigid.rotation = TarDeg;
        }
        else
        {
            float Dir = -1 * Mathf.Sign(Vector2.SignedAngle(TarDir, NowDir));
            m_Rigid.rotation = (m_Rigid.rotation + Dir * MaxRotateSpeed * Time.fixedDeltaTime);
        }
        if (Time.timeSinceLevelLoad > TimeToBegin)
            m_Rigid.velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * m_Rigid.rotation), Mathf.Sin(Mathf.Deg2Rad * m_Rigid.rotation)) * MoveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController.Instance.HasWin) return;
        if (collision.CompareTag("Player")) Dead();
        if (collision.CompareTag("Die")) Dead();
        if (collision.CompareTag("Bullet")) Dead();
        if (collision.CompareTag("Key")) Destroy(collision.gameObject);
    }
    public void Dead()
    {
        //TODO: Spawn VFX and audio
        StartCoroutine(Player.Instance.Defeat());
        isDead = true;
    }
}
