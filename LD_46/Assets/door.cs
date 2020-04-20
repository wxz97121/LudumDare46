using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class door : MonoBehaviour
{
    public GameObject[] Keys;
    int KeysNum;
    public TextMesh m_TextMesh;
    public Vector2 MoveVector;
    bool END = false;
    private void Start()
    {
        KeysNum = Keys.Length;
    }
    private void FixedUpdate()
    {
        if (END) return;
        int NowNum = 0;
        for (int i = 0; i < KeysNum; i++)
        {
            if (Keys[i] == null) NowNum++;
        }

        if (NowNum == KeysNum) StartCoroutine(DisAppear());
        m_TextMesh.text = NowNum.ToString() + '/' + KeysNum.ToString();
    }
    IEnumerator DisAppear()
    {
        END = true;
        GetComponent<Collider2D>().isTrigger = true;
        transform.DOMoveX(transform.position.x + MoveVector.x, 1.5f);
        transform.DOMoveY(transform.position.y + MoveVector.y, 1.5f);
        yield return new WaitForSeconds(1.5f);
    }
}
