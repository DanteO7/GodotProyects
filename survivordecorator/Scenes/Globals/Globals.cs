using Godot;
using System;

public partial class Globals : Node
{

    [Signal] public delegate void ChangeHealthEventHandler();
     private int _playerHealth = 4;
    public int PlayerHealth
    {
        get => _playerHealth;
        set
        {
            _playerHealth = value;
            EmitSignal(SignalName.ChangeHealth);
        }
    }
}
