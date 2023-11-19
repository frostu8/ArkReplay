using HarmonyLib;
using ChronoArkMod.Plugin;
using UnityEngine;
using ChronoArkMod.ModData;
using ChronoArkMod;
using ChronoArkMod.ModData.Settings;
using System.IO;

namespace ArkReplay
{
    [PluginConfig(GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : ChronoArkPlugin
    {
        public const string GUID = "net.frostu8.ca.arkreplay";

        private Harmony harmony;

        public override void Dispose()
        {
            // destroy run recorder
            RunRecorder.DestroyInstance();

            // undo patches
            harmony.UnpatchSelf();
        }

        public override void OnModSettingUpdate()
        {
            ModInfo info = ModManager.getModInfo(GUID);

            // get out dir
            var setting = info.GetSetting<InputFieldSetting>("OutDir");

            if (setting.Value.IsNullOrEmpty())
            {
                // set setting
                setting.Value = Path.Combine(Application.persistentDataPath, "Replays");
                info.SaveSetting();
            }

            RunRecorder.Instance.OutDir = setting.Value;
        }

        public override void Initialize()
        {
            harmony = new Harmony(GUID);
            harmony.PatchAll();

            // create run recorder
            var runRecorder = new GameObject("RunRecorder");
            runRecorder.AddComponent<RunRecorder>();
            Object.DontDestroyOnLoad(runRecorder);
            
            OnModSettingUpdate();
        }
    }
}