using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 3;
    public float MinX = -10, MaxX = 10, MinY = -10, MaxY = 10;
    public float DistToMove = 1.5f;
    public Transform TargetTransform;
    void Update()
    {
        var MoveDir = TargetTransform.position - transform.position;
        Vector3 newVector;

        if (Mathf.Abs(MoveDir.x) < DistToMove && Mathf.Abs(MoveDir.y) < DistToMove) return;
        newVector = Vector3.Lerp(transform.position, TargetTransform.position, Time.deltaTime * 2);
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
