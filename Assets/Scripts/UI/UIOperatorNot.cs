using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
public class UIOperatorNot : MonoBehaviour
{
	public OpNot operatorOP;
	public TextMeshProUGUI textOP;

	public Transform aTransform;
	[HideInInspector]
	public GameObject aGo;

	GameObject Make(Operator oper, Transform parent)
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

		return go;
	}

	public void GenerateContent()
	{
		if (aGo != null)
		{
			Destroy(aGo);
			aGo = null;
		}
		if (operatorOP.A != null) aGo = Make(operatorOP.A, aTransform);
	}

	Operator MakeOperator(Operator parent)
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
		}
		if (newOp != null)
		{
			newOp.parentOP = parent;
		}
		return newOp;
	}

	public void DropOperator(int index)
	{
		if (operatorOP.A == null) operatorOP.A = MakeOperator(operatorOP);
		UIRobotProg.Instance.ChangeProgram();
	}

}
