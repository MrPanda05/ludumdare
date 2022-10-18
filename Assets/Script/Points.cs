using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;


public class Points : MonoBehaviour
{
    internal float globalP = 0;
    string localP;

    public TextMeshProUGUI LePoints;
    public TextMeshProUGUI HighScore;

    private void Start()
    {
        HighScore.text = PlayerPrefs.GetFloat("HighScore", 0).ToString();
    }

    public void SetHighScore()
    {
        //HighScore.text = globalP.ToString();
        if(globalP > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", globalP);
            HighScore.text = globalP.ToString();
        }

    }

    public void Test(string txt)
    {
        LePoints.text = txt;
    }
    public void ChangeTxt(float points)
    {
        globalP += points;

        LePoints.text = globalP.ToString();
        SetHighScore();

    }

    public void TrollPoints(float loss)
    {
        var num = float.Parse(LePoints.text);
        num -= loss;
        LePoints.text = num.ToString();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        HighScore.text = "0";
    }

}
