using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which bolds the selection.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorBold /&gt;
    /// &lt;/HtmlEdito&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorBold : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "bold";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Bold"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Bold";
    }
}
