using Godot;
using System;

public partial class UpgradeButton : TextureButton
{
	public Upgrade upgradeIdentity;

	public override void _Ready()
	{
		this.Pressed += OnPressed;
		this.MouseEntered += OnMouseEntered;
	}

	public void LoadUpgradeIdentity(Upgrade ug)
	{
		upgradeIdentity = ug;
		//This should work but it doesn't, angry
		TextureNormal = ug.upgradeIcon;
	}
	
	public void OnPressed()
	{
		if (upgradeIdentity.isAvailable() && Stats.devotion > upgradeIdentity.upgradePrice)
		{
			GetNode<UpgradeManager>("/root/root/UpgradeManager").GainUpgrade(upgradeIdentity, this);
			QueueFree();
		}
	}
	
	public void OnMouseEntered()
	{
		//GD.Print("Mouse has begun hovering over button!");
	}
}
