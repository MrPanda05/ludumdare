using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos, dire, rots;
    private Camera mainCam;
    private Rigidbody2D rg;
    public float force;
    float rotation;
    [SerializeField]private string enemyTag;


    float timer;

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rg = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        dire = mousePos - transform.position;
        rots = transform.position - mousePos;
        rg.velocity = new Vector2(dire.x, dire.y).normalized * force;
        rotation = Mathf.Atan2(rots.y, rots.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
        Destroy(gameObject, 20f);

    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag) || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
