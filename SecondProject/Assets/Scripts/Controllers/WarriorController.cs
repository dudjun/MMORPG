using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : PlayerController
{
	[SerializeField] Item item;
	public override void Init()
	{
		base.Init();
		QuickSlot.quickSlots[0].GetComponent<Slot>().AddItem(item);
	}
}
