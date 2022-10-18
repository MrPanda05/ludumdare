using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private GameObject ps;
    public Vector3 offset;

    [SerializeField] float num;
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        player = ps.transform;
        if (!player)
        {
            return;
        }
        transform.position = new Vector3(player.position.x + offset.x, num, offset.z); // Camera follows the player with specified offset position
    }
}
