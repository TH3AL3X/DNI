
using Darkness;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace DNI
{
    public class DNI : RocketPlugin<Config>
    {
        public static DNI Instance { get; set; }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            player.Player.setPluginWidgetFlag(EPluginWidgetFlags.ShowInteractWithEnemy, false);
        }

        protected override void Load()
        {
            Instance = this;
            U.Events.OnPlayerConnected += OnPlayerConnected;
        }

        protected override void Unload()
        {
            // Events
            U.Events.OnPlayerConnected -= OnPlayerConnected;
        }

        public class RaycastHelper
        {
            public static Player GetPlayerFromHits(Player caller, float maxDistance)
            {
                var hits = Physics.RaycastAll(new Ray(caller.look.aim.position, caller.look.aim.forward), maxDistance, RayMasks.PLAYER_INTERACT | RayMasks.PLAYER);
                Player player = null;
                for (int i = 0; i < hits.Length; i++)
                {
                    Player suspect = hits[i].transform.GetComponentInParent<Player>();
                    if (suspect != caller)
                    {
                        player = suspect;
                        break;
                    }
                }
                return player;
            }
        }

        private void UnturnedPlayerEvents_OnPlayerUpdateGesture(UnturnedPlayer player, UnturnedPlayerEvents.PlayerGesture gesture)
        {
            Player victimPlayer = RaycastHelper.GetPlayerFromHits(player.Player, 2);

            UnturnedPlayer victim = UnturnedPlayer.FromPlayer(victimPlayer);
            if (victim != null)
            {
                if (gesture == UnturnedPlayerEvents.PlayerGesture.Point)
                {
                   StartCoroutine(player.Player.GetComponent<PlayerComponent>().showDNI(victim, player));
                }
            }
        }
    }
}
