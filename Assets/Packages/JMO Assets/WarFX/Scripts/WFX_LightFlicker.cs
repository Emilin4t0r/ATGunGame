using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
	public float time = 0.05f;
	public bool flashOnce;
	private float timer;
	
	void Start ()
	{
		timer = time;
		StartCoroutine("Flicker");
	}
	
	IEnumerator Flicker()
	{
		if (flashOnce)
		{
			yield return new WaitForSeconds(time);
			GetComponent<Light>().enabled = false;
		}
		else
		{
			while (true)
			{
				GetComponent<Light>().enabled = !GetComponent<Light>().enabled;

				do
				{
					timer -= Time.deltaTime;
					yield return null;
				}
				while (timer > 0);
				timer = time;
			}
		}
	}
}
