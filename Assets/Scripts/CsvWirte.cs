using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CsvWirte : MonoBehaviour
{
    public static CsvWirte inst;
    private void Awake() => inst = this;

    private void Start()
    {
        csvwrite(40);
    }

    public void csvwrite(int lv)
    {
        StreamWriter streamSave = new StreamWriter (Application.dataPath + "/Resources/Maps/Lv"+lv.ToString()+".csv");
        streamSave.WriteLine("0,1,2,3,4,5,6,7,8,9,10,11");
        streamSave.Flush();
        streamSave.Close();
    }
}
