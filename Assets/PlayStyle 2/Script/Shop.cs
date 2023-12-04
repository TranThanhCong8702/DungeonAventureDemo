using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Gold;
    [SerializeField] int lives = 1;
    float t;

    [SerializeField] List<BulletSO> list;
    [SerializeField] SpriteRenderer spriter;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] TextMeshProUGUI cost_Success;
    [SerializeField] TextMeshProUGUI cost_NotSucc;
    [SerializeField] GameObject BuyButton;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetFloat("Money", 0f);
            t = PlayerPrefs.GetFloat("Money");
        }
        else
        {
            t = PlayerPrefs.GetFloat("Money");
        }
        ChangBull();
        Gold.text = t.ToString();
    }

    private void ChangBull()
    {
        if (!PlayerPrefs.HasKey("BulletCount"))
        {
            count = 0;
        }
        else
        {
            Load();
            Debug.Log(PlayerPrefs.GetInt("BulletCount"));
        }
        ChangeBullet(count);
    }

    public void SetDefault()
    {
        PlayerPrefs.SetInt("lives", lives);
    }
    void Update()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetFloat("Money", 0f);
        }
        else
        {
            t = PlayerPrefs.GetFloat("Money");
        }
        if (list[count].GetEquipedState())
        {
            GetPlayBullet();
            BuyButton.SetActive(false);
        }
        else if (!list[count].GetEquipedState())
        {
            BuyButton.SetActive(true);
        }
        Gold.text = t.ToString();
    }

    int count = 0;
    public void Buy()
    {
        if (PlayerPrefs.GetFloat("Money") < list[count].GetPointToBuy() && !list[count].GetEquipedState())
        {
            cost_NotSucc.gameObject.SetActive(true);
            StartCoroutine(Disable(cost_NotSucc.gameObject));
        }
        else if (PlayerPrefs.GetFloat("Money") > list[count].GetPointToBuy() && !list[count].GetEquipedState())
        {
            float t = PlayerPrefs.GetFloat("Money");
            t -= list[count].GetPointToBuy();
            PlayerPrefs.SetFloat("Money", t);
            list[count].SetIsEquiped();
            cost_Success.gameObject.SetActive(true);
            StartCoroutine(Disable(cost_Success.gameObject));
        }
    }
    IEnumerator Disable(GameObject x)
    {
        yield return new WaitForSecondsRealtime(1f);
        x.SetActive(false);
    }
    //void Start()
    //{
    //    if (!PlayerPrefs.HasKey("Money"))
    //    {
    //        PlayerPrefs.SetFloat("Money", 0);
    //    }

    //    if (!PlayerPrefs.HasKey("BulletCount"))
    //    {
    //        count = 0;
    //    }
    //    else
    //    {
    //        Load();
    //        Debug.Log(PlayerPrefs.GetInt("BulletCount"));
    //    }
    //    ChangeBullet(count);
    //    DisplayTotalPoints();
    //}
    //private void Update()
    //{
    //    if (list[count].GetEquipedState())
    //    {
    //        GetPlayBullet();
    //        BuyButton.SetActive(false);
    //    }
    //    else if (!list[count].GetEquipedState())
    //    {
    //        BuyButton.SetActive(true);
    //    }
    //    DisplayTotalPoints();
    //}
    private void GetPlayBullet()
    {
        PlayerPrefs.SetInt("bulletIndex", count);
    }
    public void Next()
    {
        count++;
        if (count >= list.Count)
        {
            count = 0;
        }
        ChangeBullet(count);
        Save();
    }
    public void Back()
    {
        count--;
        if (count < 0)
        {
            count = list.Count - 1;
        }
        ChangeBullet(count);
        Save();
    }
    private void ChangeBullet(int count)
    {
        spriter.sprite = list[count].GetShopIcon();
        cost.text = list[count].GetPointToBuy().ToString();
    }
    void Load()
    {
        if (PlayerPrefs.GetInt("BulletCount") >= list.Count)
        {
            count = list.Count - 1;
        }
        else
        {
            count = PlayerPrefs.GetInt("BulletCount");
        }
    }
    void Save()
    {
        PlayerPrefs.SetInt("BulletCount", count);
        Debug.Log(PlayerPrefs.GetInt("BulletCount"));
    }
}
