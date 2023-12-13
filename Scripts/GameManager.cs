using Godot;
using System;

public partial class GameManager : Node
{
	Label devotionCounter;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Get the devotion counter
		devotionCounter = GetNode<Label>("/root/root/DevotionCounter");
		
		ProcessCultists();
	
		//Connect Buy Function
		GetNode<Button>("/root/root/BuyButtons/Cultist").Pressed += () => BuyProducer("cultist");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		devotionCounter.Text = "You have " + Stats.devotion + " devotion!";
	}
	
	void BuyProducer(String producer)
	{
		switch(producer)
		{
			case "cultist":
				if (Stats.devotion >= Stats.cultistPrice)
				{
					Stats.cultistNum += 1;
					Stats.devotion -= (int)Stats.cultistPrice;
					Stats.cultistPrice *= 1.5f;
					GetNode<Button>("/root/root/BuyButtons/Cultist").Text = "Buy Cultists -- " + Mathf.Floor(Stats.cultistPrice).ToString();
				}
				break;
			default:
				break;
		}
	}
	
	async void ProcessCultists()
	{
		Stats.devotion += (int)(Stats.cultistNum * Stats.cultistOutput);
		GD.Print(Stats.devotion);
		await ToSignal(GetTree().CreateTimer(Stats.cultistRate), "timeout");
		ProcessCultists();
	}
}
