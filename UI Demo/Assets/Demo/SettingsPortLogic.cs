/// #LogicScript

using System.IO;
using UnityEngine;

namespace Demo
{
	public static class SettingsPortLogic
	{
		public static void ImportSettingsFromJson(ref Settings settings, string path)
		{
			// File Exists Guard
			if (!File.Exists(path))
			{
				Debug.LogErrorFormat("Cannot import Settings from {0}", path);
				return;
			}

			// Read file content
			string json = "";
			using (var reader = new StreamReader(path))
			{
				json = reader.ReadToEnd();
				reader.Close();
			}

			// Convert .json content to data
			var settingsCache = JsonUtility.FromJson<SettingsJson>(json);
			if (settingsCache == null)
			{
				Debug.LogErrorFormat("Cannot import Settings from {0}", path);
				return;
			}
			settings.displaySettings = settingsCache.displaySettings;
			settings.audioSettings = settingsCache.audioSettings;
		}

		public static void ExportSettingsToJson(Settings settings, string path, bool prettyPrint = false)
		{
			// Create directory if it does not exist
			if (!Directory.Exists(path)) Directory.CreateDirectory(Path.GetDirectoryName(path));

			// Convert data to .json content and write to file
			var settingsJson = new SettingsJson()
			{
				displaySettings = settings.displaySettings,
				audioSettings = settings.audioSettings,
			};
			var json = JsonUtility.ToJson(settingsJson, prettyPrint);
			using (var writer = new StreamWriter(path))
			{
				writer.Write(json);
				writer.Close();
			}
		}
	}
}
