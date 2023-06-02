using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BeatSaberRPC
{
    internal class MenuPresenceController : MonoBehaviour, IInitializable, ILateDisposable
    {
        [Inject]
        internal void Construct()
        {
        }

        protected void OnEnable()
        {
            Plugin.DiscordController.UpdatePresence(new Discord.Activity
            {
                Details = "In menus",
                Assets =
                {
                    LargeImage = "logo-icon"
                },
                Timestamps =
                {
                    Start = Plugin.DiscordController.gameStartTime
                }
            });
        }

        public void Initialize()
        {
        }

        public void LateDispose()
        {
        }
    }
}