using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShooting : MonoBehaviour
{
    [SerializeField] public ObjectsPool objectPool = null;
    // Player
    public GameObject player;
    [Header("SpawnPoints")]
    public Transform bulletPoint;
    public Transform magicAreaPoint;
    public Transform beamStartPoint;
    public Transform beamEndPoint;

    [Header("Skill Buttons")]
    public Button shootButton;
    public Button blastButton;
    public Button rainButton;
    public Button beamButton;
    [Header("Beam Properties")]
    [SerializeField]
    private GameObject beamStart;
    [SerializeField]
    private GameObject beam;
    [SerializeField]
    private GameObject beamEnd;
    [SerializeField]
    private LineRenderer line;

    private void Start()
    {
        GameManager.instance.isAvailableShoot = true;
        beamStart = Instantiate(beamStart);
        beamStart.SetActive(false);
        beamEnd = Instantiate(beamEnd);
        beamEnd.SetActive(false);
        beam = Instantiate(beam);
        beam.SetActive(false);
        line = beam.GetComponent<LineRenderer>();
        line.positionCount = 2;

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
            StartCoroutine(Disableobj(obj, 1.5f));
            StartCoroutine(DisableButton(shootButton, 0.15f));
        }
    }

    public void BlastSphere(int objectType)
    {
        if (GameManager.instance.isAvailableShoot && GameManager.instance.startGame)
        {
            var blastSphere = objectPool.GetPooledObject(objectType);
            blastSphere.transform.position = magicAreaPoint.position;
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.audioClips[0], 0.1f);
            StartCoroutine(Disableobj(blastSphere, 4f));
            StartCoroutine(DisableButton(blastButton, 3f));
        }
    }

    public void MagicRain(int objectType)
    {
        if (GameManager.instance.isAvailableShoot && GameManager.instance.startGame)
        {
            var magicRain = objectPool.GetPooledObject(objectType);
            magicRain.transform.position = magicAreaPoint.position;
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.audioClips[0], 0.1f);
            StartCoroutine(Disableobj(magicRain, 5f));
            StartCoroutine(DisableButton(rainButton, 8f));
        }
    }

    public void BeamShootOnPressed()
    {
        GameManager.instance.isMoveable = false;
        beamStart.transform.position = beamStartPoint.position;
        line.SetPosition(0, beamStartPoint.position);
        beamEnd.transform.position = beamEndPoint.position;
        line.SetPosition(1, beamEndPoint.position);
        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        beam.SetActive(true);
        beamStart.SetActive(true);
        beamEnd.SetActive(true);
    }

    public void BeamShootNonPressed()
    {
        GameManager.instance.isMoveable = true;
        beam.SetActive(false);
        beamStart.SetActive(false);
        beamEnd.SetActive(false);
    }


    // Object Disable
    IEnumerator Disableobj(GameObject obj, float timer)
    {
        yield return new WaitForSeconds(timer);
        obj.SetActive(false);
    }
    IEnumerator DisableButton(Button shootButton, float timer)
    {
        shootButton.interactable = false;
        yield return new WaitForSeconds(timer);
        shootButton.interactable = true;
    }

}
