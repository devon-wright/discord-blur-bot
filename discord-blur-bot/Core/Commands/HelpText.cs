using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

using Discord;
using Discord.Commands;


namespace discord_blur_bot.Core.Commands
{
    public class HelpText : ModuleBase<SocketCommandContext>
    {
        private List<AForge.Imaging.Filters.IFilter> listoffilters = new List<AForge.Imaging.Filters.IFilter>();

        [Command("help"), Alias("HELP-ME", "I-am-in-need-of-some-assistance-brother"), Summary("Help text command")]
        public async Task SendText()
        {
            await Context.Channel.SendMessageAsync("You puny human, I will not help the likes of you");
        }

        [Command("random"), Summary("Grabs a random image from resources and sends it back with the blur")]
        public async Task RandomBlur()
        {
            await Context.Channel.SendMessageAsync("I will see whats on the MENU");

            // load an image
            Bitmap image = (Bitmap)Bitmap.FromFile(@"D:\Main Folder\Personal Projects\discord-blur-bot\discord-blur-bot\discord-blur-bot\Core\Data\Image bank\test.jpg");

            //setup filters
            AForge.Imaging.Filters.FiltersSequence filter = new AForge.Imaging.Filters.FiltersSequence();

            filter.Add(new AForge.Imaging.Filters.ResizeBilinear(1080, 720));

            listoffilters.Add(new AForge.Imaging.Filters.AdditiveNoise(new AForge.Math.Random.UniformGenerator(new AForge.Range(-50, 50))));
            //filter.Add(new AForge.Imaging.Filters.BayerDithering());
            listoffilters.Add(new AForge.Imaging.Filters.Blur());
            //filter.Add(new AForge.Imaging.Filters.CannyEdgeDetector());
            listoffilters.Add(new AForge.Imaging.Filters.GammaCorrection(0.5));
            listoffilters.Add(new AForge.Imaging.Filters.GaussianBlur(4, 11));
            listoffilters.Add(new AForge.Imaging.Filters.GaussianSharpen(4, 11));
            //filter.Add(new AForge.Imaging.Filters.Sepia());
            listoffilters.Add(new AForge.Imaging.Filters.Jitter(4));
            listoffilters.Add(new AForge.Imaging.Filters.OilPainting(10));
            listoffilters.Add(new AForge.Imaging.Filters.Pixellate());
            //filter.Add(new AForge.Imaging.Filters.RotateBilinear(45));
            //filter.Add(new AForge.Imaging.Filters.GaussianSharpen(4, 11));

            foreach (AForge.Imaging.Filters.IFilter currfilter in listoffilters)
            {
                Random randgen = new Random();
                if (randgen.Next(0, 2) == 1)
                {
                    filter.Add(currfilter);
                }
            }

            // create filter
            //AForge.Imaging.Filters.Median filter = new AForge.Imaging.Filters.Median();

            // apply filter
            System.Drawing.Bitmap newImage = filter.Apply(image); //applyinplace

            newImage.Save(@"D:\Main Folder\Personal Projects\discord-blur-bot\discord-blur-bot\discord-blur-bot\Core\Data\Image bank\test-formatted.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            await Context.Channel.SendFileAsync(@"D:\Main Folder\Personal Projects\discord-blur-bot\discord-blur-bot\discord-blur-bot\Core\Data\Image bank\test-formatted.jpg", "here is your gift");
        }
    }
}
