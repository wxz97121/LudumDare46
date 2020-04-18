using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object _singletonLock = new object();

    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_singletonLock)
                {
                    T[] singletonInstances = FindObjectsOfType(typeof(T)) as T[];
                    if (singletonInstances.Length == 0) return null;

                    if (singletonInstances.Length > 1)
                    {
                        if (Application.isEditor)
                            Debug.LogWarning(
                                "MonoSingleton<T>.Instance: Only 1 singleton instance can exist in the scene. Null will be returned.");
                        return null;
                    }
                    _instance = singletonInstances[0];
                }
            }
            return _instance;
        }
    }
}

public class Character : SingletonBase<Character>
{

    public enum MoveType
    {
        SetVelocity,
        AddForce,
        MovePos
    }
    public MoveType m_MoveType;
    Rigidbody2D m_Rigid;
    public float MoveForce = 3;
    public float MouseForce = 100;
    public float RotateForce = 3;
    public Joint2D[] AllJoint2D;
    public float HP = 10;
    bool isDead = false;
    private void Awake()
    {
        Time.timeScale = 1;
        m_Rigid = GetComponent<Rigidbody2D>();
        AllJoint2D = FindObjectsOfType<Joint2D>();
    }
    void GetInputAndMove()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        switch (m_MoveType)
        {
            case MoveType.AddForce:
                m_Rigid.AddForce(MoveForce * new Vector2(x, y));
                break;
            case MoveType.SetVelocity:
                var NewVelocity = m_Rigid.velocity;
                if (Mathf.Abs(x) > 0.02) NewVelocity.x = x * MoveForce / 5;
                if (Mathf.Abs(y) > 0.02) NewVelocity.y = y * MoveForce / 5;
                m_Rigid.velocity = NewVelocity;
                break;
            case MoveType.MovePos:
                m_Rigid.MovePosition(m_Rigid.position + new Vector2(x, y) * Time.fixedDeltaTime * MoveForce / 5);
                break;
            default:
                break;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            //m_Rigid.AddForce((MousePos - m_Rigid.position).normalized * MouseForce, ForceMode2D.Impulse);
            m_Rigid.velocity = new Vector2(x, y);
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
        if (isDead) return;
        if (HP <= 0) StartCoroutine(Defeat());
        GetInputAndMove();
    }
    IEnumerator Defeat()
    {
        //m_Rigid.position;
        //print("Fuck");
        isDead = true;
        Time.timeScale = 0.75f;
        foreach (var joint2d in AllJoint2D)
        {
            joint2d.breakForce = 0;
            joint2d.breakTorque = 0;
        }
        yield return new WaitForSeconds(1.75f);
        SceneManager.LoadScene(0);
    }
    public void GetDamage(float DeltaHP)
    {
        HP += DeltaHP;
        //print(HP);
        //TODO: 音效和视觉效果
    }
}
