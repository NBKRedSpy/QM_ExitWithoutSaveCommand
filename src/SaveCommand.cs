using MGSC;
using System.Collections.Generic;
using UnityEngine;

namespace QM_ExitWithoutSaveCommand
{
    [ConsoleCommand(new string[] { "save" })]
    public class ExitSave
    {
        public static string CommandName { get; set; } = "exit-no-save";

        public static string Help(string command, bool verbose)
        {
            return "Saves the GameExits a game without saving.";
        }

        public string Execute(string[] tokens)
        {

            Plugin.State.Get<SaveManager>().SaveGame(takeScreenShot: true, isAutoSave: false);

            return "done!";
        }

        public static List<string> FetchAutocompleteOptions(string command, string[] tokens)
        {
            return null;
        }
        public static bool IsAvailable()
        {
            return Plugin.State.Get<DungeonGameMode>() != null || Plugin.State.Get<SpaceGameMode>() != null;
        }

        public static bool ShowInHelpAndAutocomplete()
        {
            return true;
        }
    }
}