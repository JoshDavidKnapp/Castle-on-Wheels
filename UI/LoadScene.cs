using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private GameObject mainTheme;
    public GameObject MainThemeObject;
    private void Start()
    {
        mainTheme = GameObject.FindGameObjectWithTag("main_theme_audio");

    }
    public void LoadGameplayUI()
    {
       
        SceneManager.LoadScene("GameplayUI");
        
    }

public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
        



    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNormal1()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }

    public void LoadStore()
    {
        SceneManager.LoadScene("Store");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("Credits Page");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
        Destroy(mainTheme);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
        Destroy(mainTheme);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
        Destroy(mainTheme);
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level 4");
        Destroy(mainTheme);
    }


}
