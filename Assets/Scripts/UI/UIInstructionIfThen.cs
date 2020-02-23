using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIInstructionIfThen : MonoBehaviour
{
	public Transform contentTransform;
	public UIOperator uIOperator;

	[HideInInspector]
	public StatesIf states = null;

	GameObject operatorItem = null;
	List<GameObject> contentItems = new List<GameObject>();

	public void GenerateContent()
	{
		foreach (GameObject go in contentItems)
		{
			Destroy(go);
		}
		contentItems.Clear();
		if (states == null) return;

		if (operatorItem != null)
		{
			Destroy(operatorItem);
			operatorItem = null;
		}
		if (states.condition != null)
		{
			if (states.condition.GetType() == typeof(OpAnd))
			{
				operatorItem = Instantiate(Resources.Load<GameObject>("UI/OperatorOP"), uIOperator.transform);
				UIOperatorOP uiOp = operatorItem.GetComponent<UIOperatorOP>();
				uiOp.operatorOP = states.condition;
				uiOp.textOP.text = "And";
				uiOp.GenerateContent();
			}
			else if (states.condition.GetType() == typeof(OpOr))
			{
				operatorItem = Instantiate(Resources.Load<GameObject>("UI/OperatorOP"), uIOperator.transform);
				UIOperatorOP uiOp = operatorItem.GetComponent<UIOperatorOP>();
				uiOp.operatorOP = states.condition;
				uiOp.textOP.text = "Or";
				uiOp.GenerateContent();
			}
			else if (states.condition.GetType() == typeof(OpEqual))
			{
				operatorItem = Instantiate(Resources.Load<GameObject>("UI/OperatorBlock"), uIOperator.transform);
				UIOperatorBlock uiOp = operatorItem.GetComponent<UIOperatorBlock>();
				uiOp.operatorBlock = states.condition;
				uiOp.textOP.text = "=";
				uiOp.GenerateContent();
			}
			else if (states.condition.GetType() == typeof(OpSup))
			{
				operatorItem = Instantiate(Resources.Load<GameObject>("UI/OperatorBlock"), uIOperator.transform);
				UIOperatorBlock uiOp = operatorItem.GetComponent<UIOperatorBlock>();
				uiOp.operatorBlock = states.condition;
				uiOp.textOP.text = ">";
				uiOp.GenerateContent();
			}
			else if (states.condition.GetType() == typeof(OpInf))
			{
				operatorItem = Instantiate(Resources.Load<GameObject>("UI/OperatorBlock"), uIOperator.transform);
				UIOperatorBlock uiOp = operatorItem.GetComponent<UIOperatorBlock>();
				uiOp.operatorBlock = states.condition;
				uiOp.textOP.text = "<";
				uiOp.GenerateContent();
			}
			else if (states.condition.GetType() == typeof(OpNot))
			{
				operatorItem = Instantiate(Resources.Load<GameObject>("UI/OperatorNot"), uIOperator.transform);
				UIOperatorNot uiOp = operatorItem.GetComponent<UIOperatorNot>();
				uiOp.operatorOP = states.condition as OpNot;
				uiOp.GenerateContent();
			}

		}

		foreach (States state in states.ifProgram)
		{
			GameObject go = UIRobotProg.Instance.MakeGameObject(state, contentTransform);
			if (go != null)
			{
				contentItems.Add(go);
			}
		}
	}

	public void DropOperator(int index)
	{
		if (states.condition != null) return;
		switch (UINewTool.ToolDragAndDrop)
		{
			case UINewTool.Tool.O_And:
				OpAnd and = new OpAnd();
				and.parentST = states;
				states.condition = and;
				break;
			case UINewTool.Tool.O_Or:
				OpOr or = new OpOr();
				or.parentST = states;
				states.condition = or;
				break;
			case UINewTool.Tool.O_Equal:
				OpEqual equal = new OpEqual();
				equal.parentST = states;
				states.condition = equal;
				break;
			case UINewTool.Tool.O_Inf:
				OpInf inf = new OpInf();
				inf.parentST = states;
				states.condition = inf;
				break;
			case UINewTool.Tool.O_Sup:
				OpSup sup = new OpSup();
				sup.parentST = states;
				states.condition = sup;
				break;
			case UINewTool.Tool.O_Not:
				OpNot not = new OpNot();
				not.parentST = states;
				states.condition = not;
				break;
		}
		UIRobotProg.Instance.ChangeProgram();
	}

}
