using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_ExitWithoutSaveCommand
{

    [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.SaveGame))]
    internal static class SaveManager_Save_Patch
    {
        public static bool DoNotSave { get; set; } = false;

        public static bool Prefix()
        {
            if (!DoNotSave) return true;

            DoNotSave = false;
            return false;
        }
    }
}
