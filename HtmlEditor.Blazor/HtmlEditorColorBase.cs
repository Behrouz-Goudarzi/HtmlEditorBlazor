using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// Base class that HtmlEditor color picker tools inherit from.
    /// </summary>
    public abstract class HtmlEditorColorBase : HtmlEditorButtonBase
    {
        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.ShowHSV" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public bool ShowHSV { get; set; } = true;

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.ShowRGBA" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public bool ShowRGBA { get; set; } = true;

        /// <summary>
        /// Gets or sets the child content.
        /// </summary>
        /// <value>The child content.</value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.ShowColors" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public bool ShowColors { get; set; } = true;

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.ShowButton" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public bool ShowButton { get; set; } = true;

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.HexText" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public string HexText { get; set; } = "Hex";

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.RedText" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public string RedText { get; set; } = "R";

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.GreenText" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public string GreenText { get; set; } = "G";

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.BlueText" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public string BlueText { get; set; } = "B";

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.AlphaText" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public string AlphaText { get; set; } = "A";

        /// <summary>
        /// Sets <see cref="HtmlEditorColorPicker.ButtonText" /> of the built-in HtmlEditorColorPicker.
        /// </summary>
        [Parameter]
        public string ButtonText { get; set; } = "OK";


        /// <summary>
        /// Handles the change event of built-in HtmlEditorColorPicker.
        /// </summary>
        /// <param name="value">The new color.</param>
        protected async Task OnChange(string value)
        {
            await Editor.ExecuteCommandAsync(CommandName, value);
        }
    }
}
