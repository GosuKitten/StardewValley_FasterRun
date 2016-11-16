using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;

namespace Main
{
    public class Main : Mod
    {
        private int addedSpeed;

        public override void Entry(params object[] objects)
        {
            StardewModdingAPI.Events.PlayerEvents.LoadedGame += Event_LoadedGame;
        }

        private void Event_LoadedGame(object sender, EventArgs e)
        {
            var ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "Mods\\FasterRun\\FasterRun.ini");

            int totalSpeed;
            int.TryParse(ini.IniReadValue("Config", "runSpeed"), out totalSpeed);

            if (totalSpeed > 0)
            {
                addedSpeed = totalSpeed - 5;
                StardewModdingAPI.Log.Info("FasterRun run speed is set to " + totalSpeed);
                StardewModdingAPI.Events.GameEvents.UpdateTick += GameEvents_UpdateTick;
            }
            else
            {
                StardewModdingAPI.Log.Error("Speed value of " + ini.IniReadValue("Config", "runSpeed") + ", provided in \"" + ini.path + "\", is an invalid speed value. Only intergers (whole numbers) that are higher than 0 are allowed.");
            }
        }

        private void GameEvents_UpdateTick(object sender, EventArgs e)
        {
            StardewValley.Game1.player.addedSpeed = addedSpeed;
        }
    }
}
