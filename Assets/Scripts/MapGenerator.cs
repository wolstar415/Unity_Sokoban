using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[Serializable]
public class MapPack
{
    public List<GameObject> map = new List<GameObject>();
}

public class MapGenerator : MonoBehaviour
{
    public GameObject[] mapObjectPrefab;
    public List<MapPack> mapList = new List<MapPack>();
    public string dataPath;
    public List<Dictionary<string,object>> data;
    public GameMgr gameMgr;

    [SerializeField] 
    private Transform Map12x12;
    [SerializeField] 
    private Transform Ground12x12;


    void Start()  // 처음 시작시 실행되는 함수입니다.
    {
        LoadMapData(1);
        
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                GameObject ground = Instantiate(mapObjectPrefab[0]) as GameObject;
                ground.gameObject.name = ground.tag + "(" + j + ", " + i + ")";
                ground.transform.parent = Ground12x12.transform;
                ground.transform.localPosition = new Vector3(j, 0, i);
            }
            
        }
        MakeMap();
    }
    public void LoadMapData(int stageNum)
    {
        dataPath = "Maps/"+"Lv" + stageNum;
        data = CSVReader.Read(dataPath);
    }

    void MakeMap()
    {
        for (int i = 0; i < 12; i++)
        {
            MapPack mapPack = new MapPack();

            for (int j = 0; j < 12; j++)
            {
                int dataSet = (int)data[i][j.ToString()];
                if (dataSet != 0)
                {
                    if (dataSet==5)
                    {
                    GameObject mapObj1 = Instantiate(mapObjectPrefab[2]) as GameObject;
                    GameObject mapObj2 = Instantiate(mapObjectPrefab[3]) as GameObject;

                        
                        mapObj1.name = mapObj1.tag + "(" + i + ", " + j + ")";
                        mapObj1.transform.parent = Map12x12.transform;
                        mapObj1.transform.localPosition = new Vector3(i, 0, j);
                        mapPack.map.Add(mapObj1);
                        mapObj2.name = mapObj2.tag + "(" + i + ", " + j + ")";
                        mapObj2.transform.parent = Map12x12.transform;
                        mapObj2.transform.localPosition = new Vector3(i, 0, j);
                        mapPack.map.Add(mapObj2);

                        mapObj1.GetComponent<Bucket_Check>().ball = mapObj2;
                        mapObj2.GetComponent<Ball_Check>().Bucket = mapObj1;
                        gameMgr.curCnt++;
                    }
                    else if (dataSet == 6)
                    {
                        GameObject mapObj1 = Instantiate(mapObjectPrefab[1]);
                        GameObject mapObj2 = Instantiate(mapObjectPrefab[4]);

                        
                        mapObj1.name = mapObj1.tag + "(" + i + ", " + j + ")";
                        mapObj1.transform.parent = Map12x12.transform;
                        mapObj1.transform.localPosition = new Vector3(i, 0, j);
                        mapPack.map.Add(mapObj1);
                        mapObj2.name = mapObj2.tag + "(" + i + ", " + j + ")";
                        mapObj2.transform.parent = Map12x12.transform;
                        mapObj2.transform.localPosition = new Vector3(i, 0, j);
                        mapPack.map.Add(mapObj2);
                    }
                    else
                    {
                    GameObject mapObj = Instantiate(mapObjectPrefab[dataSet]) as GameObject;
                    mapObj.name = mapObj.tag + "(" + i + ", " + j + ")";
                    mapObj.transform.parent = Map12x12.transform;
                    mapObj.transform.localPosition = new Vector3(i, 0, j);
                    mapPack.map.Add(mapObj);
                        // switch (mapObj.tag)
                        // {
                        //     case "Wall":
                        //         mapObj.name = mapObj.tag + "(" + i + ", " + j + ")";
                        //         mapObj.transform.parent = Map12x12.transform;
                        //         mapObj.transform.localPosition = new Vector3(i, 0, j);
                        //         mapPack.map.Add(mapObj);
                        //         break;
                        //     case "Bucket":
                        //         mapObj.name = mapObj.tag + "(" + i + ", " + j + ")";
                        //         mapObj.transform.parent = Map12x12.transform;
                        //         mapObj.transform.localPosition = new Vector3(i, 0, j);
                        //         mapPack.map.Add(mapObj);
                        //         break;
                        //     case "Ball":
                        //         mapObj.name = mapObj.tag + "(" + i + ", " + j + ")";
                        //         mapObj.transform.parent = Map12x12.transform;
                        //         mapObj.transform.localPosition = new Vector3(i, 0, j);
                        //         mapPack.map.Add(mapObj);
                        //         break;
                        //
                        //     case "Player":
                        //         mapObj.name = mapObj.tag + "(" + i + ", " + j + ")";
                        //         mapObj.transform.parent = Map12x12.transform;
                        //         mapObj.transform.localPosition = new Vector3(i, 0, j);
                        //         mapPack.map.Add(mapObj);
                        //         break;
                        //     default:
                        //         break;
                        // }
                    }

                    
                    
                }
            }
            mapList.Add(mapPack);
        }
        gameMgr.SetBuckesAnsBalls();
    }


    public void MapDestory(int lv)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] buckets = GameObject.FindGameObjectsWithTag("Bucket");
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < walls.Length; i++)
        {
            Destroy(walls[i]);
        }
        for (int i = 0; i < buckets.Length; i++)
        {
            Destroy(buckets[i]);
        }
        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }
        Destroy(player);
        mapList.Clear();

        int movec = gameMgr.moveCnt;
        int movebest = PlayerPrefs.GetInt("Round" + (gameMgr.curLv - 1).ToString(),0);
        if (movebest ==0)
        {
            PlayerPrefs.SetInt("Round" + (gameMgr.curLv - 1).ToString(),movec);
        }
        else
        {
            if (movec < movebest)
            {
                PlayerPrefs.SetInt("Round" + (gameMgr.curLv - 1).ToString(), movec);
            }
        }

        LoadMapData(lv);
        gameMgr.curCnt = 0;
        MakeMap();
        gameMgr.SetBuckesAnsBalls();
    }
    public void Reset(int lv)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] buckets = GameObject.FindGameObjectsWithTag("Bucket");
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < walls.Length; i++)
        {
            Destroy(walls[i]);
        }
        for (int i = 0; i < buckets.Length; i++)
        {
            Destroy(buckets[i]);
        }
        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }
        Destroy(player);
        mapList.Clear();

        LoadMapData(lv);
        gameMgr.curCnt = 0;
        MakeMap();
        gameMgr.SetBuckesAnsBalls();
    }

}
