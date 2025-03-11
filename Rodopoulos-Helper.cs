using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Menu;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rodopoulos_Helper
{
    public class Rodo_HelpConfig : BasePluginConfig
    {
        [JsonPropertyName("HelpCommands")]
        public List<string> HelpCommands { get; set; } = new() { "help", "ajuda" };        

        [JsonPropertyName("CommandItem")]
        public List<HelpItem> CommandItems { get; set; } = new()
        {
            new HelpItem
            {
                Command = "!help",
                Options = new List<CommandHelpOption>
                {
                    new CommandHelpOption {  Description = "Give help to a player"},
                }
            },
            new HelpItem
            {
                Command = "!rtv",
                Options = new List<CommandHelpOption>
                {
                    new CommandHelpOption {  Description = "Rock the vote"}
                }
            }
        };
    }

    public class HelpPlugin : BasePlugin, IPluginConfig<Rodo_HelpConfig>
    {
        public override string ModuleName => "Rodopoulos-Helper";
        public override string ModuleVersion => "1.0.0";
        public override string ModuleAuthor => "Rodopoulos";
        public Rodo_HelpConfig Config { get; set; } = new();

        public void OnConfigParsed(Rodo_HelpConfig config)
        {
            Config = config;
        }

        public override void OnAllPluginsLoaded(bool hotReload)
        {
            CreateCommands();
        }

        private void CreateCommands()
        {
            foreach (var cmd in Config.HelpCommands)
            {
                AddCommand($"css_{cmd}", "Open the help menu", Command_HelpMenu);
            }
        }

        public void Command_HelpMenu(CCSPlayerController? player, CommandInfo info)
        {
            if (player == null) return;

            var menu = new CenterHtmlMenu(Localizer["Help Menu Title"], this);

            foreach (var item in Config.CommandItems)
            {
                menu.AddMenuOption(Localizer["Help Menu Item", item.Command], (client, option) =>
                {
                    OpenSkinsSubMenu(player, item);
                });
            }

            MenuManager.OpenCenterHtmlMenu(this, player, menu);
        }

        private void OpenSkinsSubMenu(CCSPlayerController player, HelpItem item)
        {
            var subMenu = new CenterHtmlMenu(Localizer["Help Menu Options title"], this);
            
            foreach (var option in item.Options)
            {
                subMenu.AddMenuOption(Localizer["Help Menu Options item", option.Description], (client, subOption) => { }, disabled:true);
            }

            // Opção para voltar ao menu principal
            subMenu.AddMenuOption(Localizer["Help Menu Back"], (client, option) =>
            {
                Command_HelpMenu(player, null);
            });

            MenuManager.OpenCenterHtmlMenu(this, player, subMenu);
        }
    }

    public class HelpItem
    {
        public string Command { get; set; }
        public List<CommandHelpOption> Options { get; set; }
    }

    public class CommandHelpOption
    {
        public string Description { get; set; }
    }
}