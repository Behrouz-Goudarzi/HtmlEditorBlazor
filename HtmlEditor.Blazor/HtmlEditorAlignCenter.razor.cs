using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which centers the selection.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorAlignCenter /&gt;
    /// &lt;/HtmlEdito&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorAlignCenter : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "justifyCenter";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Align center"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Align center";
    }
}
