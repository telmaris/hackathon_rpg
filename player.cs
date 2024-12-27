using Godot;
using System.Collections.Generic;

using rpg;
using itemId = System.Int32;
using System.Reflection.Metadata;
using System.Diagnostics;
public class Backpack
{
	public Backpack(player player)
	{
		p = player;
	}
	public class ItemSlot
	{
		public ItemSlot(int id)
		{
			slotId = id;
			i = null;
		}

		public int AddItem(itemId item)
		{
			if(i is null)
			{
				i = new item(item);
				return 0;				// success
			}
			else if(i.id == item && i.stackable)
			{
				if(amount < i.maxStackSize)
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

		public itemId GetItemId()
		{
			return i is null ? 0 : i.id;
		}
		public readonly int slotId;
		public item i;
		public int amount = 0;
	}
	public int AddItem(itemId item)
	{
		foreach(KeyValuePair<int,ItemSlot> pair in items)
		{
			ItemSlot slot = pair.Value;
			if(slot.AddItem(item) == 0)
			{
				return 0;
			}
		}
		return 1;
	}

	//public int RemoveItem()

	public Dictionary<int, ItemSlot> items;
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
	}

	public Backpack eq;
	public Statistics stats;
}
