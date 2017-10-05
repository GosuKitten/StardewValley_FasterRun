using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Main
{
    public class Main : Mod
    {
        ModConfig config;
        int addedSpeed;

        public override void Entry(IModHelper helper)
        {
            config = helper.ReadConfig<ModConfig>();
            
            SaveEvents.AfterLoad += SaveEvents_AfterLoad;
        }

        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            int totalSpeed = config.runSpeed;

            if (totalSpeed > 1)
            {
                addedSpeed = totalSpeed - 5;
                Monitor.Log("FasterRun run speed is set to " + totalSpeed, LogLevel.Debug);
                GameEvents.UpdateTick += GameEvents_UpdateTick;
            }
            else
            {
                Monitor.Log("Speed value of " + config.runSpeed + " provided in config.JSON" + "is an invalid speed value. Only intergers (whole numbers) that are higher than 0 are allowed.", LogLevel.Error);
            }
        }

        private void GameEvents_UpdateTick(object sender, EventArgs e)
        {
            Game1.player.addedSpeed = addedSpeed;
        }
    }

    class ModConfig
    {
        public int runSpeed = 7;
    }
}
