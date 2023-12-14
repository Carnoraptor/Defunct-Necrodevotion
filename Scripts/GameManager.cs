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
		
		//Process producers
		ProcessAltars();
	
		//Connect Buy Function
		GetNode<TextureButton>("/root/root/BuyButtons/AltarBuy").Pressed += () => BuyProducer("altar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		devotionCounter.Text = "You have " + Mathf.Floor(Stats.devotion) + " Devotion!";
	}
	
	void BuyProducer(String producer)
	{
		switch(producer)
		{
			case "altar":
				if (Stats.devotion >= Stats.altarPrice)
				{
					Stats.altarNum += 1;
					Stats.devotion -= Stats.altarPrice;
					Stats.altarPrice *= 1.5f;
					GetNode<Label>("/root/root/BuyLabels/AltarLabel").Text = "Buy an Altar -- " + Mathf.Floor(Stats.altarPrice).ToString() + " Dev";
					GetNode<Label>("/root/root/Counters/Labels/AltarCounterText").Text = Stats.altarNum.ToString();
				}
				break;
			default:
				break;
		}
	}
	
	async void ProcessAltars()
	{
		Stats.devotion += (Stats.altarNum * Stats.altarOutput);
		GD.Print(Stats.devotion);
		await ToSignal(GetTree().CreateTimer(Stats.altarRate), "timeout");
		ProcessAltars();
	}
}
