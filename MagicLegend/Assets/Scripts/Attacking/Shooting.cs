using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] public ObjectsPool objectPool = null;
    //Manager
    public GameManager gameManager;
    // SpawnPoint
    public Transform bulletPoint;
    // Player
    public GameObject player;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.isAvailableShoot = true;
        StartCoroutine(Shoot(0));
    }

    private void FixedUpdate()
    {
        bulletPoint.rotation = player.transform.rotation;
    }

    public IEnumerator Shoot(int objectType)
    {
        //Spawn New Object
        if (gameManager.isAvailableShoot && gameManager.startGame && !gameManager.isGameOver)
        {
            var obj = objectPool.GetPooledObject(objectType);
            obj.transform.rotation = bulletPoint.rotation;
            obj.transform.position = bulletPoint.position;
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.audioClips[0], 0.1f);
            StartCoroutine(Disableobj(obj));
        }
        yield return new WaitForSeconds(GameManager.instance.fireRate);
        StartCoroutine(Shoot(objectType));

    }
    // Object Disable
    IEnumerator Disableobj(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        obj.SetActive(false);
    }
}
