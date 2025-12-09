using MGSC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QM_ExitWithoutSaveCommand
{
    [ConsoleCommand(new string[] { "reload-save", "rs" })]
    public class ReloadSaveCommand
    {
        public static string CommandName { get; set; } = "reload-save";

        public static string Help(string command, bool verbose)
        {
            return "Reloads the current save without saving first.";
        }

        public string Execute(string[] tokens)
        {
            ConsoleDaemon dameon = UI.Get<DevConsole>().Daemon;

            if (!IsAvailable())
            {
                dameon.PrintText($"Cannot execute.  Not currently in space or dungeon mode");
                return "";
            }


            //The DoNotSave should no longer be required, but it doesn't hurt anything
            //  to keep it.
            SaveManager_Save_Patch.DoNotSave = true;

            dameon.PrintText($"Exiting without save");

            UI.Hide<EscScreen>();

            //Debug
            GameModeStateMachine gameModeStateMachine = Plugin.State.Get<GameModeStateMachine>();
            //yield return gameModeStateMachine.StartCoroutine(gameModeStateMachine.GoToMainMenu());
            gameModeStateMachine.StartCoroutine(ReloadSave());

            return "done!";


        }

        public IEnumerator ReloadSave()
        {
            GameModeStateMachine gameModeStateMachine = Plugin.State.Get<GameModeStateMachine>();
            int slot = gameModeStateMachine._state.Get<SavedGameMetadata>().Slot;

            yield return gameModeStateMachine.GoToMainMenu();
            UI.Get<ManageSavesScreen>().SlotOnStartGame(slot, false);
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