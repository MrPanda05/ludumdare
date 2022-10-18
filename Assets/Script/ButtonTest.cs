using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ButtonTest : MonoBehaviour
{

    [SerializeField]private Button button;
    [SerializeField]private Restart restart;
    // Start is called before the first frame update
    void Start()
    {
        if(restart == null)
        {
            restart = GameObject.FindGameObjectWithTag("Reset").GetComponent<Restart>();
        }
        button.onClick.AddListener(() => {restart.RestartGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
