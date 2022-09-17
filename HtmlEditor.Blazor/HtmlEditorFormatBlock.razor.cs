using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A tool which changes the style of a the selected text by making it a heading or paragraph.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorFormatBlock /&gt;
    /// &lt;/HtmlEditor&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorFormatBlock
    {
        /// <summary>
        /// The HtmlEditor component which this tool is part of.
        /// </summary>
        [CascadingParameter]
        public HtmlEditorComponent Editor { get; set; }

        /// <summary>
        /// Specifies the placeholder displayed to the user. Set to <c>"Format block"</c> by default.
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; } = "Format block";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Text style"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Text style";

        async Task OnChange(string value)
        {
            await Editor.ExecuteCommandAsync("formatBlock", value);
        }
    }

}
