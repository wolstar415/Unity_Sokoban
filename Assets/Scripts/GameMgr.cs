using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public static GameMgr inst;

    private void Awake() => inst = this;

    public GameObject[] buckets;
    public GameObject[] balls;
    public int curCnt = 0;
    public int maxCnt = 0;
    public int correctCnt;
    public int insCorrectCnt;
    public int curLv = 1;
    public int maxLv;
    public MapGenerator mapGenerator;
    public Text roundtext;
    public Text movetext;
    public Text bestmovetext;
    public int moveCnt=0;

    private void Start()
    {

        int cnt=0;
        while (true)
        {
            cnt++;
            if (!Resources.Load("Maps/Lv"+cnt))
            {
                break;
            }
        }

        maxLv = cnt-1;
    }
    public void movemove()
    {
        moveCnt++;
        movetext.text = "Move " + moveCnt;
    }
    public void SetBuckesAnsBalls()
    {
        StartCoroutine(FindBucketsAndBalls());
    }
    public void ButtonReset()
    {
        mapGenerator.Reset(curLv);
    }
    public void ButtonNext()
    {
        curLv++;
        if (curLv> maxLv)
        {
            curLv = maxLv;
        }
        mapGenerator.Reset(curLv);
    }
    public void Buttonprevious()
    {
        curLv--;
        if (curLv < 1)
        {
            curLv = 1;
        }
        mapGenerator.Reset(curLv);
    }
    
    IEnumerator FindBucketsAndBalls()
    {
        yield return buckets = new GameObject[GameObject.FindGameObjectsWithTag("Bucket").Length];
        yield return balls = new GameObject[GameObject.FindGameObjectsWithTag("Ball").Length];
        
        buckets = GameObject.FindGameObjectsWithTag("Bucket");
        balls = GameObject.FindGameObjectsWithTag("Ball");
        maxCnt = balls.Length;
        moveCnt = 0;
        movetext.text = "Move " + moveCnt;
        roundtext.text = "Round " + curLv;

        int movebest = PlayerPrefs.GetInt("Round" + curLv.ToString(),0);
        if (movebest ==0)
        {
            bestmovetext.text = "Best Move -";
        }
        else
        {
            bestmovetext.text = "Best Move " + movebest;
        }
        //Debug.Log("MAX COUNT :::: " + maxCnt);

    }
    

}
