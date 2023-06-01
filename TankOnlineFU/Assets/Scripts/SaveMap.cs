using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveMap : MonoBehaviour
{
    [SerializeField]
    Button buttonSave;
    GameObject[] listSteel;
    GameObject[] listTree;
    GameObject[] listBrick;
    GameObject[] listWater;
    public void saveGame()
    {
        string filePath = Application.dataPath + "/savemap.txt";
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, String.Empty);

            listSteel = GameObject.FindGameObjectsWithTag("Wall_Steel");
            listTree = GameObject.FindGameObjectsWithTag("Tree");
            listBrick = GameObject.FindGameObjectsWithTag("Brick");
            listWater = GameObject.FindGameObjectsWithTag("Water");
            string txt = "";
            txt += $"Wall_Steel: (";
            StreamWriter writer = new StreamWriter(filePath, true);
            foreach (GameObject steel in listSteel)
            {
                txt += "(" + steel.transform.position.x + " " + steel.transform.position.y + " " + steel.transform.localScale.x + ")";
            }
            txt += $")\nTree: (";
            foreach (GameObject tree in listTree)
            {
                txt += "(" + tree.transform.position.x + " " + tree.transform.position.y + " " + tree.transform.localScale.x + ")";
            }
            txt += $")\nBrick: (";
            foreach (GameObject brick in listBrick)
            {
                txt += "(" + brick.transform.position.x + " " + brick.transform.position.y + " " + brick.transform.localScale.x + ")";
            }
            txt += $")\nWater: (";
            foreach (GameObject water in listWater)
            {
                txt += "(" + water.transform.position.x + " " + water.transform.position.y + " " + water.transform.localScale.x + ")";
            }
            writer.Write(txt);
            writer.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }
}
