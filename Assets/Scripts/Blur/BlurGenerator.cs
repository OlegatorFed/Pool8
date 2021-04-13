using UnityEngine;

[ExecuteInEditMode]
public class BlurGenerator : MonoBehaviour
{
	public LayerMask excludeLayers = LayerMask.GetMask();
	public RenderTexture renderTexture = null;

	private int resWidth = 0;
	private int resHeight = 0;

	public void Awake()
	{
		resWidth = (int) (Screen.width * 1f);
		resHeight = (int) (Screen.height * 1f);
	}

	public RenderTexture GetBlurScreenshot()
	{
		TakeScreenshot(Camera.main, UseRenderTexture());

		return renderTexture;
	}
	
	public RenderTexture UseRenderTexture()
	{
		return renderTexture != null ? renderTexture : (renderTexture = new RenderTexture(resWidth, resHeight, 24));
	}

	public void TakeScreenshot(Camera camera, RenderTexture commonRender)
	{
		RenderTexture prevRenderTexture = camera.targetTexture;
		camera.targetTexture = commonRender;
		var currentMask = camera.cullingMask;
		camera.cullingMask &= ~excludeLayers;
		camera.Render();
		camera.cullingMask = currentMask;
		camera.targetTexture = prevRenderTexture;
	}
}