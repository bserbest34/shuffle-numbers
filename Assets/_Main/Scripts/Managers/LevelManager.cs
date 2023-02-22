using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    private int level;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadLevel()
    {
        if (PlayerPrefs.GetInt("Level") <= (SceneManager.sceneCountInBuildSettings - 1) && PlayerPrefs.GetInt("Level") > 1)
        {
            level = PlayerPrefs.GetInt("Level");
        }
        else if (PlayerPrefs.GetInt("Level") > (SceneManager.sceneCountInBuildSettings - 1))
        {
            level = PlayerPrefs.GetInt("Level") % (SceneManager.sceneCountInBuildSettings - 1);
            if (level == 0 && PlayerPrefs.GetInt("Level") == 0)
            {
                level = 1;
            }
            else if (level == 0)
            {
                level = (SceneManager.sceneCountInBuildSettings - 1);
            }
        }
        else
        {
            level = 1;
            PlayerPrefs.SetInt("Level", 1);
        }

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(level);
    }
}