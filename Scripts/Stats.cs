using Godot;
using System;

public static class Stats
{
	public static float devotion = 0;
	public static float devotionPerSecond = 0;
	
	//Altar Stats
	public static int altarNum = 0;
	public static float altarRate = 5;
	public static float altarOutput = 1f;
	public static float altarPrice = 10;
	
	//temple Stats
	public static int templeNum = 0;
	public static float templeRate = 5;
	public static float templeOutput = 3;
	public static float templePrice = 450;
	
	//library Stats
	public static int libraryNum = 0;
	public static float libraryRate = 5;
	public static float libraryOutput = 12;
	public static float libraryPrice = 2300;
	
	//complex Stats
	public static int complexNum = 0;
	public static float complexRate = 5;
	public static float complexOutput = 35;
	public static float complexPrice = 10000;
	
	//cathedral Stats
	public static int cathedralNum = 0;
	public static float cathedralRate = 5;
	public static float cathedralOutput = 100;
	public static float cathedralPrice = 35000;
	
	//monument Stats
	public static int monumentNum = 0;
	public static float monumentRate = 5;
	public static float monumentOutput = 650;
	public static float monumentPrice = 100000;
	
	//ziggurat Stats
	public static int zigguratNum = 0;
	public static float zigguratRate = 5;
	public static float zigguratOutput = 3200;
	public static float zigguratPrice = 3500000;
	
	//gate Stats
	public static int gateNum = 0;
	public static float gateRate = 5;
	public static float gateOutput = 15000;
	public static float gatePrice = 25000000;
	
	public static string NumAbbreviate(float number)
{
	const decimal billion = 1000000000m;
	const decimal million = 1000000m;
	const decimal trillion = 1000000000000m;
	const decimal quadrillion = 1000000000000000m;
	const decimal quintillion = 1000000000000000000m;
	const decimal sextillion = 1000000000000000000000m;
	const decimal septillion = 1000000000000000000000000m;
	const decimal octillion = 1000000000000000000000000000m;
		
		if ((decimal)number >= octillion)
		{
			return $"{(decimal)number / octillion:F1} octillion";
		}
		else if ((decimal)number >= septillion)
		{
			return $"{(decimal)number / septillion:F1} septillion";
		}
		else if ((decimal)number >= sextillion)
		{
			return $"{(decimal)number / sextillion:F1} sextillion";
		}
		else if ((decimal)number >= quintillion)
		{
			return $"{(decimal)number / quintillion:F1} quintillion";
		}
		else if ((decimal)number >= quadrillion)
		{
			return $"{(decimal)number / quadrillion:F1} quadrillion";
		}
		else if ((decimal)number >= trillion)
		{
			return $"{(decimal)number / trillion:F1} trillion";
		}
		else if ((decimal)number >= billion)
		{
			return $"{(decimal)number / billion:F1} billion";
		}
		else if ((decimal)number >= million)
		{
			return $"{(decimal)number / million:F1} million";
		}
		else
		{
			return $"{(decimal)number:F0}";
		}
	}
}
