using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowResRender : MonoBehaviour {

	[Range(0, 10)]
	public int DownRes;
	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture rt = RenderTexture.GetTemporary(source.width >> DownRes, source.height >> DownRes);
		Graphics.Blit(source, rt);
		rt.filterMode = FilterMode.Point;
		rt.antiAliasing = 1;
		rt.anisoLevel = 0;
		Graphics.Blit(rt, destination);
		RenderTexture.ReleaseTemporary(rt);
	}
}
