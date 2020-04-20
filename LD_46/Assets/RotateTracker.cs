using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTracker : MonoBehaviour
{
    //public int DelayFrame = 300;
    [HideInInspector]
    public int NowIndex = 0;
    bool HasBegin = false;
    Rigidbody2D m_Rigid;
    Player m_Player;
    public float MaxRotateSpeed = 60;
    private void Start()
    {
        m_Player = Player.Instance;
        m_Rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 NowDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * m_Rigid.rotation), Mathf.Sin(Mathf.Deg2Rad * m_Rigid.rotation));
        var TarDir = Player.Instance.m_Rigid.position - m_Rigid.position;
        var TarDeg = Vector2.SignedAngle(TarDir, Vector2.right);
        print(TarDeg);
        if (Vector2.Angle(NowDir, TarDir) < MaxRotateSpeed * Time.fixedDeltaTime)
        {
            //m_Rigid.rotation = TarDeg;
        }
        else
        {
            float Dir = -1 * Mathf.Sign(Vector2.SignedAngle(TarDir, NowDir));
            m_Rigid.rotation = (m_Rigid.rotation + Dir * MaxRotateSpeed * Time.fixedDeltaTime);
        }
    }

}
