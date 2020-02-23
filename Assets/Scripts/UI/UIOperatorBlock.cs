using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIOperatorBlock : MonoBehaviour
{
	public Operator operatorBlock;
	public TextMeshProUGUI textOP;

	public Transform aTransform;
	[HideInInspector]
	public GameObject aGo;
	public Transform bTransform;
	[HideInInspector]
	public GameObject bGo;
	
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
		if (operatorBlock.GetType() == typeof(OpEqual))
		{
			OpEqual ope = operatorBlock as OpEqual;
			if (ope.A != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), aTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.A;
			}
			if (ope.B != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), bTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.B;
			}
		}
		else if (operatorBlock.GetType() == typeof(OpInf))
		{
			OpInf ope = operatorBlock as OpInf;
			if (ope.A != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), aTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.A;
			}
			if (ope.B != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), bTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.B;
			}

		}
		else if (operatorBlock.GetType() == typeof(OpSup))
		{
			OpSup ope = operatorBlock as OpSup;
			if (ope.A != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), aTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.A;
			}
			if (ope.B != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), bTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.B;
			}

		}
	}

	Block MakeBlock(Operator parent)
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
		return newBl;
	}

	public void DropOperator(int index)
	{
		if (operatorBlock.GetType() == typeof(OpEqual))
		{
			OpEqual ope = operatorBlock as OpEqual;
			if (index == 0 && ope.A == null) ope.A = MakeBlock(ope);
			if (index == 1 && ope.B == null) ope.B = MakeBlock(ope);
		}
		else if (operatorBlock.GetType() == typeof(OpInf))
		{
			OpInf ope = operatorBlock as OpInf;
			if (index == 0 && ope.A == null) ope.A = MakeBlock(ope);
			if (index == 1 && ope.B == null) ope.B = MakeBlock(ope);

		}
		else if (operatorBlock.GetType() == typeof(OpSup))
		{
			OpSup ope = operatorBlock as OpSup;
			if (index == 0 && ope.A == null) ope.A = MakeBlock(ope);
			if (index == 1 && ope.B == null) ope.B = MakeBlock(ope);

		}
		UIRobotProg.Instance.ChangeProgram();
	}
}
