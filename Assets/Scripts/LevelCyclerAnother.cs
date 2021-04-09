using System.Collections;

using UnityEngine;

public class LevelCyclerAnother : MonoBehaviour
{
    public string nextScene;
    public LevelBuilder levelBuilder;
    public Animator transitionAnimator;
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
        
        transitionAnimator.SetTrigger("Transite");
        
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;
        cue.enabled = true;
        
        levelBuilder.NextLevelLoad(nextScene);
        Gameplay.instance.NewLevel();
        
        transitionAnimator.ResetTrigger("Transite");
    }
}
