using Godot;
using System;

public partial class CultNameGenerator : Label
{
	Godot.Collections.Array group = new Godot.Collections.Array{"Cult", "Followers", "Church", "Devoted", "United", "Scions", "Servants", "Acolytes", "Emissaries", "Damned", "Gathering", "Chosen", "Order", "Divinity", "Messengers"};
	Godot.Collections.Array adjective = new Godot.Collections.Array{"Dark", "Sinister", "Blasphemous", "Prophetic", "Cyclopean", "Abyssal", "Cosmic", "Unnamed", "Damned", "Cthonic", "Phantasmagoric", "Abberant", "Esoteric", "Stygian", "Dissonant"};
	Godot.Collections.Array noun = new Godot.Collections.Array{"Entity", "Harbinger", "Omen", "Thing", "Creature", "Effigy", "Abomination", "Incarnation", "Tome", "Presence", "Whisper", "Slime", "Banana", "Crab", "Gremlin", "Gourd", "Daemon", "God", "Peanut", "Gargoyle"};
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GenerateName();
	}
	
	void GenerateName()
	{
		String cultName = "";
		switch(GD.RandRange(0, 1))
		{
			case 0:
				//Cult of the Dark Dawn
				cultName = group[GD.RandRange(0, group.Count - 1)] + " of the " + adjective[GD.RandRange(0, adjective.Count - 1)] + " " + noun[GD.RandRange(0, noun.Count - 1)];
				break;
			case 1:
				//The Sinister Devotees of the Lord
				cultName = "The " + adjective[GD.RandRange(0, adjective.Count - 1)] + " " + group[GD.RandRange(0, group.Count - 1)] + " of the " + noun[GD.RandRange(0, noun.Count - 1)];
				break;
		}
		this.Text = cultName;
	}
}
