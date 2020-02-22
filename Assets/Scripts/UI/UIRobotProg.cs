using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIRobotProg : MonoBehaviour
{
	public static UIRobotProg Instance { get; private set; }

	public TextMeshProUGUI title;
	public Dropdown programList;

	public Transform contentTransform;
	List<GameObject> contentItems = new List<GameObject>();


	[HideInInspector]
	public Robot robot = null;

	public static bool isOpen
	{
		get
		{
			if (Instance)
				return Instance.gameObject.activeSelf;
			return false;
		}
	}

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	private void Start()
	{
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		if (Instance == this)
			Instance = null;
	}

	public void Show(Robot robot)
	{
		if (UIMenu.Instance.isPlayMode) return;
		this.robot = robot;

		programList.ClearOptions();
		programList.AddOptions(this.robot.GetProgramNameList().Select((x) => x.Capitalize()).ToList<string>());
		BtnChangeProgram();

		gameObject.SetActive(true);
		UIMenu.Instance.gameObject.SetActive(false);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		UIMenu.Instance.gameObject.SetActive(true);
	}

	public void BtnChangeProgram()
	{
		List<States> instruction = this.robot.GetProgram(programList.options[programList.value].text);
		

	}

	public void BtnCreateProgram()
	{

	}

	public void BtnDeleteProgram()
	{
		this.robot.DeleteProgram(programList.options[programList.value].text);
		programList.ClearOptions();
		programList.AddOptions(this.robot.GetProgramNameList().Select((x) => x.Capitalize()).ToList<string>());
		BtnChangeProgram();

	}
}
