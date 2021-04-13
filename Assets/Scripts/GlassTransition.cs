using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GlassTransition : MonoBehaviour
{
    public static GlassTransition instance = null;
    
    public float initialAspect = 16 / 10f;
    public Transform transformTarget;
    public TakeBlur blurApplier;

    private Animator animator = null;

    private void Awake()
    {
        instance = this;
        
        animator = GetComponentInChildren<Animator>();
        
        Reset();
    }

    private void Update()
    {
        if (transformTarget == null)
        {
            return;
        }
        
        Vector3 newScale = Vector3.one;

        newScale.x /= initialAspect;
        newScale.x *= (float)Screen.width / Screen.height;

        transformTarget.localScale = newScale;
    }

    public void Play()
    {
        blurApplier.Apply();
        
        transformTarget.gameObject.SetActive(true);
        animator.Play("Transition", 0, 0f);

        StartCoroutine(BlurProcess());
        
        Invoke("Reset", 1.5f);
    }

    IEnumerator BlurProcess()
    {
        for (float t = 0f; t < 1; t += Time.unscaledDeltaTime / 0.2f)
        {
            blurApplier.SetBluriness(t * 20f);

            yield return null;
        }
    }

    public void Reset()
    {
        transformTarget.gameObject.SetActive(false);
    }
}
