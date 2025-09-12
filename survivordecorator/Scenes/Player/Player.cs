using System.ComponentModel;
using System.Threading.Tasks;
using Godot;
public interface IPlayer
{
	int GetSpeed();
	void SetSpeed(int newSpeed);
	void Update();
}

public partial class Player : CharacterBody2D, IPlayer
{
	[Export] public int Speed = 250;
	public int GetSpeed() => Speed;
	public void SetSpeed(int newSpeed) {
		this.Speed = newSpeed;
	}
	public void Update()
	{
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("Right")) direction.X += 1;
		if (Input.IsActionPressed("Left")) direction.X -= 1;
		if (Input.IsActionPressed("Up")) direction.Y -= 1;
		if (Input.IsActionPressed("Down")) direction.Y += 1;

		Velocity = direction.Normalized() * Speed;
		MoveAndSlide();
	}
}


public abstract class PlayerDecorator : IPlayer
{
	protected IPlayer player;
	 public PlayerDecorator(IPlayer player)
	{
		this.player = player;
	}

	public virtual int GetSpeed() => player.GetSpeed();
	public virtual void SetSpeed(int newSpeed) => player.SetSpeed(newSpeed);
	public virtual void Update() => player.Update();
}

public class VelocityDecorator : PlayerDecorator
{
	public int extra = 50;
	public VelocityDecorator(IPlayer player) : base(player)
	{
		player.SetSpeed(player.GetSpeed() + extra);
	}
}

public class ShootDecorator : PlayerDecorator
{
	[Signal] public delegate void PlayerShootEventHandler();

	public event PlayerShootEventHandler PlayerShoot;

	private int cooldownInMs = 1000;
	private bool canShoot = true;
	public ShootDecorator(IPlayer player) : base(player) { }
	public override async void Update()
	{
		base.Update();

		if (canShoot)
		{
			PlayerShoot?.Invoke();
			canShoot = false;

			await Task.Delay(cooldownInMs);
			canShoot = true;
		}

	}
}
