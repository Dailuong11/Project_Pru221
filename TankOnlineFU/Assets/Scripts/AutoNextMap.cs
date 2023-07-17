using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoNextMap : MonoBehaviour
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
		id += 1;
		PlayerPrefs.SetInt("id", id);
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemies.Length == 0)
		{
			DestroyAllObjects();
			string filePath = "Assets//map" + 2 + ".json";
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
			// Xóa đối tượng nếu nó không bị hủy và không phải là đối tượng EnemyManager hiện tại
			if (allObjects[i].gameObject != gameObject && !allObjects[i].gameObject.CompareTag("Enemy"))
			{
				Destroy(allObjects[i].gameObject);
			}
		}
	}
}
