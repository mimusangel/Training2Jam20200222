using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
public class UIOperatorOP : MonoBehaviour
{
	public Operator operatorOP;
	public TextMeshProUGUI textOP;

	public Transform aTransform;
	[HideInInspector]
	public GameObject aGo;
	public Transform bTransform;
	[HideInInspector]
	public GameObject bGo;

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
		if (bGo != null)
		{
			Destroy(bGo);
			bGo = null;
		}
		if (operatorOP.GetType() == typeof(OpAnd))
		{
			OpAnd and = operatorOP as OpAnd;
			aGo = Make(and.A, aTransform);
			bGo = Make(and.B, bTransform);
		}
		else if (operatorOP.GetType() == typeof(OpOr))
		{
			OpOr or = operatorOP as OpOr;
			aGo = Make(or.A, aTransform);
			bGo = Make(or.B, bTransform);
		}
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
		if (operatorOP.GetType() == typeof(OpAnd))
		{
			OpAnd and = operatorOP as OpAnd;
			if (index == 0 && and.A == null) and.A = MakeOperator(and);
			if (index == 1 && and.B == null) and.B = MakeOperator(and);
		}
		else if (operatorOP.GetType() == typeof(OpOr))
		{
			OpOr or = operatorOP as OpOr;
			if (index == 0 && or.A == null) or.A = MakeOperator(or);
			if (index == 1 && or.B == null) or.B = MakeOperator(or);
		}
		UIRobotProg.Instance.ChangeProgram();
	}

}
