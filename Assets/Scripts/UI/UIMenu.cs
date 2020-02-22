using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
	public static UIMenu Instance { get; private set; }

	public bool isPlayMode { get; private set; } = false;
	public List<GameObject> playList = new List<GameObject>();
	public List<GameObject> resetList = new List<GameObject>();

	public TextMeshProUGUI btnControlText;
	public Button resetButton;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	private void Start()
	{
		foreach (GameObject reset in resetList)
		{
			reset.SendMessage("ResetObject");
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
			Instance = null;
	}

	public void ControlReset()
	{
		if (!isPlayMode)
		{
			foreach (GameObject reset in resetList)
			{
				reset.SendMessage("ResetObject");
			}
			foreach (GameObject play in playList)
			{
				play.SendMessage("ResetObject");
			}
		}
	}

	public void ControlPlay()
	{
		isPlayMode = !isPlayMode;
		if (!isPlayMode)
		{
			btnControlText.text = "Play";
			foreach (GameObject play in playList)
			{
				play.SendMessage("Stop");
			}
			resetButton.interactable = true;
		}
		else
		{
			btnControlText.text = "Stop";
			foreach (GameObject reset in resetList)
			{
				reset.SendMessage("ResetObject");
			}
			foreach (GameObject play in playList)
			{
				play.SendMessage("ResetObject");
			}
			foreach (GameObject play in playList)
			{
				play.SendMessage("Play");
			}
			resetButton.interactable = false;
		}
	}
}
