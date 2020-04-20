using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerCamera : MonoBehaviour
{
    public float FollowSpeed = 3;
    public float MinX = -10, MaxX = 10, MinY = -10, MaxY = 10;
    public float DistToMove = 1.5f;
    public Transform Player, Enemy;
    public float MaxDist = 7, MinDist = 3;
    private Camera m_Camera;
    private void Start()
    {
        m_Camera = GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        var TargetPos = (Player.position + Enemy.position) / 2;
        var MoveDir = TargetPos - transform.position;
        Vector3 newVector;
        float Alpha = Mathf.Clamp01(((Player.position - Enemy.position).magnitude - MinDist) / MaxDist);
        m_Camera.fieldOfView = Mathf.Lerp(60, 90, Alpha);
        if (Mathf.Abs(MoveDir.x) < DistToMove && Mathf.Abs(MoveDir.y) < DistToMove) return;
        newVector = Vector3.Lerp(transform.position, TargetPos, Time.fixedDeltaTime * 2);
        

        //MoveDir.z = 0;
        //if (MoveDir.magnitude > FollowSpeed * Time.deltaTime)
        //{
        //    MoveDir.Normalize();
        //    newVector = transform.position + MoveDir * FollowSpeed * Time.fixedDeltaTime;
        //}
        //else newVector = TargetTransform.position;

        newVector = new Vector3(Mathf.Clamp(newVector.x, MinX, MaxX), Mathf.Clamp(newVector.y, MinY, MaxY), transform.position.z);
        transform.position = newVector;
    }
}
