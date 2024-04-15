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


    private ArrayList gatherables = new ArrayList();
    private ArrayList obstacles = new ArrayList();
    private ArrayList gatherablesTransformsList = new ArrayList();
    //private ArrayList obstaclesTransformsList = new ArrayList();
    public int[] occupiedObstaclesLocations;
    //GatherableMaterials --- Element 5 To Element 8
    //Obstacles --- Element 9 To Element 12
    private void Start()
    {

        //Get Gatherables
        for (int i = 5; i < 9; i++)
        {
            for (int j = 0; j < objectsPool.pools[i].poolSize; j++)
            {

                gatherables.Add(objectsPool.GetPooledObjectForLevel(i));
            }
        }
        //Get Obstacles
        for (int i = 9; i < 13; i++)
        {
            for (int j = 0; j < objectsPool.pools[i].poolSize; j++)
            {
                obstacles.Add(objectsPool.GetPooledObjectForLevel(i));
            }
        }

        for (int i = 0; i < gatherablesTransforms.Length; i++)
        {
            gatherablesTransformsList.Add(gatherablesTransforms[i]);
        }

        /*for (int i = 0; i < obstaclesTransforms.Length; i++)
        {
            obstaclesTransformsList.Add(obstaclesTransforms[i]);
        }*/
        occupiedObstaclesLocations = new int[obstaclesTransforms.Length];
        CreateGatherables();
        CreateObstacles();
    }

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

    private void CreateObstacles()
    {
        int tempCountOfObstacles = obstacles.Count;
        for (int i = 0; i < tempCountOfObstacles && i < obstaclesTransforms.Length; i++)
        {
            //Select Random Obstacle and Location
            int selectedObstacle = Random.Range(0, obstacles.Count);
            int selectedLocation = Random.Range(0, occupiedObstaclesLocations.Length);
            while (occupiedObstaclesLocations[selectedLocation] != 0)
            {
                selectedLocation = Random.Range(0, occupiedObstaclesLocations.Length);
            }
            GameObject currentObstacleGameobject = (GameObject)obstacles[selectedObstacle];

            if (currentObstacleGameobject.CompareTag("SpinnedSpike"))
            {
                int tempSelectedLocation = selectedLocation;
                currentObstacleGameobject.SetActive(true);

                while (true)
                {
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
                    else
                    {
                        selectedLocation = Random.Range(0, occupiedObstaclesLocations.Length);
                    }
                }

                tempSelectedLocation = selectedLocation;
                if (tempSelectedLocation % 3 == 0)
                {
                    selectedLocation = tempSelectedLocation;
                    for (int j = 0; j < 3; j++)
                    {
                        occupiedObstaclesLocations[selectedLocation + j] = 1;

                    }
                    obstacles.RemoveAt(selectedObstacle);
                }
                else
                {
                    while (true)
                    {
                        if (tempSelectedLocation % 3 == 0)
                        {
                            selectedLocation = tempSelectedLocation;
                            break;
                        }
                        tempSelectedLocation--;
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        occupiedObstaclesLocations[selectedLocation + j] = 1;

                    }
                    obstacles.RemoveAt(selectedObstacle);
                }
                //currentObstacleGameobject.transform.position = ((Transform)obstaclesTransformsList[selectedLocation]).position;
                currentObstacleGameobject.transform.transform.position = obstaclesTransforms[selectedLocation].position + new Vector3(0, 2.64f, 0);
            }
            else if (currentObstacleGameobject.CompareTag("FlameThrower"))
            {
                currentObstacleGameobject.SetActive(true);
                int tempSelectedLocation = selectedLocation;

                while (tempSelectedLocation > 0)
                {
                    if (tempSelectedLocation % 3 == 0)
                    {
                        break;
                    }
                    tempSelectedLocation--;
                    print(tempSelectedLocation);
                    if (tempSelectedLocation < 0)
                        break;
                }

                while (true)
                {
                    int countOfObstacles = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (occupiedObstaclesLocations[tempSelectedLocation + j] == 1)
                        {
                            countOfObstacles++;
                        }
                    }
                    if (countOfObstacles >= 2)
                    {
                        selectedLocation = Random.Range(0, occupiedObstaclesLocations.Length);
                        tempSelectedLocation = selectedLocation;
                        while (tempSelectedLocation > 0)
                        {
                            if (tempSelectedLocation % 3 == 0)
                            {
                                break;
                            }
                            tempSelectedLocation--;
                            print(tempSelectedLocation);
                            if (tempSelectedLocation < 0)
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }


                }

                for (int j = 0; j < 3; j++)
                    occupiedObstaclesLocations[selectedLocation] = 1; // BROKEN CODE
                obstacles.RemoveAt(selectedObstacle);

                currentObstacleGameobject.transform.position = obstaclesTransforms[selectedLocation].position;
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
    }

}
