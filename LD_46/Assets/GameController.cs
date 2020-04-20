using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : SingletonBase<GameController>
{
    public Text m_Text;
    public Image Image1, Image2, Image3;
    public float BeginTime;
    public bool HasWin = false;
    public bool Hasbegin = false;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        if (!Hasbegin)
            StartCoroutine(IStart());
    }
    IEnumerator IStart()
    {
        Hasbegin = true;
        
        Image1.DOFade(1, 1f);
        m_Text.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        m_Text.text = "Your Friend: Traitor! I'll kill you!";
        m_Text.DOFade(1, 1);
        yield return new WaitForSeconds(4.5f);

        Image1.DOFade(0, 1f);
        Image2.DOFade(1, 1f);
        m_Text.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        m_Text.text = "You: I didin't betray you!\nIt's a misunderstanding";
        m_Text.DOFade(1, 1);
        yield return new WaitForSeconds(3f);

        Image2.DOFade(0, 1f);
        Image3.DOFade(1, 1f);
        m_Text.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        m_Text.text = "Your Friend: I don't believe you any moreeeeeeee";
        m_Text.DOFade(1, 1);
        yield return new WaitForSeconds(3f);
        BeginTime = Time.unscaledTime;
        SceneManager.LoadScene(1);
    }
}
