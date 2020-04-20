using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavedController : SingletonBase<SavedController>
{
    public Vector2[] PlayerPos, TrackerPos;
    public float[] Rotation;
    public static int Index = 0;
    public static float StartTime = 0;
    public Vector2 NowPlayerPos()
    {
        return PlayerPos[Index];
    }
    public Vector2 NowTrackerPos()
    {
        return TrackerPos[Index];
    }
    public float NowRotation()
    {
        return Rotation[Index];
    }
    public void SetIndex(int newIndex)
    {
        if (newIndex > Index) Index = newIndex;
    }
    private void Start()
    {
        StartTime = Time.unscaledTime;
        //print("TIME RECORDED");
        //DontDestroyOnLoad(gameObject);
    }
}
