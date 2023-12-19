using Godot;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

public partial class UpgradeManager : Node
{
	public List<Upgrade> upgradeList = new List<Upgrade>();
	public List<Upgrade> availableUpgrades = new List<Upgrade>();
	public List<Upgrade> ownedUpgrades = new List<Upgrade>();
	
	public List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();
	
	PackedScene packedButton = ResourceLoader.Load<PackedScene>("res://Scenes/UpgradeButtonInstance.tscn");
	

	public override void _Ready()
	{
		GetAllUpgrades();
		LoopAvailability();
	}
	
	//Constantly check what upgrades are available
	async void LoopAvailability()
	{
		CheckAvailability();
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		LoopAvailability();
	}
	
	//Check specifically which upgrades are available
	public void CheckAvailability()
	{
		List<Upgrade> storeUpgrades = new List<Upgrade>();
		storeUpgrades = new List<Upgrade>(availableUpgrades);
		availableUpgrades.Clear();
		foreach(Upgrade ug in upgradeList)
		{
			if (ug.isAvailable())
			{
				availableUpgrades.Add(ug);
			}
		}
		if (availableUpgrades.SequenceEqual(storeUpgrades) == false)
		{
			ResetUpgradeBar();
		}
	}
	
	//somehow stop adding the same upgrade to available multiple times
	void ResetUpgradeBar()
	{
		foreach(TextureButton tb in upgradeButtons)
		{
			tb.QueueFree();
		}
		upgradeButtons.Clear();
		foreach(Upgrade ug in availableUpgrades)
		{
			CreateButton(ug);
		}
	}
	
	public void GetAllUpgrades()
	{
		// Use reflection to find all types derived from Upgrade
		var derivedTypes = Assembly.GetExecutingAssembly().GetTypes()
			.Where(type => type.IsSubclassOf(typeof(Upgrade)) && !type.IsAbstract);

		// Create instances of each derived type and add them to the list
		foreach (var derivedType in derivedTypes)
		{
			var instance = Activator.CreateInstance(derivedType) as Upgrade;
			if (instance != null)
			{
				upgradeList.Add(instance);
			}
		}
	}
	
	public void GainUpgrade(Upgrade ug, UpgradeButton ub)
	{
		foreach(Upgrade ugr in ownedUpgrades)
		{
			if (ugr == ug)
			{
				ub.QueueFree();
				return;
			}
		}
		
		//Gain the upgrade
		ownedUpgrades.Add(ug);
		//Gain the upgrade effect
		ug.UpgradeEffect();
		//Lose the devotion
		Stats.devotion -= ug.upgradePrice;
		//Remove the upgrade from availableUpgrades
		for (int i = 0; i < availableUpgrades.Count - 1;i++)
		{
			if (availableUpgrades[i] == ug)
			{
				availableUpgrades.RemoveAt(i);
			}
		}
		
		ub.QueueFree();
		GD.Print(ub);
	}
	
	public void CreateButton(Upgrade ug)
	{
	// Instantiate the scene and cast it to UpgradeButton
	UpgradeButton button = (UpgradeButton)packedButton.Instantiate();
	
	GetNode("/root/root/UpgradeButtons").AddChild(button);
	button.LoadUpgradeIdentity(ug);
	upgradeButtons.Add(button);
	int x = ((upgradeButtons.Count % 5) * 32) + 5;
	float y = (Mathf.Floor((float)upgradeButtons.Count / 5f)) + 6.3f;
	button.Position = new Vector2(x * 2, y * 25);
	}
}


public class Upgrade
{
	public string upgradeName;
	public string upgradeText;
	public string upgradeTip;
	public float upgradePrice;
	public Texture2D upgradeIcon;
	
	public virtual void LoadUpgrade(){}
	
	public virtual bool isAvailable()
	{
		return false;
	}
	
	public virtual void UpgradeEffect(){}
}

public class AltarProductionI : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Inscription I";
		upgradeText = "Your altars produce twice as much Devotion.";
		upgradeTip = "Dark text etched into your altars improves sacrificial capabilities and harvests emotion from sacrifices";
		upgradePrice = 2;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/AltarUG1Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.altarNum >= 2)
		{
			return true;
		}
		return false;
	}
	
	public override void UpgradeEffect()
	{
		Stats.devotionPerSecond -= Stats.altarOutput * Stats.altarNum;
		Stats.altarOutput *= 2;
		Stats.devotionPerSecond += Stats.altarOutput * Stats.altarNum;
	}
}

public class AltarProductionII : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Inscription II";
		upgradeText = "Your altars produce twice as much Devotion.";
		upgradeTip = "Dark text etched into your altars improves sacrificial capabilities and harvests emotion from sacrifices";
		upgradePrice = 4;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/AltarUG1Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.altarNum >= 3)
		{
			return true;
		}
		return false;
	}
	
	public override void UpgradeEffect()
	{
		Stats.devotionPerSecond -= Stats.altarOutput * Stats.altarNum;
		Stats.altarOutput *= 2;
		Stats.devotionPerSecond += Stats.altarOutput * Stats.altarNum;
	}
}
