using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    private Transform target;

    public TextMeshProUGUI Points;

    [SerializeField] private float speed;

    [SerializeField] private string tagF;

    [SerializeField] internal float hitCount, life, healthbar;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GunScript gunScript;

    internal bool canChange;

    Points ponts;
    internal float points, highP;

    float step, rotZ;

    Vector3 rotation;

    [SerializeField]private Player player;

    [SerializeField] private DoThings doFunc;


    Color color;

    //float cu = 1;

    public AudioSource death;
   

    void ChangeCor(int r, int g, int b)
    {
        color = new Color(r,g,b);
        spriteRenderer.color = color;
    }

    public void SetPoints(float cum = 10)
    {
        points = cum;
    }
    public float SetSpeed(float speedF = 5)
    {
        speed = speedF;
        return speed;
    }
    public void SetStats(int health)
    {
        life = health;
    }
    void MoveToTarget(float step)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag(tagF).transform;
        gunScript = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunScript>();
        Points = GameObject.FindGameObjectWithTag("Pontos").GetComponent<TextMeshProUGUI>();
        ponts = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Points>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(doFunc == null)
        {
            doFunc = GameObject.FindWithTag("Timer").GetComponent<DoThings>();
        }
        SetStats(5);
        SetSpeed();
        SetPoints();
    }
    private void Start()
    {
        if (canChange)
        {

            if (life <= 5)
            {
                ChangeCor(255, 255, 255);

            }
            else if (life > 5 && life <= 25)
            {
                ChangeCor(255, 55, 220);
            }
            else if (life > 25 && life <= 50)
            {
                ChangeCor(0, 255, 180);
            }
            else if (life > 50 && life <= 75)
            {
                ChangeCor(30, 0, 180);
            }
            else
            {
                ChangeCor(0, 0, 0);
            }
        }
        
    }

    IEnumerator Die()
    {
        death.Play(0);
        yield return new WaitForSecondsRealtime(0.08f);
        Destroy(gameObject);
    }


    private void Update()
    {
        if(!target && !player)
        {
            return;
        }
        step = speed * Time.deltaTime;
        if (!target || player.isPaused)
        {
            speed = 0;
        }
        else
        {
            SetSpeed(doFunc.SpeedFor(5, doFunc.rounds));
            rotation = target.position - transform.position;
            MoveToTarget(step);
        }
        //highP++;
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        //Debug.Log(highP);

        if (life <= 0)
        {
            ponts.ChangeTxt(points);
            StartCoroutine(Die());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            life -= gunScript.damage;
        }
    }
}
