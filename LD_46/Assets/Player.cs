using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : SingletonBase<Player>
{
    [HideInInspector]
    public Rigidbody2D m_Rigid;
    public float m_MaxSpeed;
    public float MoveForce = 3;
    static float Gh2 = Mathf.Sqrt(2);
    public float MaxRushVelocity = 2;
    public float RushTime = 1;
    private bool isRush = false;
    private bool isDead = false;
    Rigidbody2D TrackerRigid;
    public GameObject Explosion;
    [HideInInspector]
    static Vector2 SavedPos = new Vector2(0, 0);
    Vector2[] AllRush = new Vector2[8] { new Vector2(0, 1), new Vector2(0.5f * Gh2, 0.5f * Gh2), new Vector2(1, 0), new Vector2(0.5f * Gh2, -0.5f * Gh2), new Vector2(0, -1), new Vector2(-0.5f * Gh2, -0.5f * Gh2), new Vector2(-1, 0), new Vector2(-0.5f * Gh2, 0.5f * Gh2) };
    Vector2 FindRushDir(Vector2 InputDir)
    {
        float MinDist = 1000;
        int index = 0;
        InputDir.Normalize();
        for (int i = 0; i < 8; i++)
        {
            if (Vector2.Angle(AllRush[i], InputDir) < MinDist)
            {
                MinDist = Vector2.Distance(AllRush[i], InputDir);
                index = i;
            }
        }
        return AllRush[index];
    }

    private void Awake()
    {
        Time.timeScale = 1f;
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Rigid.position = SavedController.Instance.NowPlayerPos();
        TrackerRigid = GameObject.FindGameObjectWithTag("Tracker").GetComponent<Rigidbody2D>();
        TrackerRigid.position = SavedController.Instance.NowTrackerPos();
        TrackerRigid.rotation = SavedController.Instance.NowRotation();
    }
    void FixedUpdate()
    {

        if (isDead) return;
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        //m_Rigid.velocity = new Vector2(x, y) * MoveForce;
        if (!isRush)
        {
            m_Rigid.MovePosition(m_Rigid.position + new Vector2(x, y) * MoveForce);
            /*
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {

                var RushDir = FindRushDir(new Vector2(x, y));
                m_Rigid.velocity = new Vector2(0, 0);
                StartCoroutine(Rush(new Vector2(x, y)));
                //TODO: 特效
            }
            */
        }
    }
    void Update()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        if (!isRush && !isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {

                var RushDir = FindRushDir(new Vector2(x, y));
                m_Rigid.velocity = new Vector2(0, 0);
                StartCoroutine(Rush(new Vector2(x, y)));
            }
        }
    }
    //m_Rigid.AddForce(new Vector2(x, y));

    /*
    if (Mathf.Abs(m_Rigid.velocity.x) > m_MaxSpeed && (m_Rigid.velocity.x * x > 0))
        m_Rigid.AddForce(new Vector3());
    //m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
    else m_Rigid.AddForce(new Vector3(x, 0, 0) * MoveForce);
    */

    float LastRushTime;
    IEnumerator Rush(Vector2 Dir)
    {
        Dir.Normalize();
        isRush = true;
        LastRushTime = Time.time;
        while (Time.time - LastRushTime < RushTime)
        {
            if (isDead) yield break;
            m_Rigid.velocity = Dir * MaxRushVelocity * (1 - (Time.time - LastRushTime) / RushTime);
            yield return new WaitForFixedUpdate();
        }
        isRush = false;
    }
    public IEnumerator Defeat()
    {
        isDead = true;
        Time.timeScale = 0.6f;
        TrackerRigid.GetComponent<Renderer>().enabled = false;
        Instantiate(Explosion, TrackerRigid.transform);
        yield return new WaitForSeconds(1.75f);
        SceneManager.LoadScene(1);
    }
}
