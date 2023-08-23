using System.Diagnostics;
using HarmonyLib;

namespace LocalizationCache {
    public static class DebugTimingPatch {
        [HarmonyPatch(typeof(Localization), nameof(Localization.SetupLanguage)), HarmonyPrefix, HarmonyPriority(Priority.First)]
        public static void SetupLanguage_Prefix(Localization __instance, ref Stopwatch __state) {
            __state = Stopwatch.StartNew();
        }

        [HarmonyPatch(typeof(Localization), nameof(Localization.SetupLanguage)), HarmonyPostfix, HarmonyPriority(Priority.Last)]
        public static void SetupLanguage_Postfix(Localization __instance, ref Stopwatch __state) {
            __state.Stop();

            if (Plugin.DebugTiming.Value) {
                Plugin.Log.LogInfo($"Localization.SetupLanguage took {__state.ElapsedMilliseconds}ms");
            }

            if (Plugin.DebugStacktrace.Value) {
                Plugin.Log.LogInfo($"Localization.SetupLanguage was called\n{new StackTrace()}");
            }
        }
    }
}
