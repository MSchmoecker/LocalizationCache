using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace LocalizationCache {
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Plugin : BaseUnityPlugin {
        public const string ModName = "LocalizationCache";
        public const string ModGuid = "com.maxsch.valheim.LocalizationCache";
        public const string ModVersion = "0.1.0";

        internal static ManualLogSource Log { get; private set; }
        private Harmony harmony;

        private void Awake() {
            Log = Logger;

            harmony = new Harmony(ModGuid);
            harmony.PatchAll();
        }
    }
}
