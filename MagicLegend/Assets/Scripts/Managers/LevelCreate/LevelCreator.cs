using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class LevelCreator : MonoBehaviour
{
    [Header("ObjectPool")]
    public ObjectsPool objectsPool;
    [Header("GatherablesTransforms")]
    public Transform[] gatherablesTransforms;
    [Header("ObstaclesTransforms")]
    public Transform[] obstaclesTransforms;

    private List<GameObject> gatherables = new List<GameObject>();
    private List<GameObject> obstacles = new List<GameObject>();
    private List<Transform> gatherablesLocations = new List<Transform>();
    private List<Transform> obstaclesLocations = new List<Transform>();

    private void Start()
    {
        // Gatherables'larý al
        for (int i = 5; i < 9; i++)
        {
            for (int j = 0; j < objectsPool.pools[i].poolSize; j++)
            {
                gatherables.Add(objectsPool.GetPooledObjectForLevel(i));
            }
        }

        // Obstacles'larý al ve etkinleþtir
        for (int i = 9; i < 13; i++)
        {
            for (int j = 0; j < objectsPool.pools[i].poolSize; j++)
            {
                GameObject obstacle = objectsPool.GetPooledObjectForLevel(i);
                obstacles.Add(obstacle);
            }
        }
        // Locations' larý al
        for (int i = 0; i < gatherablesTransforms.Length; i++)
        {
            gatherablesLocations.Add(gatherablesTransforms[i]);
        }

        for (int i = 0; i < obstaclesTransforms.Length; i++)
        {
            obstaclesLocations.Add(obstaclesTransforms[i]);
        }

        // Gatherables'larý rastgele konumlara yerleþtir
        for (int i = 0; i < gatherablesTransforms.Length; i++)
        {
            if (gatherables.Count < 1)
            {
                break;
            }
            GameObject currentGatherable = gatherables[Random.Range(0, gatherables.Count)];
            currentGatherable.SetActive(true);
            Transform randomTransform = gatherablesLocations[Random.Range(0, gatherablesLocations.Count)];
            currentGatherable.transform.position = randomTransform.position;
            gatherablesLocations.Remove(randomTransform);
            gatherables.Remove(currentGatherable);
        }

        // Obstacles'larý rastgele konumlara yerleþtir
        for (int i = 0; i < obstaclesTransforms.Length; i++)
        {
            if (obstacles.Count < 1)
            {
                break;
            }
            GameObject currentObstacle = obstacles[Random.Range(0, obstacles.Count)];
            currentObstacle.SetActive(true);
            Transform randomTransform = obstaclesLocations[Random.Range(0, obstaclesLocations.Count)];
            currentObstacle.transform.position = randomTransform.position;
            currentObstacle.transform.SetParent(randomTransform.transform);

            print(int.Parse(randomTransform.name.Filter(false, true, false, false, false)));
            obstaclesLocations.Remove(randomTransform);
            obstacles.Remove(currentObstacle);
        }

    }
    /*
    private void CreateGatherables()
    {
        int tempCountOfGatherables = gatherables.Count;
        for (int i = 0; i < tempCountOfGatherables && i < gatherablesTransforms.Length; i++)
        {
            //Select Random Gatherable and Location
            int selectedGatherable = Random.Range(0, gatherables.Count);
            int selectedLocation = Random.Range(0, gatherablesTransformsList.Count);
            GameObject currentGatherableGameobject = (GameObject)gatherables[selectedGatherable];
            //print(currentGatherableGameobject.name);
            //if (currentGatherableGameobject.CompareTag("Hearth")) // Can code like this for each gatherable

            //print(((Transform)gatherablesTransformsList[selectedLocation]).position);
            currentGatherableGameobject.transform.position = ((Transform)gatherablesTransformsList[selectedLocation]).position;
            currentGatherableGameobject.SetActive(true);
            //Discard from List gatherable and location for Gatherables
            gatherables.RemoveAt(selectedGatherable);
            gatherablesTransformsList.RemoveAt(selectedLocation);

        }
    }

    //First Select Obstacle and Location
    //Then Check if the location is available
    //If not available select another location
    //If tryCounter is greater than 50, break the loop
    //If tryCounter is greater than 50, continue the loop
    //If location is available, place the obstacle
    //Discard from List obstacle and location for Obstacles


    private void CreateObstacles()
    {
        int tempCountOfObstacles = obstacles.Count;
        for (int i = 0; i < tempCountOfObstacles; i++)
        {
            //Select Random Obstacle and Location
            int selectedObstacle = Random.Range(0, obstacles.Count - i);
            int selectedLocation = Random.Range(0, obstaclesTransformsList.Count);
            while (occupiedObstaclesLocations[selectedLocation] != 1)
            {
                selectedLocation = Random.Range(0, obstaclesTransformsList.Count);
            }
            GameObject currentObstacleGameobject = (GameObject)obstacles[selectedObstacle];

            if (currentObstacleGameobject.CompareTag("SpinnedSpike"))
            {
                currentObstacleGameobject.SetActive(true);
                tryCounter = 0;

                int tempSelectedLocation;
                while (true)
                {
                    tempSelectedLocation = selectedLocation;
                    while (true)
                    {
                        if (tempSelectedLocation % 3 == 0)
                        {
                            break;
                        }
                        tempSelectedLocation--;
                    }
                    if (occupiedObstaclesLocations[tempSelectedLocation] != 1 && occupiedObstaclesLocations[tempSelectedLocation + 1] != 1 && occupiedObstaclesLocations[tempSelectedLocation + 2] != 1)
                    {
                        break;
                    }
                    else if (tryCounter > 50)
                    {
                        break;

                    }
                    else
                    {
                        selectedLocation = Random.Range(0, obstaclesTransformsList.Count);
                    }
                    tryCounter++;
                }
                if (tryCounter > 50)
                {
                    continue;
                }

                selectedLocation = tempSelectedLocation;
                for (int j = 0; j < 3; j++)
                {
                    if (selectedLocation + j >= occupiedObstaclesLocations.Length)
                    {
                        break;
                    }
                    print(selectedLocation + j);
                    occupiedObstaclesLocations[selectedLocation + j] = 1;
                }
                obstacles.RemoveAt(selectedObstacle);
                currentObstacleGameobject.transform.transform.position = obstaclesTransforms[selectedLocation].position + new Vector3(0, 2.64f, 0);
            }
            else if (currentObstacleGameobject.CompareTag("FlameThrower"))
            {
                obstacles.RemoveAt(selectedObstacle);
            }
            else if (currentObstacleGameobject.CompareTag("Wall"))
            {
                obstacles.RemoveAt(selectedObstacle);
            }
            else if (currentObstacleGameobject.CompareTag("Enemy"))
            {
                obstacles.RemoveAt(selectedObstacle);
            }
            else
            {
                print("No Tag Found");
                return;
            }

        }
    }*/

}
