using UnityEngine;

public class TakeBlur : MonoBehaviour
{
	public BlurGenerator blurGenerator = null;
	public SkinnedMeshRenderer meshRenderer = null;

	public void Apply()
	{
		meshRenderer.material.mainTexture = blurGenerator.GetBlurScreenshot();
	}

	public void SetBluriness(float value)
	{
		meshRenderer.material.SetFloat("radius", value);
	}
}