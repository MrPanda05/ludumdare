using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //Player movement script
    float xInput, imunityFrames;

    bool jumpInput, isHit, isDead, pauseInput, isRunning;

    internal bool isPaused = false, mouseIn;

    public SpriteRenderer players, balls;

    public Points points;

    Rigidbody2D rg;
    public AudioSource jump;

    [Header("Variables Editor")]
    [SerializeField] internal int jumpMax, jumpCount, life, maxFlick;

    [SerializeField] internal float forceJump, walkForce;

    public TextMeshProUGUI LePoints, testPause, testCanShoot;

    public List<GameObject> UI;

    public GunScript gunscrp;

    float damageL;
    [SerializeField] Vector2 spawnLocation;

    [Header("Life edit")]
    public TextMeshProUGUI Health;
    public GameObject PauseUI;
    public void ChangeTXTdebug()
    {
        testPause.text = isPaused.ToString();
        testCanShoot.text = gunscrp.canFire.ToString();
    }
    IEnumerator Die()
    {
        if(life > 0)
        {
            yield return null;
        }
        else
        {
            gameObject.SetActive(false);
            isDead = true;
            walkForce = 0;
            forceJump = 0;
            for(int i= 0; i < UI.Count; i++)
            {
                if (UI[i] == null)
                {
                    break;
                }
                if(i <= 2)
                {
                    UI[i].SetActive(false);
                }
                else
                {
                    UI[i].SetActive(true);
                }
            }
            yield return new WaitForSecondsRealtime(0.5f);//Play anim
            //play sound
            Destroy(gameObject);
        }
    }

    IEnumerator DoDamage()
    {
        if (isHit || isDead)
        {
            yield return null;
        }
        Debug.Log("Tomou hit");
        isHit = true;
        life--;
        Health.text = life.ToString();
        //Play Sound
        for (int i = 0; i < maxFlick; i++)
        {
            if (!isHit)
            {
                yield return null;
            }
            players.enabled = !players.enabled;
            balls.enabled = !balls.enabled;
            yield return new WaitForSecondsRealtime(0.06f);
        }
        isHit = false;
    }

    void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        pauseInput = Input.GetKeyDown(KeyCode.Escape);
        mouseIn = Input.GetMouseButton(0);
    }
    //UI[4].SetActive(true);
    //UI[5].SetActive(true);
    //PauseUI.SetActive(true);

    void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            UI[4].SetActive(true);
            UI[5].SetActive(true);
            PauseUI.SetActive(true);
        }
        else
        {
            UI[4].SetActive(false);
            UI[5].SetActive(false);
            PauseUI.SetActive(false);
        }
    }
    void Move()
    {
        if (isPaused)
        {
            return;
        }
        rg.velocity = new Vector2(xInput * walkForce, rg.velocity.y);
    }

    void Jump()
    {
        if (isDead || isPaused)
        {
            return;
        }
            jumpCount++;
            if (jumpCount < jumpMax)
            {
                //jump
                Debug.Log("Pulo");
                rg.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
                jump.Play(0);
            }
            else
            {
                jumpCount--;
                return;
                //dont jump
            }
    }

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        Health.text = life.ToString();
    }


    private void Update()
    {
        ChangeTXTdebug();
        GetInput();
        if(life <= 0)
        {
            points.Test(LePoints.text);
            points.SetHighScore();
            StartCoroutine(Die());
        }
        if (pauseInput)
        {
            PauseGame();
        }
        
    }

    private void LateUpdate()
    {
        if (jumpInput)
        {
            Jump();
        }
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isHit)
            {
                StartCoroutine(DoDamage());

            }
            else
            {
                return;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Anticheat"))
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = spawnLocation;
        }
    }

}
