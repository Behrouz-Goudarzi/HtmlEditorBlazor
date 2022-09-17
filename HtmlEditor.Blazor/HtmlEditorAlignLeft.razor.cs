using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which aligns the selection to the left.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorAlignLeft /&gt;
    /// &lt;/HtmlEdito&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorAlignLeft : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "justifyLeft";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Align left"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Align left";
    }
}
