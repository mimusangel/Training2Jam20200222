using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
	public Operator parent = null;
	public BlockType type { get; private set; }
	public byte[] data { get; private set; }

	public Block(BlockType type)
	{
		this.type = type;
	}

	public Block(BlockType type, bool value)
	{
		this.type = type;
		data = BitConverter.GetBytes(value);
	}

	public Block(BlockType type, StatesColor.StatesColorType color)
	{
		this.type = type;
		data = BitConverter.GetBytes((int)color);
	}

	public virtual bool GetBool(Robot robot = null)
	{
		return BitConverter.ToBoolean(data, 0);
	}

	public virtual StatesColor.StatesColorType GetColor(Robot robot = null)
	{
		return (StatesColor.StatesColorType)BitConverter.ToInt32(data, 0);
	}

	public virtual void SetColor(StatesColor.StatesColorType color)
	{
		data = BitConverter.GetBytes((int)color);
	}

}
