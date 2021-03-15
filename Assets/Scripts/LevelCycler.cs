using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCycler : MonoBehaviour
{
    public string nextScene;

    void Start()
    {
        Gameplay.instance.OnWin += NextSceneLoad;
    }

    public void NextSceneLoad()
    {
        SceneManager.LoadScene(nextScene);
    }
}
