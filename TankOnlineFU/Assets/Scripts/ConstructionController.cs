using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ConstructionController : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;

    public RuleTile[] tiles;

    public GameObject tank;

    private int index;
    private Vector3Int posTemp;
    private Vector2 cellSize;
    private Vector2Int gridSize;
    Vector3Int posMaterial;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        cellSize = new Vector2(0.32f, 0.32f);
        gridSize = new Vector2Int(12, 26);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = tank.transform.position;
        posMaterial = ConvertToGridPosition(pos);
        if (!posTemp.Equals(posMaterial))
        {
            index = 0;
        }
        posTemp = posMaterial;

        if (Input.GetKeyDown(KeyCode.X))
        {
            tank.GetComponent<BoxCollider2D>().enabled = false;
            tilemap.SetTile(posMaterial, null);
            tilemap.SetTile(posMaterial, tiles[index]);

            index++;
            if (index >= tiles.Length)
            {
                index = 0;
            }
        }
    }

    public Vector3Int ConvertToGridPosition(Vector3 position)
    {
        int gridX = (int)Mathf.FloorToInt(position.x / cellSize.x);
        int gridY = (int)Mathf.FloorToInt(position.y / cellSize.y);

        return new Vector3Int(gridX, gridY, 1);
    }
}
