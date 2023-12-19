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
		
		//Init prices
		SetPrices();
	
		//Connect Buy Function
		GetNode<TextureButton>("/root/root/StoreScroll/StoreContainer/AltarBuy").Pressed += () => BuyProducer("altar");
		GetNode<TextureButton>("/root/root/StoreScroll/StoreContainer/TempleBuy").Pressed += () => BuyProducer("temple");
		GetNode<TextureButton>("/root/root/StoreScroll/StoreContainer/LibraryBuy").Pressed += () => BuyProducer("library");
		GetNode<TextureButton>("/root/root/StoreScroll/StoreContainer/ComplexBuy").Pressed += () => BuyProducer("complex");
		GetNode<TextureButton>("/root/root/StoreScroll/StoreContainer/CathedralBuy").Pressed += () => BuyProducer("cathedral");
		GetNode<TextureButton>("/root/root/StoreScroll/StoreContainer/MonumentBuy").Pressed += () => BuyProducer("monument");
		GetNode<TextureButton>("/root/root/StoreScroll/StoreContainer/ZigguratBuy").Pressed += () => BuyProducer("ziggurat");
		GetNode<TextureButton>("/root/root/StoreScroll/StoreContainer/GateBuy").Pressed += () => BuyProducer("gate");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		devotionCounter.Text = "You have " + Stats.NumAbbreviate(Mathf.Floor(Stats.devotion)) + " Devotion!";
		Stats.devotion += Stats.devotionPerSecond * (float)delta;
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
					Stats.devotionPerSecond += Stats.altarOutput;
					SetPrices();
				}
				break;
			case "temple":
				if (Stats.devotion >= Stats.templePrice)
				{
					Stats.templeNum += 1;
					Stats.devotion -= Stats.templePrice;
					Stats.templePrice *= 1.5f;
					Stats.devotionPerSecond += Stats.templeOutput;
					SetPrices();
				}
				break;
			case "library":
				if (Stats.devotion >= Stats.libraryPrice)
				{
					Stats.libraryNum += 1;
					Stats.devotion -= Stats.libraryPrice;
					Stats.libraryPrice *= 1.5f;
					Stats.devotionPerSecond += Stats.libraryOutput;
					SetPrices();
				}
				break;
			case "complex":
				if (Stats.devotion >= Stats.complexPrice)
				{
					Stats.complexNum += 1;
					Stats.devotion -= Stats.complexPrice;
					Stats.complexPrice *= 1.5f;
					Stats.devotionPerSecond += Stats.complexOutput;
					SetPrices();
				}
				break;
			case "cathedral":
				if (Stats.devotion >= Stats.cathedralPrice)
				{
					Stats.cathedralNum += 1;
					Stats.devotion -= Stats.cathedralPrice;
					Stats.cathedralPrice *= 1.5f;
					Stats.devotionPerSecond += Stats.cathedralOutput;
					SetPrices();
				}
				break;
			case "monument":
				if (Stats.devotion >= Stats.monumentPrice)
				{
					Stats.monumentNum += 1;
					Stats.devotion -= Stats.monumentPrice;
					Stats.monumentPrice *= 1.5f;
					Stats.devotionPerSecond += Stats.monumentOutput;
					SetPrices();
				}
				break;
			case "ziggurat":
				if (Stats.devotion >= Stats.zigguratPrice)
				{
					Stats.zigguratNum += 1;
					Stats.devotion -= Stats.zigguratPrice;
					Stats.zigguratPrice *= 1.5f;
					Stats.devotionPerSecond += Stats.zigguratOutput;
					SetPrices();
				}
				break;
			case "gate":
				if (Stats.devotion >= Stats.gatePrice)
				{
					Stats.gateNum += 1;
					Stats.devotion -= Stats.gatePrice;
					Stats.gatePrice *= 1.5f;
					Stats.devotionPerSecond += Stats.gateOutput;
					SetPrices();
				}
				break;
			default:
				SetPrices();
				break;
		}
	}
	
	void SetPrices()
	{
		GetNode<Label>("/root/root/StoreScroll/StoreContainer/AltarBuy/AltarLabel").Text = "You have " + Stats.NumAbbreviate(Stats.altarNum) + " Altars \n Buy new: " + Stats.NumAbbreviate(Stats.altarPrice) + " Dv";
		GetNode<Label>("/root/root/StoreScroll/StoreContainer/TempleBuy/TempleLabel").Text = "You have " + Stats.NumAbbreviate(Stats.templeNum) + " Temples \n Buy new: " + Stats.NumAbbreviate(Stats.templePrice) + " Dv";
		GetNode<Label>("/root/root/StoreScroll/StoreContainer/LibraryBuy/LibraryLabel").Text = "You have " + Stats.NumAbbreviate(Stats.libraryNum) + " Libraries \n Buy new: " + Stats.NumAbbreviate(Stats.libraryPrice) + " Dv";
		GetNode<Label>("/root/root/StoreScroll/StoreContainer/ComplexBuy/ComplexLabel").Text = "You have " + Stats.NumAbbreviate(Stats.complexNum) + " Complexes \n Buy new: " + Stats.NumAbbreviate(Stats.complexPrice) + " Dv";
		GetNode<Label>("/root/root/StoreScroll/StoreContainer/CathedralBuy/CathedralLabel").Text = "You have " + Stats.NumAbbreviate(Stats.cathedralNum) + " Cathedrals \n Buy new: " + Stats.NumAbbreviate(Stats.cathedralPrice) + " Dv";
		GetNode<Label>("/root/root/StoreScroll/StoreContainer/MonumentBuy/MonumentLabel").Text = "You have " + Stats.NumAbbreviate(Stats.monumentNum) + " Monuments \n Buy new: " +Stats.NumAbbreviate(Stats.monumentPrice) + " Dv";
		GetNode<Label>("/root/root/StoreScroll/StoreContainer/ZigguratBuy/ZigguratLabel").Text = "You have " + Stats.NumAbbreviate(Stats.zigguratNum) + " Ziggurats \n Buy new: " + Stats.NumAbbreviate(Stats.zigguratPrice) + " Dv";
		GetNode<Label>("/root/root/StoreScroll/StoreContainer/GateBuy/GateLabel").Text = "You have " + Stats.NumAbbreviate(Stats.gateNum) + " Gates \n Buy new: " + Stats.NumAbbreviate(Stats.gatePrice) + " Dv";
	}
}
