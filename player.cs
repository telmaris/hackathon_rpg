using Godot;
using System.Collections.Generic;

using System.Reflection.Metadata;
using System.Diagnostics;
using System;
using rpg;

public partial class Player : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		eq = new Backpack(this);
		stats = new Statistics();
		playerMain = GetParent<player_main>();
		animController.PlayIdle(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ProcessInputs();
		State temp = stateMachine.UpdateState(playerMain._velocity, false);
		RefreshState(temp);
	}

	public void ProcessInputs()
	{
		if (Input.IsActionJustPressed("equipment_key"))
		{
			if (eq.visible)
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

		if (Input.IsActionJustPressed("add_testitem_nonstackable"))
		{
			eq.AddItem(ItemDatabase.Instance.GetItem(0));
		}
		if (Input.IsActionJustPressed("add_testitem_stackable"))
		{
			eq.AddItem(ItemDatabase.Instance.GetItem(1));
		}
	}

	public void RefreshState(State newState)
	{
		if (state != newState)
		{
			state = newState;
			animController.OnStateChanged(state, this);
		}
	}
	public State state = State.Idle;
	public StateMachine stateMachine = new StateMachine();
	public AnimationController animController = new AnimationController();
	public Backpack eq;
	public Statistics stats;
	public player_main playerMain;
}


