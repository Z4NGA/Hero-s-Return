using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{   
    static int currentLevel = 0;//at main menu , will be static to all levels
    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;    
    }
    
    public void selectMain()
    {
        Player.newplayer();
        SceneManager.LoadScene(0);
        currentLevel = 0;
    }
    public void startGame()
    {
        Player.newplayer();
        SceneManager.LoadScene(1); //assuming scene 1 will be level 1
        currentLevel = 1;
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void selectTutorial()
    {
        if (SceneManager.GetSceneByBuildIndex(4) != null)//update when tutorial scene index changes
        {
            Player.newplayer();
            SceneManager.LoadScene(4);
            currentLevel = 4;
        }
        else Debug.Log("Error  !! Tutorial scene not found !!");
    }
    public void selectLevel1()
    {
        if (SceneManager.GetSceneByBuildIndex(1) != null)
        {
            Player.newplayer();
            SceneManager.LoadScene(1);
            currentLevel = 1;
        }
        else Debug.Log("Error !! Scene 1 doesn't exist !!");

    }
    public void selectLevel2()
    {
        if (SceneManager.GetSceneByBuildIndex(2) != null)
        {
            SceneManager.LoadScene(2);
            currentLevel = 2;
        }
        else Debug.Log("Error !! Scene 2 doesn't exist !!");

    }
    public void selectLevel3()
    {
        if (SceneManager.GetSceneByBuildIndex(3) != null)
        {
            SceneManager.LoadScene(3);
            currentLevel = 3;
        }
        else Debug.Log("Error !! Scene 3 doesn't exist !!");

    }
    public void restart() //for retrying a level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void nextLevel()
    {
        if (currentLevel + 1 <= 4)
        {
            SceneManager.LoadScene(currentLevel + 1);
            currentLevel++;
        }
        else selectMain();
    }
}
