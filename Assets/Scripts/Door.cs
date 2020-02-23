using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public GameObject left;
	public GameObject right;

	public bool isActivate { get; private set; } = false;

	public void Activate()
	{
		isActivate = true;
		left.transform.localPosition = new Vector3(-0.7f, 0.5f, 0.0f);
		right.transform.localPosition = new Vector3(0.7f, 0.5f, 0.0f);
	}

	public void Desactivate()
	{
		isActivate = false;
		left.transform.localPosition = new Vector3(-0.25f, 0.5f, 0.0f);
		right.transform.localPosition = new Vector3(0.25f, 0.5f, 0.0f);
	}
}
