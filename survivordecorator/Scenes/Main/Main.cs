using Godot;
using System;

public partial class Main : Node2D
{
	public PackedScene arrowScene = GD.Load<PackedScene>("res://Scenes/Projectiles/arrow.tscn");

	private IPlayer player;
	private Node2D projectiles;
	public override void _Ready()
	{
		player = GetNode<IPlayer>("Player");
		projectiles = GetNode<Node2D>("Projectiles");
	}

	public override void _Process(double delta)
    {
		player.Update();
    }

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("VelocityDecorator")) // apretar Enter
		{
			GD.Print($"Velocidad anterior: {player.GetSpeed()}");
			player = new VelocityDecorator(player);
			GD.Print($"Velocidad aumentada: {player.GetSpeed()}");
		}

		if (@event.IsActionPressed("ShootDecorator")) // apretar Espacio
		{
			player = new ShootDecorator(player);
			if (player is ShootDecorator shootDecorator) shootDecorator.PlayerShoot += OnPlayerShoot;
		}
	}

	private void OnPlayerShoot()
	{
		var arrow = arrowScene.Instantiate<Area2D>();
		arrow.Position = player.GetPosition();
		projectiles.AddChild(arrow);
	}
}
