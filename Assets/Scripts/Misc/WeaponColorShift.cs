using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColorShift : MonoBehaviour
{

	public Material mat1;
	public Color[] colors;
	public Color color;
	public float lerpSpeed = 10;
	public float changeSpeed = 1;
	// Use this for initialization
	void Start()
	{
		StartCoroutine("RandomColor");
		StartCoroutine("FadeColor");
	}

	IEnumerator RandomColor()
	{
		while (true)
		{

			color = colors[Random.Range(0, colors.Length - 1)];
			yield return new WaitForSeconds(changeSpeed);
		}
	}

	IEnumerator FadeColor()
	{
		while (true)
		{
			Color c = Color.Lerp(mat1.color, color, Time.deltaTime * lerpSpeed);
			mat1.SetColor("_Color", c);
			mat1.SetColor("_EmissionColor", c);
			yield return null;
		}
	}
}
