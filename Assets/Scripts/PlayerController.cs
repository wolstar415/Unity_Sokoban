using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Ray ray;

    RaycastHit hit;
    RaycastHit hit2;
    public LayerMask layerMask;
    public GameObject character;


    void Update() // 매 프레임마다 실행되는 함수입니다.
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CheckOthers(Vector3.back);
            character.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CheckOthers(Vector3.forward);
            character.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckOthers(Vector3.left);
            character.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CheckOthers(Vector3.right);
            character.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }

    public void CheckOthers(Vector3 dir)
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(dir), out hit, 1.0f, layerMask))
            //키보드로 이벤트를 받은 방향으로 레이캐스트를 쏴서 확인합니다.
        {
            Transform tr = hit.collider.transform;
            //히트된 Transform 를 저장합니다.
            switch (hit.collider.tag)
            {
                case "Ball":
                //감지된 오브젝트가 볼일경우
                    
                    if (Physics.Raycast(tr.transform.position, tr.transform.TransformDirection(dir), out hit2, 1.0f))
                        //두칸뒤에있는 오브젝트를 감지합니다.
                    {
                        switch (hit2.collider.tag)
                        {
                            case "Bucket":
                                //바구니
                                if (hit2.transform.GetComponent<Bucket_Check>().ball == null) // 두번째블록이 바구니인데 볼이 없을경우
                                {
                                    if (tr.GetComponent<Ball_Check>().Bucket != null) // 첫번째블록의 볼이 바구니안이라면!
                                    {
                                        tr.GetComponent<Ball_Check>().Bucket.GetComponent<Bucket_Check>().ball =
                                            null; //기존 바구니의 볼은 밀리니까 사라지게 합니다

                                        tr.GetComponent<Ball_Check>().Bucket =
                                            hit2.transform.gameObject; // 볼에게 두번째 바구니를 설정합니다.
                                        hit2.transform.GetComponent<Bucket_Check>().ball =
                                            tr.gameObject; // 두번째 바구니에게도 볼 설정합니다.
                                        tr.transform.Translate(dir); // 볼 이동
                                        transform.Translate(dir); // 캐릭터 이동
                                        GameMgr.inst.movemove(); //UI 움직임+
                                    }
                                    else //첫번째블록의 볼이 바구니안이 아니라면!
                                    {
                                        tr.GetComponent<Ball_Check>().Bucket = hit2.transform.gameObject; //바구니 설정
                                        hit2.transform.GetComponent<Bucket_Check>().ball = tr.gameObject; // 볼 설정
                                        tr.transform.Translate(dir); // 볼 이동
                                        transform.Translate(dir); // 캐릭터 이동
                                        GameMgr.inst.movemove();  //UI 움직임+
                                        GameMgr.inst.curCnt++; //바구니안에 볼이 들어감
                                        Bucketup(); // 게임이 끝났는지 체크합니다.
                                    }
                                }

                                break;

                            default:
                                break;
                        }
                    }
                    else
                    //두번째 레이캐스트에 아무것도 없을경우
                    {
                        if (tr.GetComponent<Ball_Check>().Bucket != null)
                        {
                            GameMgr.inst.curCnt--; // 바구니 체크 감소
                            tr.GetComponent<Ball_Check>().Bucket.GetComponent<Bucket_Check>().ball = null;
                            tr.GetComponent<Ball_Check>().Bucket = null;
                        }

                        tr.transform.Translate(dir);
                        transform.Translate(dir);
                        GameMgr.inst.movemove();
                        //이동합니다
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
            GameMgr.inst.movemove();
            transform.Translate(dir);
            //이동
        }
    }

    public void Bucketup()
    {
        if (GameMgr.inst.curCnt == GameMgr.inst.maxCnt)

        {
            GameMgr.inst.curLv++;
            if (GameMgr.inst.curLv > GameMgr.inst.maxLv)
            {
                GameMgr.inst.curLv = GameMgr.inst.maxLv;
                Debug.Log("Game Clear");
            }
            else
            {
                GameMgr.inst.mapGenerator.MapNext(GameMgr.inst.curLv);
            }
        }
    }
}