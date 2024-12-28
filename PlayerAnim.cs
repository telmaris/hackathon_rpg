using Godot;
using System;

public enum State 
{
	Idle = 0,
	Walk,
	Combat
}

public partial class PlayerAnim : AnimatedSprite2D
{
	private AnimatedSprite2D _animatedSprite;
	[Export] public State state { get; set; } = State.Idle;
	public State prevState = State.Idle;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		 _animatedSprite.Play("idle");
		state = State.Idle;
		Play("idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(prevState != state)
		{
			switch (state) 
			{
				case State.Idle:
					Play("idle");
					break;
				case State.Walk:
					Play("walk");
					break;
				case State.Combat:
					Play("combat");
					break;
				default:
					Play("idle");
					break;
			}
			prevState = state;
		}
	}
}
