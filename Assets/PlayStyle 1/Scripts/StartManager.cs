using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] GameObject setting;
    public void levelloaded(string name)
    {
        //print("Level muon den la :"+name);
        SceneManager.LoadScene(name);
        //FindObjectOfType<GameSession>().DestroyGameSession();
        //FindObjectOfType<ScenePersist>().ResetscenePersist();
    }
    public void Setting()
    {
        setting.SetActive(true);
    }
    public void Back()
    {
        setting.SetActive(false);
    }
}
