using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private Camera mainCamera;

    private Vector3 mousePos;

    private Vector3 rotation;

    public AudioSource Shoots;


    float rotZ, timer;

    public GameObject bullet;
    public GameObject self;
    public Transform bulletTrans;
    public bool canFire = true;
    public float timeBet;

    [SerializeField]private Player player;

    internal float damage = 5;

    void MoveWeapon()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        rotation = mousePos - transform.position;

        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void Shoot()
    {
        canFire = false;
        var newBullet = Instantiate(bullet, bulletTrans.position, Quaternion.identity);
        Shoots.Play();
    }

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if(timeBet >= 1.5)
        {
            timeBet = 1;
        }
        if(timeBet <= 0)
        {
            timeBet = 0.01f;
        }
        MoveWeapon();
        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBet)
            {
                canFire = true;
                timer = 0;
            }

        }
        if ((player.mouseIn || Input.GetKey(KeyCode.E)) && canFire && !player.isPaused)
        {
                Shoot();
        }
    }

}
