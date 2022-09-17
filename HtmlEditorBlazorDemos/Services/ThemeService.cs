using System;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace HtmlEditorBlazorDemos
{
    public class ThemeService
    {
        public class Theme
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
        public static readonly Theme[] Themes = new [] 
        {
            new Theme { Text = "Default Theme", Value = "default"}
        };

        public const string DefaultTheme = "default";
        public const string QueryParameter = "theme";

        public string CurrentTheme { get; set; } = DefaultTheme;

        public void Initialize(NavigationManager navigationManager)
        {
            var uri = new Uri(navigationManager.ToAbsoluteUri(navigationManager.Uri).ToString());
            var query = HttpUtility.ParseQueryString(uri.Query);
            var value = query.Get(QueryParameter);

            if (Themes.Any(theme => theme.Value == value))
            {
                CurrentTheme = value;
            }
        }

        public void Change(NavigationManager navigationManager, string theme)
        {
            var url = navigationManager.GetUriWithQueryParameter(QueryParameter, theme);

            navigationManager.NavigateTo(url, true);
        }
    }
}