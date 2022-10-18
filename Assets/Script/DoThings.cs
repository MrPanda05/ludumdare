using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoThings : MonoBehaviour
{
    [SerializeField] private TimerAction timer;

    [SerializeField] private int rng;
    internal float score;
    public TextMeshProUGUI Effects;

    [SerializeField]internal float rounds = 0;

    [Header("Debuger props")]
    public bool isDebbugMode;
    public int le_DebugTime, debugRng;

    [Header("Scripts that contains fuctions that will be used")]
    [SerializeField] private Spawner enemySpawner;
    [SerializeField] private int enemyLimit, currentEnemy;
    [SerializeField] private Player player;
    [SerializeField] private GunScript gunScript;

    [SerializeField] private Camera cam;
    [SerializeField] private Points points;

    Color colorDef;
    IEnumerator ChangeCamColor(byte r, byte g, byte b)
    {
        gunScript.self.SetActive(false);
        cam.backgroundColor = new Color32(r, g, b, 0);
        yield return new WaitForSecondsRealtime(9.8f);
        cam.backgroundColor = new Color32(72, 227, 151, 0);
        gunScript.self.SetActive(true);
        Cursor.visible = true;
        yield return null;
    }

    public AudioSource beep;

    int LifeFor(int basec, float round)
    {
        return Mathf.CeilToInt(basec + rounds);
    }


    public int SpeedFor(int basec, float round)
    {
        return Mathf.CeilToInt(basec + round / 2);
    }

    float PointsFor(int basec = 125, float round = 0)
    {
        return Mathf.CeilToInt((basec + rounds * round) * 3 / 2);
    }
    void ChangeTxt(string Text)
    {
        Effects.text = Text;
    }
    void DoSomething(int rng)
    {
        beep.Play();
        //Do things based on the rng
        //TO ADD: UI FEEDBACK
        switch (rng)
        {
            case 0:
                //Spawn fixed sets of enemies
                enemySpawner.Create(enemyLimit, currentEnemy, LifeFor(1, rounds + gunScript.damage), 0, SpeedFor(8, rounds), true, PointsFor(12, rounds));
                ChangeTxt("10 more enemies spawned!!");
                break;
            case 1:
                //Doubes speed and jump
                ChangeTxt("2 points in speed and jump");
                player.walkForce += 2;
                player.forceJump += 2;
                break;
            case 2:
                //Half speed and jump
                ChangeTxt("You lost 2 points in speed and jump");
                player.walkForce -= 2;
                player.forceJump -= 2;
                break;
            case 3:
                //spawn random 1-20 enemies
                ChangeTxt("A good chunck of enemies might apear");
                enemySpawner.Create(Mathf.CeilToInt(Random.Range(1, rounds)), currentEnemy, LifeFor(4, rounds + gunScript.damage), 1, SpeedFor(8, rounds), true, PointsFor(7, rounds));
                break;
            case 4:
                //Adds an extra jump
                ChangeTxt("+1 jump!");
                player.jumpMax++;
                break;
            case 5:
                //Delays shoots even more
                ChangeTxt("Less firerate!!");
                gunScript.timeBet += 0.05f;
                break;
            case 6:
                //inscrease shot rate
                ChangeTxt("More firerate!!");
                gunScript.timeBet -= 0.05f;
                break;
            case 7:
                ChangeTxt("More enemies");
                enemySpawner.Create(Mathf.CeilToInt(Random.Range(1, rounds)), currentEnemy, LifeFor(5, rounds + gunScript.damage), 2, SpeedFor(10, rounds), true, PointsFor(10, rounds));
                break;
            case 8:
                ChangeTxt("+1 damage");
                gunScript.damage += 1;
                break;
            case 9:
                ChangeTxt("-1 damage");
                gunScript.damage -= 1;
                break;
            case 10:
                ChangeTxt("Double damage!");
                gunScript.damage *= 2;
                break;
            case 11:
                ChangeTxt("Half a damage!");
                gunScript.damage /= 2;
                break;
            case 12:
                //Cant see yourself?
                ChangeTxt("Did you loose yourself?");
                StartCoroutine(ChangeCamColor(255, 130, 255));
                break;
            case 13:
                //Cant see your bullets?
                ChangeTxt("Where are you shooting?");
                Cursor.visible = false;
                StartCoroutine(ChangeCamColor(0, 180, 255));
                break;
            case 14:
                if(rounds > 10)
                {
                    ChangeTxt("Boss Battle?");
                    enemySpawner.Create(enemyLimit, currentEnemy, LifeFor(30, rounds + gunScript.damage), 3, SpeedFor(1, rounds), false, PointsFor(11, rounds));
                }
                else
                {
                    ChangeTxt("I was going to do somehing, but  I changed my mind");
                }
                break;
            case 15:
                if (rounds > 15)
                {
                    ChangeTxt("Boss Battle? but harder!");
                    enemySpawner.Create(enemyLimit, currentEnemy, LifeFor(45, rounds + gunScript.damage), 4, SpeedFor(3, rounds), false, PointsFor(12, rounds));

                }
                else
                {
                    ChangeTxt("Got lucky...");
                }
                break;
            case 16:
                if (rounds > 20)
                {
                    ChangeTxt("Boss Battle? but even harder!");
                    enemySpawner.Create(enemyLimit, currentEnemy, LifeFor(50, rounds + gunScript.damage), 5, SpeedFor(3, rounds), false, PointsFor(13, rounds));
                }
                else
                {
                    ChangeTxt("Maybe next time");
                }
                break;
            case 17:
                ChangeTxt("life up!");
                player.life++;
                player.Health.text = player.life.ToString();
                break;
            case 18:
                if(player.life == 1)
                {
                    ChangeTxt("I was going to kill you, but I changed my mind... life up!");
                    player.life++;
                    player.Health.text = player.life.ToString();
                }
                else
                {
                    ChangeTxt("life down!");
                    player.life--;
                    player.Health.text = player.life.ToString();
                }
                break;
            case 19:
                ChangeTxt("Loose points");
                points.TrollPoints(Mathf.Ceil(Random.Range(1, 201)));
                break;
            case 20:
                ChangeTxt("Triple damage!");
                gunScript.damage *= 3;
                break;
            case 21:
                ChangeTxt("FAST machine gun time");
                gunScript.timeBet /= 2;
                break;
            case 22:
                ChangeTxt("SLOW machine gun time");
                gunScript.timeBet *= 2;
                break;
            case 23:
                ChangeTxt("No effects... for now xD");
                break;
            case 24:
                ChangeTxt("+10 damage");
                gunScript.damage += 10;
                break;
            case 25:
                ChangeTxt("+5 life");
                player.life += 5;
                player.Health.text = player.life.ToString();
                break;
            case 26:
                if(player.life <= 5)
                {
                    ChangeTxt("Look... you are in a pretty bad state, so... +3 life");
                    player.life += 3;
                    player.Health.text = player.life.ToString();
                }
                else
                {
                    ChangeTxt("-5 life xD");
                    player.life -= 5;
                    player.Health.text = player.life.ToString();
                }
                break;
            default:
                Debug.Log("erwe");
                break;
        }
    }

    private void Update()
    {

        if (isDebbugMode)
        {
            if(timer.timer >= le_DebugTime)
            {
                timer.timer = 0;
                if(player == null)
                {
                    return;
                }
                else
                {
                    DoSomething(debugRng);
                }
            }
            
        }
        else if(!isDebbugMode && timer.timer >= 10 && player != null)
        {
            rounds++;
            timer.timer = 0;
            rng = Mathf.CeilToInt(Random.Range(0, 27));
            if (!player.isPaused)
            {
                DoSomething(rng);
            }
            if(rounds%5 == 0)
            {
                enemyLimit++;
                gunScript.damage += 5;
                player.life++;
                player.Health.text = player.life.ToString();
            }
        }
    }
}
