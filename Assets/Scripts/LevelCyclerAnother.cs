using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCyclerAnother : MonoBehaviour
{
    public string nextScene;
    public LevelBuilder levelBuilder;

    void Start()
    {
        Gameplay.instance.OnWinAnother += NextSceneLoad;
    }

    public void NextSceneLoad()
    {
        levelBuilder.NextLevelLoad(nextScene);
        Gameplay.instance.coinAmount = 0;
    }
}
