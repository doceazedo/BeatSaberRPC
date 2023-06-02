using System;
using Zenject;
using Discord;


namespace BeatSaberRPC
{
    internal class GamePresenceController: IInitializable, ILateDisposable
    {

        private readonly IGamePause gamePause;
        private readonly AudioTimeSyncController audioTimeSyncController;
        private readonly GameplayCoreSceneSetupData gameplayCoreSceneSetupData;

        internal GamePresenceController(AudioTimeSyncController audioTimeSyncController, GameplayCoreSceneSetupData gameplayCoreSceneSetupData, IGamePause gamePause)
        {
            this.audioTimeSyncController = audioTimeSyncController;
            this.gameplayCoreSceneSetupData = gameplayCoreSceneSetupData;
            this.gamePause = gamePause;
        }

        public void Initialize()
        {
            Plugin.Log.Info($"Song name: {gameplayCoreSceneSetupData.difficultyBeatmap.level.songName}");
            SetGamePresence();
            if (gamePause != null)
            {
                gamePause.didPauseEvent += SetPausedPresence;
                gamePause.didResumeEvent += SetGamePresence;
            }
        }

        public void LateDispose()
        {
            if (gamePause != null)
            {
                gamePause.didPauseEvent -= SetPausedPresence;
                gamePause.didResumeEvent -= SetGamePresence;
            }
        }

        private void SetGamePresence()
        {
            string largeImageText = $"{gameplayCoreSceneSetupData.difficultyBeatmap.level.beatsPerMinute} BPM";
            string mapper = gameplayCoreSceneSetupData.difficultyBeatmap.level.levelAuthorName;
            if (mapper != null) largeImageText += $" | Mapped by: {mapper}";

            Plugin.DiscordController.UpdatePresence(new Activity
            {
                Details = $"{gameplayCoreSceneSetupData.difficultyBeatmap.level.songName} [{gameplayCoreSceneSetupData.difficultyBeatmap.difficulty.Name()}]",
                State = gameplayCoreSceneSetupData.difficultyBeatmap.level.songAuthorName,
                Assets =
                {
                    LargeImage = "logo-icon",
                    LargeText = largeImageText
                },
                Timestamps =
                {
                    Start = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                    End = DateTimeOffset.UtcNow.AddSeconds(audioTimeSyncController.songLength - audioTimeSyncController.songTime).ToUnixTimeMilliseconds()
                }
            });
        }

        private void SetPausedPresence()
        {
            Plugin.DiscordController.UpdatePresence(new Activity
            {
                Details = "Paused",
                Assets =
                {
                    LargeImage = "logo-icon"
                },
            });
        }
    }
}
