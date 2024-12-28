using Godot;
using System.Collections.Generic;

using rpg;
using System.Reflection.Metadata;
using System.Diagnostics;
public class Backpack
{
	public Backpack(player player)
	{
		p = player;
		items = new Dictionary<int, ItemSlot>();
		for(int i = 0; i < backpackSize; i++)
		{
			items.Add(i, new ItemSlot(i));
		}
	}
	public class ItemSlot
	{
		public ItemSlot(int id)
		{
			slotId = id;
			i = null;
		}

		public int AddItem(item item)
		{
			if(i is null)
			{
				i = item;
				amount++;
				return 0;				// success
			}
			else if(i.Id == item.Id && i.Stackable)
			{
				if(amount < i.MaxStackSize)
				{
					amount++;
					return 0;
				}
			}
			return 1;
		}

		public void RemoveItem()
		{
			i = null;
		}

		public int Getint()
		{
			return i is null ? -1 : i.Id;
		}
		public readonly int slotId;
		public item i;
		public int amount = 0;
	}
	public int AddItem(item item)
	{
		foreach(KeyValuePair<int,ItemSlot> pair in items)
		{
			ItemSlot slot = pair.Value;
			if(slot.AddItem(item) == 0)
			{
				Debug.Print($"Added item ID: {slot.i.Id} in slot {slot.slotId}. Amount: {slot.amount}");
				return 0;
			}
		}
		return 1;
	}

	//public int RemoveItem()

	public Dictionary<int, ItemSlot> items;
	private int backpackSize = 16;
	public player p;
	public bool visible = false;
}

public class Statistics
{
	public int str = 1;
	public int dex = 1;
	public int wis = 1;
	public int stam = 1;
}
public partial class player : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		eq = new Backpack(this);
		stats = new Statistics();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("equipment_key"))
		{
			if(eq.visible)	
			{
				Debug.Print("Equipment invisible");
				eq.visible = false;
			}
			else
			{
				Debug.Print("Equipment visible");
				eq.visible = true;
			}
			
		}

		if(Input.IsActionJustPressed("add_testitem_nonstackable"))
		{
			eq.AddItem(ItemDatabase.Instance.GetItem(0));
		}
		if(Input.IsActionJustPressed("add_testitem_stackable"))
		{
			eq.AddItem(ItemDatabase.Instance.GetItem(1));
		}
	}

	public Backpack eq;
	public Statistics stats;
}
