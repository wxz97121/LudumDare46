using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D m_Rigid;
    public float MoveForce = 3;
    public float MouseForce = 30;
    private void Awake()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        m_Rigid.AddForce(MoveForce * new Vector2(x, y));
        if (Input.GetMouseButtonDown(1))
        {
            print("Fuck");
            Vector2 MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);
            Debug.DrawLine(m_Rigid.transform.position, MousePos);
            m_Rigid.AddForce((MousePos - m_Rigid.position).normalized * MouseForce);
        }
    }
}
