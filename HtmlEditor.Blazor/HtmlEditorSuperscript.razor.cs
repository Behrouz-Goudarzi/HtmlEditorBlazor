using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which formats the selection as superscript.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorSuperscript /&gt;
    /// &lt;/HtmlEditor&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorSuperscript : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "superscript";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Superscript"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Superscript";
    }
}
