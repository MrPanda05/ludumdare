using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAction : MonoBehaviour
{
    [SerializeField] internal float timer;
    [SerializeField] internal Player player;




    private void Update()
    {
        if (!player.isPaused)
        {
            timer += Time.deltaTime;
        }
    }
}
