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
    public int maxLv = 20;
    public MapGenerator mapGenerator;
    public Text roundtext;
    public Text movetext;
    public Text bestmovetext;
    public int moveCnt=0;

    private void Start()
    {
        mapGenerator = GameObject.Find("Map12x12").GetComponent<MapGenerator>();
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

    public void CheckBall()
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            for (int j = 0; j < balls.Length; j++)
            {
                if (buckets[i].transform.position == balls[j].transform.position)
                {
                    correctCnt++;
                    curCnt = correctCnt;
                }
            }
        }

        correctCnt = 0;
        if (curCnt == maxCnt)
        {
            Debug.Log("Game Clear!!!!!!");
            curLv++;
            curCnt = 0;
            maxCnt = 0;
            if (curLv != maxLv)
                mapGenerator.MapDestory(curLv);
            else
                Debug.Log("Game All Clear!!!!!!!");

        }
    }

}
