using System;
using System.IO;
using System.Linq;
using ArkReplay.Replay;
using GameDataEditor;
using Newtonsoft.Json;
using UnityEngine;
using Action = ArkReplay.Replay.Action;

namespace ArkReplay
{
    /// <summary>
    /// Replays runs.
    /// </summary>
    public class RunReplayer : MonoBehaviour
    {
        /// <summary>
        /// RunReplayer singleton.
        /// </summary>
        public static RunReplayer Instance { get; private set; }

        /// <summary>
        /// If the replayer is replaying a run.
        /// </summary>
        public static bool Replaying { get => Instance._replaying; }

        /// <summary>
        /// How long the replayer will wait between actions.
        /// </summary>
        public static float StepTime
        {
            get => Instance._stepTime;
            set => Instance._stepTime = value;
        }

        private float _stepTime = 0.5f;

        private float _stepTimer;

        private bool _replaying;

        private RunRecord _record;

        private int _actionCursor;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Debug.Log("ArkReplay run replayer initialized.");
            }
            else
            {
                Debug.LogError("Run replayer initialized twice!");
            }
        }

        void Update()
        {
            if (_replaying) ReplayUpdate();
        }

        private void ReplayUpdate()
        {
            var topAction = CurrentAction();

            // tick steptimer
            if (_stepTimer > 0 && topAction.Ready())
                _stepTimer -= Time.deltaTime;

            if (_stepTimer <= 0) MoveNextAction();
        }

        private void MoveNextAction()
        {
            try
            {
                var currentAction = CurrentAction();
                Debug.Log($"ArkReplay replaying action: {currentAction}");
                currentAction.Replay();
            }
            catch (Exception e) //c# devs forgive my sins
            {
                Debug.LogError($"ERROR OCCURED REPLAYING: {e.Message}");
                Debug.LogError(e.StackTrace);
                StopReplay();
                return;
            }

            // pop action
            _actionCursor++;

            if (_record.actions.Count > _actionCursor)
            {
                // reset steptimer
                _stepTimer = _stepTime;
            }
            else
            {
                Debug.Log("ArkReplay finished replaying");
                StopReplay();
            }
        }

        void OnDestroy()
        {
            Instance = null;
        }

        private void StartRun()
        {
            // seed run
            RunSeeder.NextSeed = _record.info.seed;

            PlayData.init();
            SaveManager.savemanager.TempSave = new TempSaveData();
            PlayData.TSavedata.DeletedFile = false;
            
            // disable achievos for the trolls
            PlayData.TSavedata.CantStoryAndAchievethisrun = true;

            switch (_record.info.difficulty)
            {
                case Difficulty.Normal:
                    SaveManager.NowData.GameOptions.Difficulty = 0;
                    break;
                case Difficulty.Expert:
                    SaveManager.NowData.GameOptions.Difficulty = 2;
                    break;
            }

            // might want to reverse patch this
            PlayData.TSavedata.Party.Clear();
            PlayData.TSavedata.NowMaxMemberNum = 0;
            PlayData.TSavedata.LucyInfo = new Character();
            PlayData.TSavedata.LucyInfo.LucyInit();
            PlayData.TSavedata.DonAliveChars.Clear();
            PlayData.TSavedata.LucySkills.Clear();
            PlayData.TSavedata.LucySkills.Add(GDEItemKeys.Skill_S_Lucy_0);
            PlayData.TSavedata.LucySkillList_Legendary.Add(GDEItemKeys.Skill_S_Lucy_0);
            PlayData.TSavedata.IdentifyItems.Add(GDEItemKeys.Item_Scroll_Scroll_Identify);
            if (SaveManager.Difficalty != 2)
            {
                PlayData.TSavedata.IdentifyItems.Add(GDEItemKeys.Item_Scroll_Scroll_Uncurse);
                PlayData.TSavedata.IdentifyItems.Add(GDEItemKeys.Item_Scroll_Scroll_Midas);
                PlayData.TSavedata.PassiveList.Add(GDEItemKeys.Item_Passive_CursedMask);
            }

            FieldSystem.instance.StageStart();
        }

        /// <summary>
        /// Loads and deserializes a record, and starts playing it.
        /// </summary>
        /// <param name="path">The record to load.</param>
        public void Load(string path)
        {
            try
            {
                using FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);
                using StreamReader reader = new StreamReader(stream);

                JsonTextReader jsonReader = new JsonTextReader(reader);
                JsonSerializer serializer = new JsonSerializer();

                var record = serializer.Deserialize<RunRecord>(jsonReader);

                Debug.Log($"ArkReplay read replay from \"{path}\"!");

                StartReplay(record);
            }
            catch (Exception e) //forgive my sins c# devs
            {
                Debug.LogError($"REPLAY FAILURE: {e.Message}");
                Debug.LogError(e.StackTrace);
            }
        }

        /// <summary>
        /// Starts replaying a record.
        /// </summary>
        /// <param name="record">The record to replay.</param>
        public void StartReplay(RunRecord record)
        {
            _replaying = true;
            _record = record;
            _actionCursor = 0;

            StartRun();
        }

        public Action CurrentAction()
        {
            return _record.actions[_actionCursor];
        }

        public void StopReplay()
        {
            _replaying = false;
            _record = null;
        }

        /// <summary>
        /// Stops the replay like <see cref="StopReplay"/>, but also checks the
        /// record for extraneous actions.
        /// </summary>
        public void FinishReplay()
        {
            StopReplay();

            if (_actionCursor < _record.actions.Count)
            {
                Debug.LogWarning("ERROR: Replay finished, but actions remaining:");
                foreach (Action action in _record.actions.Skip(_actionCursor))
                {
                    Debug.LogWarning(action);
                }
            }
        }
    }
}