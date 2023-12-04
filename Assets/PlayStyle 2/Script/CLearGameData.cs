using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CLearGameData : MonoBehaviour
{
    [SerializeField] GameObject confirmPanel;
    public void DeleteAllPlayerPrefs()
    {
        confirmPanel.SetActive(true);
    }
    public void Yes()
    {
        confirmPanel.SetActive(false);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void No()
    {
        confirmPanel.SetActive(false);
    }
}
