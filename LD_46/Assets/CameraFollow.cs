using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 3;
    public float MinX = -10, MaxX = 10, MinY = -10, MaxY = 10;
    public float DistToMove = 1.5f;
    public Transform TargetTransform;
    void FixedUpdate()
    {
        var MoveDir = TargetTransform.position - transform.position;
        MoveDir.z = 0;
        if (Mathf.Abs(MoveDir.x) < DistToMove && Mathf.Abs(MoveDir.y) < DistToMove) return;
        MoveDir.Normalize();
        var newVector = transform.position + MoveDir * FollowSpeed * Time.fixedDeltaTime;
        newVector = new Vector3(Mathf.Clamp(newVector.x, MinX, MaxX), Mathf.Clamp(newVector.y, MinY, MaxY), newVector.z);
        transform.position = newVector;
    }
}
