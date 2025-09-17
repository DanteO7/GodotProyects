using Godot;
using System;

public partial class Ui : CanvasLayer
{
    public Globals globals;
    public override void _Ready()
    {
        globals = GetNode<Globals>("/root/Globals");
        globals.ChangeHealth += UpdateHealth;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        var container = GetNode<BoxContainer>("HealthContainer");
        foreach (var life in container.GetChildren())
        {
            life.QueueFree();
        }

        for (int i = 0; i < globals.PlayerHealth; i++)
        {
            var newLife = new TextureRect();
            newLife.Texture = (Texture2D)GD.Load("res://Sprites/UI/Health/ui_heart_full.png");
            newLife.ExpandMode = TextureRect.ExpandModeEnum.FitWidthProportional;
            container.AddChild(newLife);
        }
    }
}
