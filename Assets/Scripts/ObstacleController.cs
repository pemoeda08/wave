using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] obstaclesArray;

    int playerDistanceIndex = -1;

    int obstacleCount;
    int obstacleIndex = 0;
    int distanceToNext = 30;

    private static System.Random random = new System.Random();

    void Start()
    {
        obstacleCount = obstaclesArray.Length;
        SpawnObstacle();
    }

    void Update()
    {
        int playerDistance = (int)(player.transform.position.y / (distanceToNext));
        // Debug.Log("playerDistance: " + playerDistance);

        if(playerDistanceIndex != playerDistance)
        {
            SpawnObstacle();
            playerDistanceIndex = playerDistance;
        }
    }

    public void SpawnObstacle()
    {
        int randomInt = random.Next(0, obstacleCount);
        GameObject newObstacle = Instantiate(obstaclesArray[randomInt], new Vector3(0, obstacleIndex * distanceToNext), Quaternion.identity);
        newObstacle.transform.SetParent(transform);
        obstacleIndex++;
    }

    public void ResetObstacles()
    {
        GameObject[] allChildren = new GameObject[transform.childCount];
        int i = 0;
        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            Destroy(child.gameObject);
        }


        obstacleIndex = 0;
        playerDistanceIndex = -1;
    }
}
