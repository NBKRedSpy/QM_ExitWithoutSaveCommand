using MGSC;
using System.Collections.Generic;
using UnityEngine;

namespace QM_ExitWithoutSaveCommand
{
    [ConsoleCommand(new string[] { "exit-no-save" })]
    public class ExitWithoutSaveCommand
    {
        public static string CommandName { get; set; } = "exit-no-save";

        public static string Help(string command, bool verbose)
        {
            return "Exits the dungeon without saving the game.";
        }

        public string Execute(string[] tokens)
        {
            ConsoleDaemon dameon = UI.Get<DevConsole>().Daemon; 

            if (!IsAvailable())
            {
                dameon.PrintText($"Cannot execute, not in dungeon mode");
                return "";
            }

            SaveManager_Save_Patch.DoNotSave = true;


            dameon.PrintText($"Exiting without save");

            UI.Hide<EscScreen>();

            SingletonMonoBehaviour<DungeonGameMode>.Instance.FinishGame(new DungeonFinishedData
            {
                Reason = GameFinishedReason.ExitToMainMenu,
            });

            if (SaveManager_Save_Patch.DoNotSave)
            {
                string errmsg = $"The DoNotSave flag was not reset after expected save event";
                Debug.LogError(errmsg);
                dameon.PrintText($"Failed!  {errmsg}");
            }

            return "done!";
        }

        public static List<string> FetchAutocompleteOptions(string command, string[] tokens)
        {
            return null;
        }

        public static bool IsAvailable()
        {
            return DungeonGameMode.Instance != null;
        }

        public static bool ShowInHelpAndAutocomplete()
        {
            return true;
        }
    }
}