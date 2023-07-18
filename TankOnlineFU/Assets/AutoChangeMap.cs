using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoChangeMap : MonoBehaviour
{
    [Serializable]
    public class MapData
    {
        public List<Vector3> listSteel;
        public List<Vector3> listTree;
        public List<Vector3> listBrick;
        public List<Vector3> listWater;
    }

    [SerializeField]
    private GameObject steelPrefab;
    [SerializeField]
    private GameObject treePrefab;
    [SerializeField]
    private GameObject brickPrefab;
    [SerializeField]
    private GameObject waterPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int id = PlayerPrefs.GetInt("id");
        int Point = PlayerPrefs.GetInt("Point");
        if (Point == id * 2)
        {
            if (id == 4)
            {
                SceneManager.LoadScene("Win");
            }
            Point = 0;
            PlayerPrefs.SetInt("Point", Point);
            id++;
            DestroyAllObjects();
            string filePath = "Assets//map" + id + ".json";
            PlayerPrefs.SetInt("id", id);
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

    private void DestroyAllObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].gameObject.CompareTag("Wall_Steel")
                || allObjects[i].gameObject.CompareTag("Brick")
                || allObjects[i].gameObject.CompareTag("Tree")
                || allObjects[i].gameObject.CompareTag("Water"))
            {
                Destroy(allObjects[i].gameObject);
            }
        }
    }
}
