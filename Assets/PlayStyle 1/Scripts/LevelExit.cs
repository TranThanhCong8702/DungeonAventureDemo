using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float WaitTime = 1f;
    [SerializeField] int scoreToPass = 3;
    [SerializeField] SpriteRenderer exit;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] ParticleSystem sprinkles;
    [SerializeField] float x = 1f;
    [SerializeField] float y = 1f;
    [SerializeField] float z = 1f;
    int hasBeenDiscover = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 1);
            StartCoroutine(LoadNextLevel());
        }

    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(WaitTime);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScenePersist>().ResetscenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void Start()
    {
        hasBeenDiscover = 0 ;
        txt.text ="Collect  " + scoreToPass + "  Coins\n  to  make  the  door  visible";
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString()) == 1)
        {
            sprinkles.gameObject.SetActive(true);
        }
    }
    public void display(int currscore)
    {
        //int currscore = FindObjectOfType<GameSession>().LoadExit();
        if (currscore >= scoreToPass /*&& SceneManager.GetActiveScene().buildIndex == 5*/)
        {
            exit./*transform.position = new Vector3(x,y,z)*/enabled = true;
            sprinkles.gameObject.SetActive(true);

        }
        //else if (SceneManager.GetActiveScene().buildIndex == 6)
        //{
        //    gameObject.transform.position = new Vector3(x, y, z);
        //}
    }
}
