using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoretxt;
    [SerializeField] TextMeshProUGUI Endscoretxt;
    [SerializeField] TextMeshProUGUI Highscoretxt;
    HighSCore high;
    EvilWizard evil;
    GameControlType2 gameControlType;
    private void Awake()
    {
        evil = FindObjectOfType<EvilWizard>();
        gameControlType = FindObjectOfType<GameControlType2>();
        high = FindObjectOfType<HighSCore>();
        high.ChangeHighScore(float.Parse(scoretxt.text));
    }
    void Start()
    {
        if (!evil.BossAlive)
        {
            float t = PlayerPrefs.GetFloat("Money") + gameControlType.GetScore();
            PlayerPrefs.SetFloat("Money", t);
        }
        Endscoretxt.text ="Score:\n" + scoretxt.text;
        Highscoretxt.text = "HighScore\n" + PlayerPrefs.GetFloat("HighScore");
    }


    void Update()
    {
        
    }
}
