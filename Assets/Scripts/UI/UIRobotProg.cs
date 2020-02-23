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
	string currentProgram = "main";

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

	public GameObject MakeGameObject(States state, Transform parent)
	{
		GameObject go = null;
		if (state.GetType() == typeof(StatesForward))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/Instruction"), parent);
			UIInstruction ui = go.GetComponent<UIInstruction>();
			ui.states = state;
			ui.textInstruction.text = "Avancer";
		}
		else if (state.GetType() == typeof(StatesBackward))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/Instruction"), parent);
			UIInstruction ui = go.GetComponent<UIInstruction>();
			ui.states = state;
			ui.textInstruction.text = "Reculer";
		}
		else if (state.GetType() == typeof(StatesRotate))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/InstructionRotate"), parent);
			UIInstructionRotate ui = go.GetComponent<UIInstructionRotate>();
			ui.states = state as StatesRotate;
		}
		else if (state.GetType() == typeof(StatesIf))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/InstructionIf"), parent);
			UIInstructionIfThen ui = go.GetComponent<UIInstructionIfThen>();
			ui.states = state as StatesIf;
			ui.GenerateContent();
		}
		else if (state.GetType() == typeof(StatesIfElse))
		{

		}
		return go;
	}

	public void BtnChangeProgram()
	{
		currentProgram = programList.options[programList.value].text;
		ChangeProgram();
	}

	public void ChangeProgram()
	{
		List<States> instruction = this.robot.GetProgram(currentProgram);
		foreach (GameObject go in contentItems)
		{
			Destroy(go);
		}
		contentItems.Clear();
		UIFonction uiFct = Instantiate(Resources.Load<GameObject>("UI/InstructionStart"), contentTransform).GetComponent<UIFonction>();
		uiFct.textInstruction.text = currentProgram;
		contentItems.Add(uiFct.gameObject);
		foreach (States state in instruction)
		{
			GameObject go = MakeGameObject(state, contentTransform);
			if (go != null)
			{
				contentItems.Add(go);
			}
		}
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

	public void DropInstruction(States afterState/*, Transform parent*/)
	{
		List<States> instruction = this.robot.GetProgram(currentProgram);
		int index = instruction.IndexOf(afterState) + 1;
		//Debug.Log($"DropInstruction {index}");
		DropInstruction(index/*, parent*/);
	}

	public void DropInstruction(UIInstructionIfThen ui, States afterState)
	{
		int index = 0;
		if (afterState != null)
			index = ui.states.ifProgram.IndexOf(afterState) + 1;
		States state = null;
		switch (UINewTool.ToolDragAndDrop)
		{
			case UINewTool.Tool.I_Forward:
				state = new StatesForward();
				ui.states.AddInstruction(state, index);
				break;
			case UINewTool.Tool.I_Backward:
				state = new StatesBackward();
				ui.states.AddInstruction(state, index);
				break;
			case UINewTool.Tool.I_Rotate:
				state = new StatesRotate();
				ui.states.AddInstruction(state, index);
				break;
			case UINewTool.Tool.I_If:
				state = new StatesIf();
				ui.states.AddInstruction(state, index);
				break;
			case UINewTool.Tool.I_IfElse:
				break;
			case UINewTool.Tool.I_Paint:
				break;
		}
		if (state != null)
		{
			ChangeProgram();
		}
	}

	public void DropInstruction(int index/*, Transform parent*/)
	{
		States state = null;
		//GameObject go = null;
		switch (UINewTool.ToolDragAndDrop)
		{
			case UINewTool.Tool.I_Forward:
				state = new StatesForward();
				//go = MakeGameObject(state, parent);
				robot.AddInstruction(currentProgram, state, index);
				break;
			case UINewTool.Tool.I_Backward:
				state = new StatesBackward();
				//go = MakeGameObject(state, parent);
				robot.AddInstruction(currentProgram, state, index);
				break;
			case UINewTool.Tool.I_Rotate:
				state = new StatesRotate();
				//go = MakeGameObject(state, parent);
				robot.AddInstruction(currentProgram, state, index);
				break;
			case UINewTool.Tool.I_If:
				state = new StatesIf();
				//go = MakeGameObject(state, parent);
				robot.AddInstruction(currentProgram, state, index);
				break;
			case UINewTool.Tool.I_IfElse:
				break;
			case UINewTool.Tool.I_Paint:
				break;
		}
		if (state != null)
		{
			ChangeProgram();
		}
		//if (go != null)
		//{
		//	if (index > -1 && index < contentItems.Count)
		//	{
		//		contentItems.Insert(index, go);
		//	}
		//	else
		//		contentItems.Add(go);
		//	if (index > -1)
		//	{
		//		for (int i = 0; i < contentItems.Count; i++)
		//		{
		//			contentItems[i].transform.SetSiblingIndex(contentItems[0].transform.GetSiblingIndex() + i);
		//		}
		//	}
		//}
	}
}
