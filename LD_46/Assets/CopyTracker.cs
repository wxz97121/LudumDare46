using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTracker : MonoBehaviour
{
    Vector2[] RigidPos;
    public int DelayFrame = 300;
    [HideInInspector]
    public int NowIndex = 0;
    bool HasBegin = false;
    Rigidbody2D m_Rigid;
    Player m_Player;
    private void Start()
    {
        m_Player = Player.Instance;
        RigidPos = new Vector2[DelayFrame];
        m_Rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (NowIndex >= DelayFrame)
        {
            HasBegin = true;
            NowIndex = 0;
        }
        if (HasBegin)
            m_Rigid.MovePosition(RigidPos[NowIndex]);
        RigidPos[NowIndex] = m_Player.m_Rigid.position;
        NowIndex++;
        
    }

}
