using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_ExitWithoutSaveCommand
{

    [HarmonyPatch(typeof(GameModeStateMachine), nameof(GameModeStateMachine.GoToMainMenu))]
    internal static class GameModeStateMachine_GoToMainMenu_Patch
    {
        public static void Postfix()
        {
            SaveManager_Save_Patch.DoNotSave = false;
        }
    }
}
