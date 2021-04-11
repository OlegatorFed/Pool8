using System.Collections;

using UnityEngine;

public class LevelCyclerAnother : MonoBehaviour
{
    public string nextScene;
    public LevelBuilder levelBuilder;
    public CueScript cue;

    void Start()
    {
        Gameplay.instance.OnWinAnother += NextSceneLoad;
    }

    public void NextSceneLoad()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        Time.timeScale = 0f;
        cue.enabled = false;
        
        GlassTransition.instance.Play();
        
        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1f;
        cue.enabled = true;
        
        levelBuilder.NextLevelLoad(nextScene);
        Gameplay.instance.NewLevel();
    }
}
