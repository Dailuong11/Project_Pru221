using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LoadMap : MonoBehaviour
{
    [SerializeField]
    private GameObject steelPrefab;
    [SerializeField]
    private GameObject treePrefab;
    [SerializeField]
    private GameObject brickPrefab;
    [SerializeField]
    private GameObject waterPrefab;


    [Serializable]
    public class MapData
    {
        public List<Vector3> listSteel;
        public List<Vector3> listTree;
        public List<Vector3> listBrick;
        public List<Vector3> listWater;
    }
    void Start()
    {
        LoadMapFromFile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadMapFromFile()
    {
        string filePath = "Assets//map.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            MapData mapData = JsonUtility.FromJson<MapData>(json);

            foreach (Vector3 steelPos in mapData.listSteel)
            {
                Instantiate(steelPrefab, steelPos, Quaternion.identity);
            }

            foreach (Vector3 treePos in mapData.listTree)
            {
                Instantiate(treePrefab, treePos, Quaternion.identity);
            }

            foreach (Vector3 brickPos in mapData.listBrick)
            {
                Instantiate(brickPrefab, brickPos, Quaternion.identity);
            }

            foreach (Vector3 waterPos in mapData.listWater)
            {
                Instantiate(waterPrefab, waterPos, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogError("Map file not found!");
        }
    }
}
