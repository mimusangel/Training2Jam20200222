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

	public Block MakeBlock(Operator parent)
	{
		Block newBl = null;
		switch (UINewTool.ToolDragAndDrop)
		{
			case UINewTool.Tool.B_True:
				newBl = new Block(BlockType.Bool, true);
				break;
			case UINewTool.Tool.B_False:
				newBl = new Block(BlockType.Bool, false);
				break;
			case UINewTool.Tool.B_DetectCollid:
				newBl = new BlockDetectCollider();
				break;
		}
		if (newBl != null)
		{
			newBl.parent = parent;
		}
		return newBl;
	}

	Operator MakeOpe()
	{
		Operator newOp = null;
		switch (UINewTool.ToolDragAndDrop)
		{
			case UINewTool.Tool.O_And:
				newOp = new OpAnd();
				break;
			case UINewTool.Tool.O_Or:
				newOp = new OpOr();
				break;
			case UINewTool.Tool.O_Equal:
				newOp = new OpEqual();
				break;
			case UINewTool.Tool.O_Inf:
				newOp = new OpInf();
				break;
			case UINewTool.Tool.O_Sup:
				newOp = new OpSup();
				break;
			case UINewTool.Tool.O_Not:
				newOp = new OpNot();
				break;
			case UINewTool.Tool.O_Is:
				newOp = new OpIs();
				break;
		}
		return newOp;
	}

	public Operator MakeOperator(Operator parent)
	{
		Operator newOp = MakeOpe();
		if (newOp != null)
		{
			newOp.parentOP = parent;
		}
		return newOp;
	}

	public Operator MakeOperator(States parent)
	{
		Operator newOp = MakeOpe();
		if (newOp != null)
		{
			newOp.parentST = parent;
		}
		return newOp;
	}

	public GameObject MakeGameObjectOperator(Operator oper, Transform parent)
	{
		GameObject go = null;
		if (oper == null) return go;
		if (oper.GetType() == typeof(OpAnd))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/OperatorOP"), parent);
			UIOperatorOP uiOp = go.GetComponent<UIOperatorOP>();
			uiOp.operatorOP = oper;
			uiOp.textOP.text = "And";
			uiOp.GenerateContent();
		}
		else if (oper.GetType() == typeof(OpOr))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/OperatorOP"), parent);
			UIOperatorOP uiOp = go.GetComponent<UIOperatorOP>();
			uiOp.operatorOP = oper;
			uiOp.textOP.text = "Or";
			uiOp.GenerateContent();
		}
		else if (oper.GetType() == typeof(OpEqual))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/OperatorBlock"), parent);
			UIOperatorBlock uiOp = go.GetComponent<UIOperatorBlock>();
			uiOp.operatorBlock = oper;
			uiOp.textOP.text = "=";
			uiOp.GenerateContent();
		}
		else if (oper.GetType() == typeof(OpSup))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/OperatorBlock"), parent);
			UIOperatorBlock uiOp = go.GetComponent<UIOperatorBlock>();
			uiOp.operatorBlock = oper;
			uiOp.textOP.text = ">";
			uiOp.GenerateContent();
		}
		else if (oper.GetType() == typeof(OpInf))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/OperatorBlock"), parent);
			UIOperatorBlock uiOp = go.GetComponent<UIOperatorBlock>();
			uiOp.operatorBlock = oper;
			uiOp.textOP.text = "<";
			uiOp.GenerateContent();
		}
		else if (oper.GetType() == typeof(OpNot))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/OperatorNot"), parent);
			UIOperatorNot uiOp = go.GetComponent<UIOperatorNot>();
			uiOp.operatorOP = oper as OpNot;
			uiOp.GenerateContent();
		}
		else if (oper.GetType() == typeof(OpIs))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/OperatorIs"), parent);
			UIOperatorIS uiOp = go.GetComponent<UIOperatorIS>();
			uiOp.operatorOP = oper as OpIs;
			uiOp.GenerateContent();
		}

		return go;
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
			go = Instantiate(Resources.Load<GameObject>("UI/InstructionIfElse"), parent);
			UIInstructionIfThen ui = go.GetComponent<UIInstructionIfThen>();
			ui.states = state as StatesIfElse;
			ui.GenerateContent();
		}
		else if (state.GetType() == typeof(StatesRepeat))
		{
			go = Instantiate(Resources.Load<GameObject>("UI/InstructionRepeat"), parent);
			UIInstructionRepeat ui = go.GetComponent<UIInstructionRepeat>();
			ui.states = state as StatesRepeat;
			ui.GenerateContent();
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

	public States MakeState()
	{
		States state = null;
		switch (UINewTool.ToolDragAndDrop)
		{
			case UINewTool.Tool.I_Forward:
				state = new StatesForward();
				break;
			case UINewTool.Tool.I_Backward:
				state = new StatesBackward();
				break;
			case UINewTool.Tool.I_Rotate:
				state = new StatesRotate();
				break;
			case UINewTool.Tool.I_If:
				state = new StatesIf();
				break;
			case UINewTool.Tool.I_IfElse:
				state = new StatesIfElse();
				break;
			case UINewTool.Tool.I_Paint:
				break;
			case UINewTool.Tool.I_Repeat:
				state = new StatesRepeat();
				break;
		}
		return state;
	}

	public void DropInstruction(States stateParent, States afterState, bool isElse = false)
	{
		int index = 0;
		if (afterState != null)
		{
			if (stateParent.GetType() == typeof(StatesIf))
				index = (stateParent as StatesIf).ifProgram.IndexOf(afterState) + 1;
			else if (stateParent.GetType() == typeof(StatesIfElse))
			{
				if (isElse)
					index = (stateParent as StatesIfElse).elseProgram.IndexOf(afterState) + 1;
				else
					index = (stateParent as StatesIfElse).ifProgram.IndexOf(afterState) + 1;
			}
			else if (stateParent.GetType() == typeof(StatesRepeat))
				index = (stateParent as StatesRepeat).ifProgram.IndexOf(afterState) + 1;
		}
		States state = MakeState();
		if (state != null)
		{
			if (stateParent.GetType() == typeof(StatesIf))
				(stateParent as StatesIf).AddInstruction(state, index);
			else if (stateParent.GetType() == typeof(StatesIfElse))
			{
				if (isElse)
					(stateParent as StatesIfElse).AddInstructionElse(state, index);
				else
					(stateParent as StatesIfElse).AddInstruction(state, index);
			}
			else if (stateParent.GetType() == typeof(StatesRepeat))
				(stateParent as StatesRepeat).AddInstruction(state, index);

			ChangeProgram();
		}
	}

	public void DropInstruction(int index)
	{
		States state = MakeState();
		if (state != null)
		{
			robot.AddInstruction(currentProgram, state, index);
			ChangeProgram();
		}
	}

	public void DropInstruction(States currentStates, GameObject parent)
	{
		UIInstructionIfContent content = parent.GetComponent<UIInstructionIfContent>();
		if (content)
		{
			DropInstruction(content.ifThen.states, currentStates, content.isElse);
		}
		else
		{
			UIInstructionRepeatContent repeat = parent.GetComponent<UIInstructionRepeatContent>();
			if (repeat)
			{
				DropInstruction(repeat.repeat.states, currentStates, false);
			}
			else
			{
				DropInstruction(currentStates);
			}
		}
	}

	public void DeleteState(States state)
	{
		robot.DeleteInstruction(currentProgram, state);
	}
}
