using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContains : MonoBehaviour
{
    //This Class need to execute before Object Pooling
    [Header("ObjectPool")]
    public ObjectsPool objectsPool;
    [Header("GatherableMaterials")]
    public int hearths;
    public int treasures;
    public int upgradeKits;
    public int essenceOrbs;
    [Header("Obstacles")]
    public int walls;
    public int spinningSpikes;
    public int flameThrower;
    public int enemyCount;

    private void Awake()
    {
        //GatherableMaterials --- Element 5 To Element 8
        hearths = Random.Range(0, 2); // Element 5
        if(Random.Range(0,8) == 2)
        {
            treasures = Random.Range(1, 3);// Element 6
        }

        if(Random.Range(0,15) == 8)
        {
            upgradeKits = Random.Range(1, 3);// Element 7
        }
        essenceOrbs = Random.Range(15, 30); // Element 8
        if(Random.Range(0,5) == 4)
        {
            essenceOrbs = Random.Range(15, 23);
        }

        //Obstacles --- Element 9 To Element 12
        walls = Random.Range(1, 3); // Element 9
        spinningSpikes = Random.Range(5, 13); // Element 10
        flameThrower = Random.Range(10, 15); // Element 11
        enemyCount = Random.Range(3, 8); // Agressive Enemy Element 12
    
        objectsPool.pools[5].poolSize = hearths;
        objectsPool.pools[6].poolSize = treasures;
        objectsPool.pools[7].poolSize = upgradeKits;
        objectsPool.pools[8].poolSize = essenceOrbs;
        objectsPool.pools[9].poolSize = spinningSpikes;
        objectsPool.pools[10].poolSize = flameThrower;
        objectsPool.pools[11].poolSize = walls;
        objectsPool.pools[12].poolSize = enemyCount;
    }
}
