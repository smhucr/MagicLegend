using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShooting : MonoBehaviour
{
    [SerializeField] public ObjectsPool objectPool = null;

    // SpawnPoint
    public Transform bulletPoint;
    // Player
    public GameObject player;
    //Fire Button
    public Button shootButton;

    private void Start()
    {
        GameManager.instance.isAvailableShoot = true;
    }

    private void FixedUpdate()
    {
        bulletPoint.rotation = player.transform.rotation;
    }

    public void Shoot(int objectType)
    {
        if (GameManager.instance.isAvailableShoot && GameManager.instance.startGame)
        {
            var obj = objectPool.GetPooledObject(objectType);
            obj.transform.rotation = bulletPoint.rotation;
            obj.transform.position = bulletPoint.position;
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.audioClips[0], 0.1f);
            StartCoroutine(Disableobj(obj));
            StartCoroutine(DisableButton(shootButton));
        }
    }

    // Object Disable
    IEnumerator Disableobj(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        obj.SetActive(false);
    }
    IEnumerator DisableButton(Button shootButton)
    {
        shootButton.interactable = false;
        yield return new WaitForSeconds(0.15f);
        shootButton.interactable = true;
    }

}
