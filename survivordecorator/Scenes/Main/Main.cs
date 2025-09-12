using Godot;
using System;

public partial class Main : Node2D
{
	private IPlayer player;
	public override void _Ready()
	{
		player = GetNode<IPlayer>("Player");
	}

	public override void _Process(double delta)
	{
		player.Update();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("VelocityDecorator"))
		{
			GD.Print(player.GetSpeed());
			player = new VelocityDecorator(player);
			GD.Print(player.GetSpeed());
		}

		if (@event.IsActionPressed("ShootDecorator"))
		{
			player = new ShootDecorator(player);
			if (player is ShootDecorator shootDecorator) shootDecorator.PlayerShoot += OnPlayerShoot;

		}
	}

	private void OnPlayerShoot()
	{
		GD.Print("Disparo");
	}
}
