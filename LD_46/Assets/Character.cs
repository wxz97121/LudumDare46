using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    Rigidbody2D m_Rigid;
    public float MoveForce = 3;
    public float MouseForce = 100;
    public float RotateForce = 3;
    public Joint2D[] AllJoint2D;
    public float HP=10;
    private void Awake()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        AllJoint2D = GameObject.FindObjectsOfType<Joint2D>();
    }
    void GetInputAndMove()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        m_Rigid.AddForce(MoveForce * new Vector2(x, y));
        if (Input.GetMouseButtonDown(0))
        {
            //print("Fuck");
            Vector2 MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);
            //Debug.DrawLine(m_Rigid.transform.position, MousePos);
            m_Rigid.AddForce((MousePos - m_Rigid.position).normalized * MouseForce, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.I))
        {
            m_Rigid.AddForce(transform.right * MoveForce, 0);
        }
        if (Input.GetKey(KeyCode.K))
        {
            m_Rigid.AddForce(transform.right * MoveForce * -1, 0);
        }
        if (Input.GetKey(KeyCode.J))
        {
            m_Rigid.AddTorque(RotateForce);
        }
        if (Input.GetKey(KeyCode.L))
        {
            m_Rigid.AddTorque(-1 * RotateForce);
        }
    }
    private void FixedUpdate()
    {
        if (HP <= 0) StartCoroutine(Defeat());
        GetInputAndMove();
    }
    IEnumerator Defeat()
    {
        Time.timeScale = 0.1f;
        foreach (var joint2d in AllJoint2D)
        {
            joint2d.breakForce = 0;
            joint2d.breakTorque = 0;
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(0);
    }
}
