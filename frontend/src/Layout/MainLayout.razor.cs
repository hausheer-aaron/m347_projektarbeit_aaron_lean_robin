using MudBlazor;

namespace WeatherApp.Frontend.Layout;

public partial class MainLayout
{
    private readonly MudTheme _theme = new()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#6366f1",
            Secondary = "#8b5cf6",
            Background = "#f0f4f8",
            Surface = "#ffffff",
            TextPrimary = "#1e293b",
            TextSecondary = "#64748b",
            AppbarBackground = "#ffffff",
        },
        Typography = new Typography()
        {
            Default = new DefaultTypography() { FontFamily = ["Inter", "Segoe UI", "Helvetica Neue", "Arial", "sans-serif"] },
        }
    };
}
