using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Map : MonoBehaviour
{
    public int sizeX, sizeZ;

    public MapCell cellPrefab;
    public BuildingL1 buildingL1Prefab;

    public BuildingS1 buildingS1Prefab;
    public BuildingS2 buildingS2Prefab;
    public BuildingS3 buildingS3Prefab;

    public BuildingL2 buildingL2Prefab;
    public BuildingL2 buildingL3Prefab;
    public BuildingL2 buildingL4Prefab;

    public PlayerMove playerRobotPrefab;
    public PlayerMove enemyRobotPrefab;
    public PlayerMove playerRobot;
    public PlayerMove enemyRobot;

    public Road roadPrefab;
    public int buildingNumber;

    public GameObject gunTip;

    //LineRenderer line;

    public static MapCell[,] cells;
    


    public void Generate()
    {

        cells = new MapCell[sizeX, sizeZ];
        

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                CreateCell(x, z);
                
            }
        }
        playerRobot = Instantiate(playerRobotPrefab);
        playerRobot.transform.position = cells[10,0].transform.position;
        playerRobot.transform.position = playerRobot.transform.position + new Vector3(0.0f, 1.1f, 0.0f);
        playerRobot.transform.Rotate(0f, -90f, 0f);
        cells[10, 0].playerCell = true;

        enemyRobot = Instantiate(enemyRobotPrefab);
        enemyRobot.transform.position = cells[10, 20].transform.position;
        enemyRobot.transform.position = enemyRobot.transform.position + new Vector3(0.0f, 1.1f, 0.0f);
        enemyRobot.transform.Rotate (0f, 90f, 0f);
        cells[10, 20].enemyCell = true;

        //line = playerRobot.GetComponentInChildren<LineRenderer>();
        //line.enabled = false;
    }

    private void CreateCell(int x, int z)
    {
        MapCell newCell = Instantiate(cellPrefab) as MapCell;
        cells[x, z] = newCell;
        newCell.name = "Map Cell " + x + ", " + z;
        newCell.xCoordinate = x;
        newCell.zCoordinate = z;

        newCell.transform.position = new Vector3(x, 0f, z);
        if (x > 0 && z > 0 && 10 > (Random.Range(0, 100)) && x < 20 && z < 20)
        {
            int randSmall = (Random.Range(1, 4));
            switch (randSmall)
            {
                case 1:
                    BuildingS1 buildingS1Cell = Instantiate(buildingS1Prefab) as BuildingS1;
                    buildingS1Cell.transform.position = newCell.transform.position;
                    newCell.cellBuilding = 1;
                    break;
                case 2:
                    BuildingS2 buildingS2Cell = Instantiate(buildingS2Prefab) as BuildingS2;
                    buildingS2Cell.transform.position = newCell.transform.position;
                    newCell.cellBuilding = 1;
                    break;
                case 3:
                    BuildingS3 buildingS3Cell = Instantiate(buildingS3Prefab) as BuildingS3;
                    buildingS3Cell.transform.position = newCell.transform.position;
                    newCell.cellBuilding = 1;
                    break;

            }
        }
        else if (x > 3 && z > 3 && 10 > (Random.Range(0, 100)) && x < 16 && z < 16)
        {
            int randSmall = (Random.Range(1, 4));
            switch (randSmall)
            {
                case 1:
                    BuildingL2 buildingL2Cell = Instantiate(buildingL2Prefab) as BuildingL2;
                    buildingL2Cell.transform.position = newCell.transform.position;
                    newCell.cellBuilding = 2;
                    break;
                case 2:
                    BuildingL2 buildingL3Cell = Instantiate(buildingL3Prefab);
                    buildingL3Cell.transform.position = newCell.transform.position;
                    newCell.cellBuilding = 2;
                    break;
                case 3:
                    BuildingL2 buildingL4Cell = Instantiate(buildingL4Prefab);
                    buildingL4Cell.transform.position = newCell.transform.position;
                    newCell.cellBuilding = 2;
                    break;

            }
        }

        else if (x > 8 && x < 11 && z > 8 && z < 11)
        {
            BuildingL1 buildingL1Cell = Instantiate(buildingL1Prefab) as BuildingL1;
            
            buildingL1Cell.transform.position = new Vector3(x, 0f, z);
            newCell.cellBuilding = 3;
        }



    }
    public void MoveRobot(int x, int z)
    {

        print(cells[x, z].transform.position.x);
        print(playerRobot.transform.position.x);
        playerRobot.transform.position = cells[x, z].transform.position;
        playerRobot.transform.position = playerRobot.transform.position + new Vector3(0.0f, 1.1f, 0.0f);
    }

    public void MoveEnemyRobot(int x)
    {

        foreach (MapCell c in Map.cells)
        {
            if (c.enemyCell == true)
            {
                
                enemyRobot.transform.position = cells[x, 20].transform.position;
                c.enemyCell = false;
                cells[x, 20].enemyCell = true;
                enemyRobot.transform.position = enemyRobot.transform.position + new Vector3(0.0f, 1.1f, 0.0f);

            }
        }
    }




}
