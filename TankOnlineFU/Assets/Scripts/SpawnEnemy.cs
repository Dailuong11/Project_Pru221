using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SpawnEnemy : MonoBehaviour
{
    public Tilemap tilemap;
    public RuleTile tileEnemy;


    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;
    private Vector3 cellSize;
    private Timer timer;
    private float timeSpawnEnemy = 3;
    private List<Vector3Int> listPos;

    void Start()
    {
        cellSize = tilemap.cellSize;
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = timeSpawnEnemy;
        timer.run();
        saveScreenSize();
    }

    void Update()
    {
        if (timer.Finished)
        {
            bool isContinue = false;
            listPos = new List<Vector3Int>();

            Scene currentScene = SceneManager.GetActiveScene();
            GameObject[] rootObjects = currentScene.GetRootGameObjects();
            foreach (GameObject rootObject in rootObjects)
            {
                if (rootObject.tag == "Brick"
                    || rootObject.tag == "Tree"
                    || rootObject.tag == "Water"
                    || rootObject.tag == "Wall_Steel")
                {
                    Vector3Int materialPos = ConvertToGridPosition(rootObject.transform.position);
                    listPos.Add(materialPos);
                }
            }

            do
            {
                Vector3 temp = new Vector3(Random.Range(screenLeft, screenRight), Random.Range(screenBottom, screenTop));
                Vector3Int randomPosItem = ConvertToGridPosition(temp);
                bool isExist = false;

                foreach (Vector3Int item in listPos)
                {
                    if (item.x == randomPosItem.x && item.y == randomPosItem.y)
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist == false)
                {
                    tilemap.SetTile(randomPosItem, tileEnemy);
                    isContinue = false;
                }
                else
                {
                    isContinue = true;
                }
            } while (isContinue);
            timer.Duration = timeSpawnEnemy;
            timer.run();
        }
    }

    private void saveScreenSize()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;
    }

    public Vector3Int ConvertToGridPosition(Vector3 position)
    {
        int gridX = (int)Mathf.FloorToInt(position.x / cellSize.x);
        int gridY = (int)Mathf.FloorToInt(position.y / cellSize.y);

        return new Vector3Int(gridX, gridY, 1);
    }
}
