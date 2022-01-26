using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_c : MonoBehaviour
{
    public PlayerController controller;
    public void moveb1()
    {
        controller=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        controller.moveb1();
    }
    public void moveb2()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        controller.moveb2();
    }
    public void moveb3()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        controller.moveb3();
    }
    public void moveb4()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        controller.moveb4();
    }
}
