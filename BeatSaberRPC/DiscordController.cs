using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Discord;

namespace BeatSaberRPC
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
	public class DiscordController : MonoBehaviour
    {
        public long appID = 1114064865951240252;

        public long gameStartTime;

        private static bool instanceExists;
        public Discord.Discord discord;

        void Awake()
        {
            if (!instanceExists)
            {
                instanceExists = true;
                DontDestroyOnLoad(gameObject);
            } else if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            discord = new Discord.Discord(appID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
            gameStartTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        void Update()
        {
            try
            {
                discord.RunCallbacks();
            } catch
            {
                Destroy(gameObject);
            }
        }

        public void UpdatePresence(Discord.Activity activity)
        {
            try
            {
                var activityManager = discord.GetActivityManager();
                activityManager.UpdateActivity(activity, (res) =>
                {
                    if (res != Discord.Result.Ok) Plugin.Log.Error("Failed connecting to Discord!");
                });
            } catch
            {
                Destroy(gameObject);
            }
        }
    }
}
