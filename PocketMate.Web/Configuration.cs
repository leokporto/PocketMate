using MudBlazor.Utilities;
using MudBlazor;

namespace PocketMate.Web
{
    public static class Configuration
    {
        /*
         Pallettes from https://coolors.co/32bea6-fbfbff-1e2d2f-f7cb15-4381c1
        #32BEA6
        #FBFBFF
        #1E2D2F
        #F7CB15
        #4381C1
         */
        public static MudTheme Theme = new()
        {
            Typography = new Typography
            {
                Default = new Default
                {
                    FontFamily = ["Open+Sans", "sans-serif"]
                }
            },
            PaletteLight = new PaletteLight
            {
                Primary = new MudColor("#32bea6"),
                PrimaryContrastText = new MudColor("#1E2D2F"),
                Secondary = new MudColor("#F7CB15"),                                
                Background = new MudColor("#FBFBFF"),
                AppbarBackground = new MudColor("#32bea6"),
                AppbarText = new MudColor("#1E2D2F"),
                TextPrimary = new MudColor("#1E2D2F"),
                DrawerText = new MudColor("#FBFBFF"),
                DrawerBackground = new MudColor("#4381C1"),
            },
            PaletteDark = new PaletteDark
            {
                Primary = new MudColor("#FBFBFF"),
                Background = new MudColor("#1E2D2F"),
                Secondary = new MudColor("#F7CB15"),
                AppbarBackground = new MudColor("#32bea6"),
                AppbarText = new MudColor("#1E2D2F"),
                PrimaryContrastText = new MudColor("#1E2D2F")
            }
        };
    }
}
