using GTANetworkAPI;
using System;

public class Teamspeak : Script
{
	[RemoteEvent("ChangeVoiceRange")]
	public static void ChangeVoiceRange(Client Player)
	{
		String voiceRange = "Normal";
		if (NAPI.Data.HasEntitySharedData(Player, "VOICE_RANGE"))
		{
			voiceRange = NAPI.Data.GetEntitySharedData(Player, "VOICE_RANGE");
		}
		switch (voiceRange)
		{
			case "Normal":
				voiceRange = "Weit";
				break;
			case "Weit":
				voiceRange = "Kurz";
				break;
			case "Kurz":
				voiceRange = "Normal";
				break;
		}
		NAPI.Data.SetEntitySharedData(Player, "VOICE_RANGE", voiceRange);
	}

	public static void Connect(Client Player, String characterName)
	{
		Player.TriggerEvent("ConnectTeamspeak");
	}
}