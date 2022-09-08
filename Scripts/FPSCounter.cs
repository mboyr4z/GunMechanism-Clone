using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
	[SerializeField] private Text fpsText;

	[SerializeField] private float fpsRate;

	private float FPS = 0;

	private float lastFpsShowTime = -1f;

	private int counter = 0;
	void Update()
	{
		FPS += (int)(1f / Time.unscaledDeltaTime);
		counter++;

		if(Time.time - lastFpsShowTime > fpsRate)
        {
			lastFpsShowTime = Time.time;

			float averageFPS = (FPS / counter);

			fpsText.text = "FPS : "+averageFPS.ToString("0.##");

			FPS = 0;
			counter = 0;
        }
	}

    


}
