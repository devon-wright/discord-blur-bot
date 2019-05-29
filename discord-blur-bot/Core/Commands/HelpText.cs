using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

using Discord;
using Discord.Commands;

namespace discord_blur_bot.Core.Commands
{
    public class HelpText : ModuleBase<SocketCommandContext>
    {
        [Command("help"), Alias("HELP-ME", "I-am-in-need-of-some-assistance-brother"), Summary("Help text command")]
        public async Task SendText()
        {
            await Context.Channel.SendMessageAsync("You puny human, I will not help the likes of you");
        }

        [Command("random"), Summary("Grabs a random image from resources and sends it back with the blur")]
        public async Task RandomBlur()
        {
            await Context.Channel.SendMessageAsync("Here is your gift");
        }
    }
}
