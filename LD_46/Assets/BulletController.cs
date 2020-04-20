using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject BulletPrefab;
    private Transform Tracker;
    public float SpawnDist = 5;
    public float SpawnDelay = 3;
    public int MaxBulletNumber = 5;
    public bool Hasbegin = false;
    List<GameObject> BulletList = new List<GameObject>();
    private void Start()
    {
        Tracker = GameObject.FindGameObjectWithTag("Tracker").transform;
    }
    private void FixedUpdate()
    {
        if (!Hasbegin)
            if (Player.Instance.transform.position.y < 104) StartSpawn();
    }
    public void StartSpawn()
    {
        if (!Hasbegin)
            StartCoroutine(SpawnBullet());
    }
    IEnumerator SpawnBullet()
    {
        Hasbegin = true;
        yield return new WaitForSeconds(SpawnDelay / 2);
        while (true)
        {
            if (GameController.Instance.HasWin) yield break;
            else
            {
                for (int i = BulletList.Count - 1; i >= 0; i--)
                {
                    if (BulletList[i] == null) BulletList.RemoveAt(i);
                }
                //print(BulletList.Count);
                if (BulletList.Count >= MaxBulletNumber)
                {
                    yield return new WaitForSeconds(SpawnDelay);
                }
                else
                {
                    float Deg = Tracker.GetComponent<Rigidbody2D>().rotation;
                    Vector3 LDir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (90 + Deg)), Mathf.Sin(Mathf.Deg2Rad * (Deg + 90)), 0);
                    Vector3 RDir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (Deg - 90)), Mathf.Sin(Mathf.Deg2Rad * (Deg - 90)), 0);
                    if (Random.value > 0.5f)
                        BulletList.Add(Instantiate(BulletPrefab, Tracker.position + LDir * SpawnDist, Quaternion.identity));
                    else
                        BulletList.Add(Instantiate(BulletPrefab, Tracker.position + RDir * SpawnDist, Quaternion.identity));
                    yield return new WaitForSeconds(SpawnDelay);
                }
            }

        }
    }
}
