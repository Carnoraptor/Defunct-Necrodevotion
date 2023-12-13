using Godot;
using System;

public partial class GameManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessCultists();
	
		//Connect Buy Function
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	void BuyProducer(String producer)
	{
		switch(producer):
		{
			case "cultist":
				if (Stats.devotion => Stats.cultistPrice)
				Stats.cultistNum += 1;
				break;
		}
	}
	
	async void ProcessCultists()
	{
		Stats.devotion += (int)(Stats.numCultists * Stats.cultistOutput);
		GD.Print(Stats.devotion);
		await ToSignal(GetTree().CreateTimer(Stats.cultistRate), "timeout");
		ProcessCultists();
	}
}
