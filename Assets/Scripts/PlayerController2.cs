using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(dir), out hit,1.0f))
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
                                tr.transform.Translate(dir);
                                transform.Translate(dir);
                                gameMgr.CheckBall();


                                break;

                            default:
                                tr.transform.Translate(dir);
                                transform.Translate(dir);
                                gameMgr.CheckBall();
                                break;
                        }
                        
                    }
                    else
                    {

                        tr.transform.Translate(dir);
                        transform.Translate(dir);
                        gameMgr.CheckBall();
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



}
