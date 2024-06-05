using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShooting : MonoBehaviour
{
    [SerializeField] public ObjectsPool objectPool = null;
    [SerializeField] public GameManager gameManager;
    // Player
    public GameObject player;
    [Header("SpawnPoints")]
    public Transform bulletPoint;
    public Transform magicAreaPoint;
    public Transform beamStartPoint;
    public Transform beamEndPoint;
    [Header("Skill Buttons")]
    public int selectedElement;
    [Header("Skill Buttons")]
    public Button shootButton;
    public bool isShootable = true;
    public Button blastButton;
    public bool isBlastable = true;
    public Button rainButton;
    public bool isRainable = true;
    public Button beamButton;
    public bool isBeamable = true;
    private bool isBeaming = false;
    private float beamTimer = 0;
    [Header("Beam Properties")]
    [SerializeField]
    private GameObject beamStart;
    [SerializeField]
    private GameObject beam;
    [SerializeField]
    private GameObject beamEnd;
    [SerializeField]
    private LineRenderer line;


    [Serializable]
    public struct Beam
    {
        [SerializeField] public GameObject beamStart;
        [SerializeField] public GameObject beam;
        [SerializeField] public GameObject beamEnd;
        [SerializeField] public LineRenderer line;
    }

    [SerializeField] public Beam[] beams = null;

    private void Start()
    {
        gameManager = GameManager.instance;
        //Assign Beam
        selectedElement = gameManager.selectedElement;
        beamStart = beams[selectedElement].beamStart;
        beamEnd = beams[selectedElement].beamEnd;
        beam = beams[selectedElement].beam;
        line = beams[selectedElement].line;
        //Create Beam
        gameManager.isAvailableShoot = true;
        beamStart = Instantiate(beamStart);
        beamStart.SetActive(false);
        beamEnd = Instantiate(beamEnd);
        beamEnd.SetActive(false);
        beam = Instantiate(beam);
        beam.SetActive(false);
        line = beam.GetComponent<LineRenderer>();
        line.positionCount = 2;
        beamTimer = 1;
        //Assign Booleans
        isShootable = true;
        isBlastable = true;
        isRainable = true;

    }

    private void FixedUpdate()
    {
        bulletPoint.rotation = player.transform.rotation;
        if (beamTimer > 0.1f)
        {
            beamTimer -= Time.deltaTime;
        }
        else if (gameManager.closestEnemy != null)
        {
            if (gameManager.closestEnemy.CompareTag("Enemy"))
            {
                if (isBeaming)
                {
                    gameManager.mainPlayer.GetComponent<MainPlayer>().playerCurrentState = Player.PlayerState.Idle;
                    MainEnemy mainEnemy = gameManager.closestEnemy.GetComponent<MainEnemy>();
                    if (mainEnemy.enemyCurrentState != MainEnemy.EnemyState.Attack && mainEnemy.enemyCurrentState != MainEnemy.EnemyState.Die)
                        mainEnemy.enemyCurrentState = MainEnemy.EnemyState.Chase;
                    mainEnemy.TakeDamage(gameManager.playerDamage * 7);
                    beamTimer = 1;
                }
            }
            else if (gameManager.closestEnemy.CompareTag("Dummy"))
            {
                if (isBeaming)
                {
                    gameManager.mainPlayer.GetComponent<MainPlayer>().playerCurrentState = Player.PlayerState.Idle;
                    if (gameManager.closestEnemy.transform.GetChild(0).GetComponent<Dummy>() != null)
                        gameManager.closestEnemy.transform.GetChild(0).GetComponent<Dummy>().TakingDamage();
                    beamTimer = 1;
                }
            }
        }


        CheckShootables();
    }

    public void Shoot(int objectType)
    {
        if (gameManager.isAvailableShoot && gameManager.startGame)
        {
            var obj = objectPool.GetPooledObject(objectType);
            if (gameManager.closestEnemy != null)
            {
                var tempBulletPoint = bulletPoint;
                tempBulletPoint.LookAt(gameManager.closestEnemy);
                obj.transform.rotation = tempBulletPoint.rotation;
            }
            else
                obj.transform.rotation = bulletPoint.rotation;
            obj.transform.position = bulletPoint.position;
            gameManager.audioSource.PlayOneShot(gameManager.audioClips[0], 0.1f);
            StartCoroutine(Disableobj(obj, 1.5f));
            isShootable = false;
            StartCoroutine(DisableButton(shootButton, 0, gameManager.fireRate));
        }
    }

    public void BlastSphere(int objectType)
    {
        if (gameManager.isAvailableShoot && gameManager.startGame)
        {
            var auraCast = objectPool.GetPooledObject(3);
            auraCast.transform.position = player.transform.position;
            gameManager.currentMergedAura.SetActive(false);
            var blastSphere = objectPool.GetPooledObject(objectType);
            if (gameManager.closestEnemy != null)
                blastSphere.transform.position = gameManager.closestEnemy.position;
            else
                blastSphere.transform.position = magicAreaPoint.position;
            gameManager.audioSource.PlayOneShot(gameManager.audioClips[0], 0.1f);
            StartCoroutine(Disableobj(blastSphere, 4f));
            StartCoroutine(Disableobj(auraCast, 1f));
            StartCoroutine(WaitingAura(gameManager.currentMergedAura));
            isBlastable = false;
            StartCoroutine(DisableButton(blastButton, 1, 3f));
        }
    }

    public void MagicRain(int objectType)
    {
        if (gameManager.isAvailableShoot && gameManager.startGame)
        {
            var auraCast = objectPool.GetPooledObject(3);
            auraCast.transform.position = player.transform.position;
            gameManager.currentMergedAura.SetActive(false);
            var magicRain = objectPool.GetPooledObject(objectType);
            if (gameManager.closestEnemy != null)
                magicRain.transform.position = gameManager.closestEnemy.position;
            else
                magicRain.transform.position = magicAreaPoint.position;
            gameManager.audioSource.PlayOneShot(gameManager.audioClips[0], 0.1f);
            StartCoroutine(Disableobj(magicRain, 5f));
            StartCoroutine(Disableobj(auraCast, 1f));
            StartCoroutine(WaitingAura(gameManager.currentMergedAura));
            isRainable = false;
            StartCoroutine(DisableButton(rainButton, 2, 8f));
        }
    }

    public void BeamShootOnPressed()
    {
        if (gameManager.isAvailableShoot && gameManager.startGame && isBeamable)
        {
            gameManager.isMoveable = false;
            beamStart.transform.position = beamStartPoint.position;
            line.SetPosition(0, beamStartPoint.position);
            if (gameManager.closestEnemy != null)
                beamEnd.transform.position = gameManager.closestEnemy.position;
            else
                beamEnd.transform.position = beamEndPoint.position;
            line.SetPosition(1, beamEnd.transform.position);
            beamStart.transform.LookAt(beamEnd.transform.position);
            beamEnd.transform.LookAt(beamStart.transform.position);


            //Deal Damage Per Second with timer
            isBeaming = true;
            isBeamable = false;
            beam.SetActive(true);
            beamStart.SetActive(true);
            beamEnd.SetActive(true);
        }

    }

    public void BeamShootNonPressed()
    {
        gameManager.isMoveable = true;
        StartCoroutine(DisableButton(beamButton, 3, 12f));
        isBeaming = false;
        beam.SetActive(false);
        beamStart.SetActive(false);
        beamEnd.SetActive(false);
    }

    public void CheckShootables()
    {
        if (isShootable)
            shootButton.interactable = true;
        if (isBlastable)
            blastButton.interactable = true;
        if (isRainable)
            rainButton.interactable = true;
        if (isBeamable)
            beamButton.interactable = true;
    }

    // Object Disable
    IEnumerator Disableobj(GameObject obj, float timer)
    {
        yield return new WaitForSeconds(timer);
        obj.SetActive(false);
    }
    IEnumerator DisableButton(Button shootButton, int order, float timer)
    {
        shootButton.interactable = false;
        yield return new WaitForSeconds(timer);
        ReturnTrue(order);
    }
    public void ReturnTrue(int order)
    {
        if (order == 0)
        {
            isShootable = true;
        }
        else if (order == 1)
        {
            isBlastable = true;
        }
        else if (order == 2)
        {
            isRainable = true;
        }
        else if(order ==3)
        {
            isBeamable = true;
        }
    }
    IEnumerator WaitingAura(GameObject gameObject)
    {

        yield return new WaitForSeconds(0.75f);
        gameObject.SetActive(true);
    }

}
