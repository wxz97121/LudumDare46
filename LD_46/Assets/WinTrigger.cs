using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public float EndTime;
    public TextMesh TimeMesh;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.Instance.HasWin = true;
        }
    }
    private void FixedUpdate()
    {
        TimeMesh.text = "You finished in " + EndTime.ToString() + " seconds.";
        if (!GameController.Instance.HasWin)
            EndTime = Time.unscaledTime;
    }

}
