using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Map mapPrefab;

    private Map mapInstance;
    private MapCell mapCell;
    private MapCell lastPlayerMove;
    private MapCell lastEnemyMove;
    public Vector3 mouseClickPosition;
    public int moveDistance = 6;
    public int enemyMoveDistance = 4;
    public int playerShootDistance = 12;
    public int enemyShootDistance = 8;
    public int movesRemaining = 3;
    public int enemyMovesRemaining = 3;

    public static int enemyHealth = 2;
    
    public static int playerHealth = 4;
    public int randomMoveDistance;

    public ParticleSystem laser;

    public float distance;
    public float shotDistance;

    LineRenderer line;

    public GameObject mapCellClicked;
    private void Start()
    {
        BeginGame();
        //line = mapInstance.playerRobot.GetComponentInChildren<LineRenderer>();
        //line.enabled = false;

        StartCoroutine(TwoSecondWait());
        movesRemaining = 3;
        laser = mapInstance.playerRobot.GetComponentInChildren<ParticleSystem>();
        
        lastEnemyMove = Map.cells[10, 20];
        lastPlayerMove = Map.cells[10, 0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
        if (movesRemaining == 0)
        {
            EnemyMove();
        }
        if (Input.GetMouseButtonDown(0))
        {
            PlayerMove();
        }
        if (enemyHealth == 0)
        {
            GameOver();
        }
    }
    public void PlayerMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            
            mouseClickPosition = hit.transform.position;
            MapCell clickedCell = Map.cells[Mathf.RoundToInt(mouseClickPosition.x),
            Mathf.RoundToInt(mouseClickPosition.z)];

            if (clickedCell.enemyCell == true)
            {
                lastEnemyMove = clickedCell;
                FireAtEnemy();
            }
            else
            { 
            foreach (MapCell c in Map.cells)
            {
                if (c.playerCell == true && clickedCell.cellBuilding == 0)
                {
                    
                    distance = GetDistance(clickedCell.xCoordinate, clickedCell.zCoordinate,
                c.xCoordinate, c.zCoordinate);
                    


                    if (distance < moveDistance && movesRemaining != 0)
                    {
                        
                        mapInstance.MoveRobot(clickedCell.xCoordinate, clickedCell.zCoordinate);
                        c.playerCell = false;
                        clickedCell.playerCell = true;
                        print(movesRemaining--);
                        lastPlayerMove = clickedCell;
                        //able to move between 2 and 4 times instead of 3
                    }
                }

                }
            }
        }
    }
    public void FireAtEnemy()
    {
        print(shotDistance = GetDistance(lastEnemyMove.xCoordinate, lastEnemyMove.zCoordinate,
        lastPlayerMove.xCoordinate, lastPlayerMove.zCoordinate));

        if (shotDistance < playerShootDistance)
        {
        mapInstance.playerRobot.transform.LookAt(mapInstance.enemyRobot.transform);
        mapInstance.playerRobot.transform.Rotate(0f, -90f, 0f);
        laser.Play();
        TwoSecondWait();
        enemyHealth--;
        }
        EnemyMove();
    }


    //need ai system.
    public void EnemyMove()
    {
        //shotDistance = GetDistance(lastEnemyMove.xCoordinate, lastEnemyMove.zCoordinate,
        //lastPlayerMove.xCoordinate, lastPlayerMove.zCoordinate);
        randomMoveDistance = Random.Range(15, 5);
        mapInstance.MoveEnemyRobot(randomMoveDistance);
        movesRemaining = 3;
    }
    public void FireWeapon()
    {
        
    }

    //public void FireLaser()
    //{
    //    line.enabled = true;

    //    while (Input.GetButtonDown("Fire1"))//need to fix...need to find way to only reduce enemy health by one and increase length
    //        //of laser animation
    //    {

    //        Ray ray = new Ray(transform.position, transform.forward);
    //        RaycastHit hit;

    //        line.SetPosition(0, ray.origin);

    //        if (Physics.Raycast(ray, out hit, 100))
    //        {

    //            line.SetPosition(1, hit.point);
    //            if (hit.collider.tag == "enemy")
    //            {
    //                TwoSecondWait();
    //                enemyHealth = enemyHealth - 1;
    //                print("Enemy health is " + enemyHealth);

    //            }
    //        }

    //        else
    //            line.SetPosition(1, ray.GetPoint(100));




    //    }

    //    line.enabled = false;
    //}
    IEnumerator TwoSecondWait()
    {
        print(Time.time);
        yield return new WaitForSeconds(2);
        print(Time.time);
    }



    public static float GetDistance(float point1x, float point1z, float point2x, float point2z)
    {
        //pythagorean theorem c^2 = a^2 + b^2
        //thus c = square root(a^2 + b^2)
        float a = (point2x - point1x);
        float b = (point2z - point1z);

        return Mathf.Sqrt(a * a + b * b);
    }

    private void BeginGame()
    {
        mapInstance = Instantiate(mapPrefab) as Map;
        mapInstance.Generate();

    }

    private void RestartGame()
    {
        Destroy(mapInstance.gameObject);
        BeginGame();
    }
    public void GameOver()
    {
        Destroy(mapInstance);
        BeginGame();
    }
   
    
}
