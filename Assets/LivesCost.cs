using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesCost : MonoBehaviour
{
    [SerializeField] float cost = 100f;
    [SerializeField] TextMeshProUGUI cost1;
    [SerializeField] TextMeshProUGUI cost_Success;
    [SerializeField] TextMeshProUGUI cost_NotSucc;
    void Start()
    {
        cost1.text = cost.ToString();
    }
    public void MoreLives()
    {
        if (PlayerPrefs.GetFloat("Money") < cost)
        {
            cost_NotSucc.gameObject.SetActive(true);
            StartCoroutine(Disable(cost_NotSucc.gameObject));
        }
        else if (PlayerPrefs.GetFloat("Money") >= cost)
        {
            if (!PlayerPrefs.HasKey("lives"))
            {
                PlayerPrefs.SetInt("lives", 9);
            }
            else
            {
                int tt = PlayerPrefs.GetInt("lives");
                PlayerPrefs.SetInt("lives", tt + 1);
            }
            float t = PlayerPrefs.GetFloat("Money");
            t -= cost;
            PlayerPrefs.SetFloat("Money", t);
            cost_Success.gameObject.SetActive(true);
            StartCoroutine(Disable(cost_Success.gameObject));
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Disable(GameObject x)
    {
        yield return new WaitForSecondsRealtime(1f);
        x.SetActive(false);
    }
}
