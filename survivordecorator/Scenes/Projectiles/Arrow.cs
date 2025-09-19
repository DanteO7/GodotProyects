using Godot;
using System;

public partial class Arrow : Area2D
{
    public Vector2 direction = Vector2.Down;
    public float speed = 400;
    public override void _Process(double delta)
    {
        Position += direction * speed * (float)delta;

        if (!GetViewportRect().HasPoint(Position))
        {
            QueueFree();
        }
    }

}
