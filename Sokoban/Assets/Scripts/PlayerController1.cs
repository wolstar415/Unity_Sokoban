using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{

    Ray ray;

    RaycastHit hit;
    RaycastHit hit2;
    public GameMgr gameMgr;
    public MapGenerator mapGenerator;
    public LayerMask layerMask;



    void Start()  // 처음 시작시 실행되는 함수입니다.
    {
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
        mapGenerator = GameObject.Find("Map12x12").GetComponent<MapGenerator>();

    }


    void Update() // 매 프레임마다 실행되는 함수입니다.
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            CheckOthers(Vector3.back);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            CheckOthers(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckOthers(Vector3.left);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CheckOthers(Vector3.right);

        }
    }

    public void CheckOthers(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(dir), out hit,1.0f,layerMask))
        {
            Transform tr = hit.collider.transform;
            //Debug.Log(tr.gameObject.name);
            switch (hit.collider.tag)
            {
                case "Ball":
                    
                    //
                    if (Physics.Raycast(tr.transform.position, tr.transform.TransformDirection(dir), out hit2, 1.0f))
                    {
                        switch (hit2.collider.tag)
                        {
                            case "Bucket":
                                if (hit2.transform.GetComponent<Bucket_Check>().ball==null) // 두번째블록이 바구니인데 볼이없다!
                                {
                                    if (tr.GetComponent<Ball_Check>().Bucket != null) // 첫번째블록의 볼이 바구니안이라면!
                                    {
                                        tr.GetComponent<Ball_Check>().Bucket.GetComponent<Bucket_Check>().ball = null; //기존 바구미의 볼은 사라져야지

                                        tr.GetComponent<Ball_Check>().Bucket = hit2.transform.gameObject; // 움직인 볼의 바구니 바꿔줘야지!
                                        hit2.transform.GetComponent<Bucket_Check>().ball = tr.gameObject; // 움직인 볼의 바구니의 볼도 현재로 바궈야지
                                        tr.transform.Translate(dir);
                                        transform.Translate(dir);
                                        
                                        
                                    }
                                    else //첫번째블록의 볼이 바구니안이 아니라면!
                                    {
                                        //tr.GetComponent<Ball_Check>().Bucket.GetComponent<Bucket_Check>().ball = null;
                                        tr.GetComponent<Ball_Check>().Bucket = hit2.transform.gameObject;
                                        hit2.transform.GetComponent<Bucket_Check>().ball = tr.gameObject;
                                        tr.transform.Translate(dir);
                                        transform.Translate(dir);
                                        gameMgr.curCnt++;
                                        Bucketup();
                                    }
                                        
                                }
                                else
                                {
                                    // 있다면 움직이면 큰일남!;
                                }
                                    
                                
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (tr.GetComponent<Ball_Check>().Bucket != null) 
                        {
                            gameMgr.curCnt--;
                            tr.GetComponent<Ball_Check>().Bucket.GetComponent<Bucket_Check>().ball = null;
                            tr.GetComponent<Ball_Check>().Bucket = null;
                        }
                        tr.transform.Translate(dir);
                        transform.Translate(dir);

                    }
                        break;
                case "Wall":
                    break;
                case "Bucket":
                    break;

                default:
                    break;
            }
        }
        else
        {
            transform.Translate(dir);

        }
    }

    public void Bucketup()
    {


        if (gameMgr.curCnt==gameMgr.maxCnt)

        {
            gameMgr.curLv++;
            if (gameMgr.curLv>= gameMgr.maxCnt)
            {
                Debug.Log("Game Clear");

            }
            else
            {
            mapGenerator.MapDestory(gameMgr.curLv);

            }

        }
    }


}
