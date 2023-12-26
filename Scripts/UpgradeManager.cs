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
				instance.LoadUpgrade();
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
	int x = ((upgradeButtons.Count % 6) * 32) - 28;
	float y = (Mathf.Floor((float)upgradeButtons.Count / 7f)) + 6.45f;
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
		upgradeName = "Bloodsoaking I";
		upgradeText = "Your altars produce twice as much Devotion.";
		upgradeTip = "Thorough sacrifical bloodstaining improves potency and capacity of altars.";
		upgradePrice = 200;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/AltarUG1Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.altarNum >= 10)
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
		upgradeName = "Bloodsoaking II";
		upgradeText = "Your altars produce twice as much Devotion.";
		upgradeTip = "Blood for the altars is extracted from veins rather than from general areas, leading to more sacred rituals.";
		upgradePrice = 1250;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/AltarUG2Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.altarNum >= 20)
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

public class AltarProductionIII : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Bloodsoaking III";
		upgradeText = "Your altars produce twice as much Devotion.";
		upgradeTip = "Lifeblood offerings result in doubled optimization as the blood is extracted from sacrifices themselves.";
		upgradePrice = 4250;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/AltarUG3Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.altarNum >= 30)
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

public class AltarProductionIV : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Bloodsoaking IV";
		upgradeText = "Your altars produce twice as much Devotion.";
		upgradeTip = "Traditional structures of sacrifice are restructured into hyperefficient methods of constant ritual execution.";
		upgradePrice = 10000;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/AltarUG4Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.altarNum >= 40)
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

public class AltarProductionV : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Bloodsoaking V";
		upgradeText = "Your altars produce twice as much Devotion.";
		upgradeTip = "By sacrificing hundreds of souls per hour, a direct connection to the Elder Plane is formed.";
		upgradePrice = 95000;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/AltarUG5Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.altarNum >= 50)
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

public class TempleProductionI : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Consecration I";
		upgradeText = "Your temples produce twice as much Devotion.";
		upgradeTip = "Daily rituals to bless the temple improve favor and efficiency, leading to increased devotion production.";
		upgradePrice = 600;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/TempleUG1Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.templeNum >= 10)
		{
			return true;
		}
		return false;
	}
	
	public override void UpgradeEffect()
	{
		Stats.devotionPerSecond -= Stats.templeOutput * Stats.templeNum;
		Stats.templeOutput *= 2;
		Stats.devotionPerSecond += Stats.templeOutput * Stats.templeNum;
	}
}

public class TempleProductionII : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Consecration II";
		upgradeText = "Your temples produce twice as much Devotion.";
		upgradeTip = "Mass distribution of religious substance creates a veritable sphere of influence.";
		upgradePrice = 2400;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/TempleUG2Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.templeNum >= 20)
		{
			return true;
		}
		return false;
	}
	
	public override void UpgradeEffect()
	{
		Stats.devotionPerSecond -= Stats.templeOutput * Stats.templeNum;
		Stats.templeOutput *= 2;
		Stats.devotionPerSecond += Stats.templeOutput * Stats.templeNum;
	}
}

public class TempleProductionIII : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Consecration III";
		upgradeText = "Your temples produce twice as much Devotion.";
		upgradeTip = "Specific border-based sacrifice creates a intense aura of zeal.";
		upgradePrice = 16000;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/TempleUG3Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.templeNum >= 30)
		{
			return true;
		}
		return false;
	}
	
	public override void UpgradeEffect()
	{
		Stats.devotionPerSecond -= Stats.templeOutput * Stats.templeNum;
		Stats.templeOutput *= 2;
		Stats.devotionPerSecond += Stats.templeOutput * Stats.templeNum;
	}
}

public class TempleProductionIV : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Consecration IV";
		upgradeText = "Your temples produce twice as much Devotion.";
		upgradeTip = "Constant human sacrifice accelerates prayer and strengthens extraplanar connection.";
		upgradePrice = 48000;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/TempleUG4Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.templeNum >= 40)
		{
			return true;
		}
		return false;
	}
	
	public override void UpgradeEffect()
	{
		Stats.devotionPerSecond -= Stats.templeOutput * Stats.templeNum;
		Stats.templeOutput *= 2;
		Stats.devotionPerSecond += Stats.templeOutput * Stats.templeNum;
	}
}

public class TempleProductionV : Upgrade
{
	public override void LoadUpgrade()
	{
		upgradeName = "Consecration V";
		upgradeText = "Your temples produce twice as much Devotion.";
		upgradeTip = "Ultraconcentrated mass blessing enables direct transmission of god-essence and thus direct worship.";
		upgradePrice = 200000;
		upgradeIcon = GD.Load<Texture2D>("res://Art/UpgradeIcons/TempleUG5Box.png");
	}
	
	public override bool isAvailable()
	{
		if (Stats.templeNum >= 50)
		{
			return true;
		}
		return false;
	}
	
	public override void UpgradeEffect()
	{
		Stats.devotionPerSecond -= Stats.templeOutput * Stats.templeNum;
		Stats.templeOutput *= 2;
		Stats.devotionPerSecond += Stats.templeOutput * Stats.templeNum;
	}
}
