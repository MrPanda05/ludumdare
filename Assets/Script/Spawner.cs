using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private Transform spawnerPos;
    public List<Transform> otherPos;
    [SerializeField] private Enemy enemy;

    public bool canSpawn;
    int randEnemy;

    float timer;
    public float timerSpawn;
    public DoThings rounds;

    //[SerializeField] private int enemyLimit, currentEnemy;

    public void Create(int enemyLimit, int currentEnemy = 0, int life = 1, int num = 0, int speed = 5, bool cor = false, float points = 10)
    {
        if (currentEnemy < enemyLimit)
        {
            while(currentEnemy < enemyLimit)
            {
                var newEnemy = Instantiate(prefabs[num], spawnerPos);
                enemy = newEnemy.GetComponent<Enemy>();
                enemy.SetStats(life);
                enemy.SetSpeed(speed + num);
                enemy.canChange = cor;
                enemy.SetPoints(points);
                currentEnemy++;
            }
        }
        else
        {
            return;
        }
    }

    //if (!canFire)
    //    {
    //        timer += Time.deltaTime;
    //        if(timer > timeBet)
    //        {
    //            canFire = true;
    //            timer = 0;
    //        }

    //    }

    void Spawn(int enemyLimit, int currentEnemy = 0, int life = 5, int num = 0, int speed = 5, bool cor = false, float points = 10)
    {
        canSpawn = false;
        if (currentEnemy < enemyLimit)
        {
            while (currentEnemy < enemyLimit)
            {
                var newEnemy = Instantiate(prefabs[num], otherPos[num]);
                enemy = newEnemy.GetComponent<Enemy>();
                enemy.SetStats(life);
                enemy.SetSpeed(speed + num);
                enemy.canChange = cor;
                enemy.SetPoints(points);
                currentEnemy++;
            }
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        randEnemy = Mathf.CeilToInt(Random.Range(0, 3));
        Spawn(10, 0, 5, randEnemy, 5, true, Mathf.Ceil((12 + rounds.rounds) + 1));
    }
    private void Update()
    {

        if (!canSpawn)
        {
            timer += Time.deltaTime;
            if (timer > timerSpawn)
            {
                canSpawn = true;
                timer = 0;
            }
        }
        if (canSpawn)
        {
            if(rounds.rounds < 15)
            {
                randEnemy = Mathf.CeilToInt(Random.Range(0, 3));
            }
            else
            {
                randEnemy = Mathf.CeilToInt(Random.Range(0, 7));
            }
            Spawn(10, 0, Mathf.CeilToInt(5 + rounds.rounds * 2.4f), randEnemy, Mathf.CeilToInt(6 + rounds.rounds), true, Mathf.Ceil((12 + rounds.rounds) +1));
        }
    }
}
