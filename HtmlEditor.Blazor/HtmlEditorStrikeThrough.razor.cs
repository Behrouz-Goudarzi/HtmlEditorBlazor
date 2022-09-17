using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which applies "strike through" styling to the selection.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorStrikeThrough /&gt;
    /// &lt;/HtmlEditor&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorStrikeThrough : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "strikeThrough";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Strikethrough"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Strikethrough";
    }
}
