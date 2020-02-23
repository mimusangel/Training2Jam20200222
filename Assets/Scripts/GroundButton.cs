using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : MonoBehaviour
{
	public List<GameObject> listObject = new List<GameObject>();

	private void OnTriggerEnter(Collider other)
	{
		foreach (GameObject go in listObject)
		{
			go.SendMessage("Activate");
		}
	}

	private void OnTriggerStay(Collider other)
	{
		foreach (GameObject go in listObject)
		{
			go.SendMessage("Activate");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		foreach(GameObject go in listObject)
		{
			go.SendMessage("Desactivate");
		}
	}
}
