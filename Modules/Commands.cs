using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using System.IO;
using System.IO.Compression;
using System;
using System.Threading;
using Discord.WebSocket;

namespace InputOutput.Modules
{
    public class Commands :  ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task Test()
        {
            var ping = new EmbedBuilder()
            {
                Title = "Checking ping!",
                Description = "This may take a while depending on the actual ping!"
            };
        }

        [Command("rt")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        [RequireBotPermission(GuildPermission.AttachFiles)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RT([Remainder] string filelocal)
        {
            //Embeds
            var builderfile = new EmbedBuilder()
            {
                Title = "Grabbing file, just for you!",
                Description = "This may take a while depending on the file size and upload speed"
            };

            var builderfolder = new EmbedBuilder()
            {
                Title = "Grabbing folder, just for you!",
                Description = "This may take a while depending on the folder's size and upload speed"
            };

            var filenotfound = new EmbedBuilder()
            {
                Title = "File not found.... sorry",
                Description = "This file could not be found, maybe check for typo's or use io.tree [PATH TO DIRECTORY] \nto see what files are in the directory"
            };

            var filesent = new EmbedBuilder()
            {
                Title = "Here you go!",
                Description = "This file could not be found, maybe check for typo's or use io.tree [PATH TO DIRECTORY] \nto see what files are in the directory"
            };

            if (File.Exists(filelocal))
            {

            }
            else
            {
                if (Directory.Exists(filelocal))
                {
                    var embedsent = await ReplyAsync(embed: builderfolder.Build());
                }
                else
                {

                }


            }




            FileAttributes attr = File.GetAttributes(filelocal);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                
            }
            else
            {
                var embedsent = await ReplyAsync(embed: builderfile.Build());

                if (File.Exists(filelocal))
                {
                    var messages = await Context.Message.Channel.GetMessagesAsync(1).FlattenAsync();
                    await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

                    var sentfile = await Context.Channel.SendFileAsync(filelocal, "Here you go!");
                }
                else
                {
                    
                }
            }

        }

        [Command("retrieve")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        [RequireBotPermission(GuildPermission.AttachFiles)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Retrieve([Remainder] string filelocal)
        {
            var builder = new EmbedBuilder()
            {
                Title = "Grabbing file, just for you!",
                Description = "This may take a while depending on the file size and upload speed"
            };
            await ReplyAsync(embed: builder.Build());
            await Context.Channel.SendFileAsync(filelocal, "Here you go!");
        }

        [Command("zip")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        [RequireBotPermission(GuildPermission.AttachFiles)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Zip([Remainder] string foldertozip)
        {
            var sendgrabfile = ReplyAsync("Grabbing file!");
            if (Directory.Exists(foldertozip))
            {
                string folder = foldertozip;
                string zipped = @".\result.zip";

                ZipFile.CreateFromDirectory(folder, zipped);

                await Context.Channel.SendFileAsync(zipped, "Here you go!");
            }
            else
            {
                await ReplyAsync("Folder not found.. sorry");
            }

        }
    }
}
