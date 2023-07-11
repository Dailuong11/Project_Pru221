using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMap : MonoBehaviour
{
    [Serializable]
    public class MapData
    {
        public List<Vector3> listSteel;
        public List<Vector3> listTree;
        public List<Vector3> listBrick;
        public List<Vector3> listWater;
        public List<Vector3> Appear;
    }

    [SerializeField]
    private GameObject[] lSteel;
    [SerializeField]
    private GameObject[] lTree;
    [SerializeField]
    private GameObject[] lBrick;
    [SerializeField]
    private GameObject[] lWater;
    [SerializeField]
    private GameObject[] appear;
    public void SaveNewMap()
    {
        MapData mapData = new MapData();
        lSteel = GameObject.FindGameObjectsWithTag("Wall_Steel");
        lTree = GameObject.FindGameObjectsWithTag("Tree");
        lBrick = GameObject.FindGameObjectsWithTag("Brick");
        lWater = GameObject.FindGameObjectsWithTag("Water");

        mapData.listSteel = new List<Vector3>();
        foreach (GameObject obj in lSteel)
        {
            mapData.listSteel.Add(obj.transform.position);
        }

        mapData.listTree = new List<Vector3>();
        foreach (GameObject obj in lTree)
        {
            mapData.listTree.Add(obj.transform.position);
        }

        mapData.listBrick = new List<Vector3>();
        foreach (GameObject obj in lBrick)
        {
            mapData.listBrick.Add(obj.transform.position);
        }

        mapData.listWater = new List<Vector3>();
        foreach (GameObject obj in lWater)
        {
            mapData.listWater.Add(obj.transform.position);
        }

        string json = JsonUtility.ToJson(mapData);
        File.WriteAllText("Assets//map.json", json);
        SceneManager.LoadScene("Menu");
    }
}
