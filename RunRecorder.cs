using System;
using System.Collections.Generic;
using System.IO;
using ArkReplay.Replay;
using ArkReplay.Replay.Battle;
using GameDataEditor;
using Newtonsoft.Json;
using UnityEngine;
using Action = ArkReplay.Replay.Action;

namespace ArkReplay
{
    /// <summary>
    /// Records runs.
    /// </summary>
    public class RunRecorder : MonoBehaviour
    {
        /// <summary>
        /// RunRecorder singleton.
        /// </summary>
        public static RunRecorder Instance { get; private set; }

        /// <summary>
        /// Returns <code>true</code> if the recorder is recording.
        /// </summary>
        public static bool Recording
        {
            get => Instance?._recording ?? false;
        }

        /// <summary>
        /// Whether the recorder should record each run.
        /// </summary>
        public bool alwaysRecord = true;

        private bool _recording;

        private RunInfo currentRunInfo;
        private List<Action> currentRunActions;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Debug.Log("ArkReplay run recorder initialized.");
            }
            else
            {
                Debug.LogError("Run recorder initialized twice!");
            }
        }

        internal static void DestroyInstance()
        {
            UnityEngine.Object.Destroy(Instance.gameObject);
            Instance = null;
        }

        /// <summary>
        /// Begins recording a run.
        /// </summary>
        /// <param name="info">The info of the start.</param>
        public void Begin(RunInfo info)
        {
            Debug.Log("ArkReplay start recording.");
            _recording = true;

            currentRunInfo = info;
            currentRunActions = new List<Action>();
        }

        /// <summary>
        /// Records an action .
        /// </summary>
        /// <param name="action">The action to record.</param>
        public void Record(IAction action)
        {
            Debug.Log($"ArkReplay recorded: {action}");

            currentRunActions.Add(new Replay.Action(action));
        }

        /// <summary>
        /// Takes a snapshot of the game's current state and records it.
        /// </summary>
        public void RecordSnapshot()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the current run to a timestamped file.
        /// </summary>
        /// <returns>The path of the file.</returns>
        public string Save()
        {
            // make sure out dir exists
            Directory.CreateDirectory(Plugin.ReplayDir);

            // generate filename
            var now = DateTime.Now;
            var filename = $"replay-{now.Year}-{now.Month}-{now.Day}-{now.Hour}"
                + $"-{now.Minute}-{now.Second}.arkr";
            var path = Path.Combine(Plugin.ReplayDir, filename);

            Save(path);

            return path;
        }

        /// <summary>
        /// Saves the current run to a file and stops recording.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        public void Save(string path)
        {
            if (!_recording)
                throw new InvalidOperationException("Attemmpting to save recording without recording.");

            var runRecord = new RunRecord(currentRunInfo, currentRunActions);

            using FileStream stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None);
            using StreamWriter writer = new StreamWriter(stream);

            JsonTextWriter jsonWriter = new JsonTextWriter(writer);
            JsonSerializer serializer = new JsonSerializer();

            serializer.Serialize(jsonWriter, runRecord);

            Debug.Log($"ArkReplay wrote replay to \"{path}\"!");

            // clear actions to free unused memory
            currentRunActions = null;

            _recording = false;
        }

        void OnApplicationQuit()
        {
            if (_recording) Save();
        }
    }
}