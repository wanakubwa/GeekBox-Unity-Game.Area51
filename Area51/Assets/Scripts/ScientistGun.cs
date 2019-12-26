using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExplosionDataPattern;

public class ScientistGun : MonoBehaviour
{
    [SerializeField] float shotForce;
    [SerializeField] float bullerSecRadius;
    [SerializeField] float explosionForce = 6f;
    [SerializeField] float explosionBounceForce = 1f;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Animator gunAnimator;
    [SerializeField] Rigidbody2D playerRigidbody2D;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip bulletExplosion;
    [SerializeField] float bulletExplosionVolume = 0.3f;
    [SerializeField] AudioClip gunshotSound;
    [SerializeField] float gunshotSoundVolume = 0.3f;

    public bool CanShoot { get; set; }
    bool isEnteringDoor = false;
    bool wasFirstShoot = false;
    ExplosionData explosionData;
    GameMenager gameMenager;
    LvlMenager lvlMenager;

    GameObject bulletObject;

    struct BulletData
    {
        public GameObject gameObject;
        public Rigidbody2D rigidbody2D;

        public BulletData(GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }
    }
    BulletData bulletData;

    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        SetAnimatorStatement();
        explosionData = ExplosionData.getInstance();
        gameMenager = FindObjectOfType<GameMenager>();
        lvlMenager = FindObjectOfType<LvlMenager>();
        InGameEvents.destroyBulletEvent += BulletDestroyEvent_Handler;
        
        bulletData = new BulletData(Instantiate(bulletPrefab));
        bulletData.gameObject.SetActive(false);

        explosionData.setExplosionForces(explosionForce, explosionBounceForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            SetGunPosition();
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            GunFire();
    }

    private void OnDestroy()
    {
        InGameEvents.destroyBulletEvent -= BulletDestroyEvent_Handler;
    }

    private void BulletDestroyEvent_Handler()
    {
        CanShoot = true;
        SetAnimatorStatement();
    }

    public void EnteringEndLvlDoor()
    {
        isEnteringDoor = true;
    }

    private void SetAnimatorStatement()
    {
        gunAnimator.SetBool("CanShoot", CanShoot);
    }

    private void GunFire()
    {
        if (CanShoot && !isEnteringDoor)
        {
            if (CheckSpaceForBullet())
                CreateBulletInSpawnPoint();
            else
                CreateExposionAtSpawnPoint();
        }
    }

    private bool CheckSpaceForBullet()
    {
        var collidersCounter = 0;
        var coliders = Physics2D.OverlapCircleAll(spawnPoint.position, bullerSecRadius);

        foreach (var element in coliders)
        {
            if (element.gameObject.layer == 9)
                collidersCounter++;
            else if (element.gameObject.layer == 13)
                element.gameObject.GetComponent<Mine>().DestroyMine();
        }
        
        if(collidersCounter != 0)
            return false;

        return true;
    }

    private void CreateBulletInSpawnPoint()
    {
        AudioSource.PlayClipAtPoint(gunshotSound, Camera.main.transform.position, gunshotSoundVolume);
        CanShoot = false;
        bulletData.gameObject.transform.position = spawnPoint.position;
        bulletData.gameObject.transform.rotation = spawnPoint.rotation;

        bulletData.gameObject.SetActive(true);
        bulletData.rigidbody2D.AddForce(spawnPoint.right * shotForce, ForceMode2D.Impulse);
        
        SetAnimatorStatement();
    }

    private void CreateExposionAtSpawnPoint()
    {
        CanShoot = false;
        SetAnimatorStatement();

        AudioSource.PlayClipAtPoint(bulletExplosion, Camera.main.transform.position, bulletExplosionVolume);
        // make delay for simulate normal shoot bounce time
        StartCoroutine(MakeExplosionDelay());

        // add force to player object
        gameMenager.CreateExplosionBounceAnimAtPoint(spawnPoint.position);
        gameMenager.CreateExplosionHitPlayerAnimAtPoint(spawnPoint.position);
        Rigidbody2DExtension.AddExplosionForce(playerRigidbody2D, spawnPoint.position, explosionData.ExplosionForce, explosionData.ExplosionBounceForce);
    }

    private void SetGunPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.right = direction;
    }

    private IEnumerator MakeExplosionDelay()
    {
        yield return new WaitForSeconds(0.1f);
        CanShoot = true;
        SetAnimatorStatement();
    }
}