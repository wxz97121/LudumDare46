using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
public class Moving_Platform : MonoBehaviour
{
    public Transform[] TargetPoint;
    private Vector3[] TargetPos;
    public double[] MoveTime;
    private double LastTime = 0;
    private int Nowindex = 0;
    public bool IsResetPos = true;
    //private float SumDist = 0;
    //private double SumTime = 0;
    //Moving_Platform(Transform m_Trans,double m_Time)
    //{
    //    TargetPoint = new Transform[0];
    //    TargetPoint[0] = m_Trans;
    //    MoveTime = new double[0];
    //    MoveTime[0] = m_Time;
    //}
    public void Awake()
    {
        Assert.IsTrue(MoveTime.Length == TargetPoint.Length);

        Nowindex = 0;
        TargetPos = new Vector3[TargetPoint.Length];
        for (int i = 0; i < TargetPoint.Length; i++)
            TargetPos[i] = TargetPoint[i].position;
        //print("name " + IsResetPos);
        if (IsResetPos)
        {
            LastTime = Time.time;
            transform.position = TargetPos[0];
        }
        else LastTime = CalcInitTime();

    }
    double CalcInitTime()
    {
        float Dist = (TargetPos[TargetPos.Length - 1] - TargetPos[0]).magnitude;
        float NowDist = (transform.position - TargetPos[0]).magnitude;
        return -1 * (NowDist / Dist) * MoveTime[0];
        //GetComponent<Collider2D>().m

    }
    private void FixedUpdate()
    {
        //foreach (var m_Rigid in RigidOnPlatform)
        //    print(m_Rigid.velocity);
        double lamda = (Time.time - LastTime) / MoveTime[Nowindex];
        if (lamda > 1)
        {
            LastTime = Time.time;
            Nowindex = (Nowindex + 1) % TargetPos.Length;
            transform.position = TargetPos[Nowindex];
            //GetComponent<Rigidbody2D>().MovePosition(TargetPos[Nowindex]);
            Vector3 newSpeed = TargetPos[(Nowindex + 1) % TargetPos.Length] - TargetPos[Nowindex];
            newSpeed /= (float)MoveTime[Nowindex];
            //print(newSpeed);
        }
        else
        {
            Vector3 newPos = Vector3.Lerp(TargetPos[Nowindex], TargetPos[(Nowindex + 1) % TargetPos.Length], (float)lamda);
            //Vector3 newSpeed = (newPos - transform.position) / Time.fixedDeltaTime;
            //GetComponent<Rigidbody2D>().MovePosition(newPos);
            transform.position = newPos;

            //print(newSpeed);
            //ClearSpeed(newSpeed);
        }
    }
    IEnumerator DisAppear()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.35f);
        GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(0.15f);

        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.15f);
        GetComponent<Collider2D>().isTrigger = false;
    }
}
