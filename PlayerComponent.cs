using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DNI
{
    class PlayerComponent : UnturnedPlayerComponent
    {
        public IEnumerator showDNI(UnturnedPlayer player, UnturnedPlayer player2)
        {
            EffectManager.sendUIEffect(19662, 19663, player.CSteamID, true, player2.CharacterName);
            yield return new WaitForSeconds(5);
            EffectManager.askEffectClearByID(19662, player.CSteamID);
        }
    }
}
