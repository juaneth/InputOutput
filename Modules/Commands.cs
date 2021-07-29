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
        [Command("help")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        public async Task Help()
        {
            var HelpEmbed = new EmbedBuilder()
            {
                Title = "Here's a list of commands: ",
            };

            HelpEmbed.AddField("io.RT/io.retrieve",
            "This 'retrieves' a file or folder and sends it as a file!").WithAuthor(Context.Client.CurrentUser).WithFooter(footer => footer.Text = ":)").WithColor(Color.DarkerGrey).WithDescription("This command was called by " + Context.Message.Author).WithCurrentTimestamp();



            await ReplyAsync(embed: HelpEmbed.Build());
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
                Title = "Grabbing file/folder, just for you!",
                Description = "This may take a while depending on the file size and upload speed"
            };

            var filenotfound = new EmbedBuilder()
            {
                Title = "File not found.... sorry",
                Description = "This file could not be found, maybe check for typo's or use io.tree [PATH TO DIRECTORY] \nto see what files are in the directory"
            };

            var filesent = new EmbedBuilder()
            {
                Title = "Here you go!",
                Description = "Use io.help for a command list"
            };

            var embedsent = await ReplyAsync(embed: builderfile.Build());

            if (File.Exists(filelocal))
            {
                //If its a file
                var messages = await Context.Message.Channel.GetMessagesAsync(1).FlattenAsync();
                await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

                var sendfilesent = filesent.Build();
                await Context.Channel.SendFileAsync(embed: sendfilesent, filePath: filelocal);
            }
            else
            {
                if (!Directory.Exists(filelocal))
                {
                    //if it cant be found
                    var messages = await Context.Message.Channel.GetMessagesAsync(1).FlattenAsync();
                    await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

                    await ReplyAsync(embed: filenotfound.Build());
                }
                else
                {
                    //if its a directory
                    FileAttributes attr = File.GetAttributes(filelocal);
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        string folder = filelocal;
                        string zipped = @".\result.zip";

                        if (File.Exists(@".\result.zip"))
                        {
                            File.Delete(@".\result.zip");
                        }

                        ZipFile.CreateFromDirectory(folder, zipped);

                        var messages = await Context.Message.Channel.GetMessagesAsync(1).FlattenAsync();
                        await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

                        await Context.Channel.SendFileAsync(zipped, "Here you go!");
                    }
                }
            }

        }

        [Command("retrieve")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        [RequireBotPermission(GuildPermission.AttachFiles)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Retrieve([Remainder] string filelocal)
        {
            //Embeds
            var builderfile = new EmbedBuilder()
            {
                Title = "Grabbing file/folder, just for you!",
                Description = "This may take a while depending on the file size and upload speed"
            };

            var filenotfound = new EmbedBuilder()
            {
                Title = "File not found.... sorry",
                Description = "This file could not be found, maybe check for typo's or use io.tree [PATH TO DIRECTORY] \nto see what files are in the directory"
            };

            var filesent = new EmbedBuilder()
            {
                Title = "Here you go!",
                Description = "Use io.help for a command list"
            };

            var embedsent = await ReplyAsync(embed: builderfile.Build());

            if (File.Exists(filelocal))
            {
                //If its a file
                var messages = await Context.Message.Channel.GetMessagesAsync(1).FlattenAsync();
                await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

                var sendfilesent = filesent.Build();
                await Context.Channel.SendFileAsync(embed: sendfilesent, filePath: filelocal);
            }
            else
            {
                if (!Directory.Exists(filelocal))
                {
                    //if it cant be found
                    var messages = await Context.Message.Channel.GetMessagesAsync(1).FlattenAsync();
                    await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

                    await ReplyAsync(embed: filenotfound.Build());
                }
                else
                {
                    //if its a directory
                    FileAttributes attr = File.GetAttributes(filelocal);
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        string folder = filelocal;
                        string zipped = @".\result.zip";

                        if (File.Exists(@".\result.zip"))
                        {
                            File.Delete(@".\result.zip");
                        }

                        ZipFile.CreateFromDirectory(folder, zipped);

                        var messages = await Context.Message.Channel.GetMessagesAsync(1).FlattenAsync();
                        await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

                        await Context.Channel.SendFileAsync(zipped, "Here you go!");
                    }
                }
            }
        }
    }
}
