using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which removes the styling of the selection.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorRemoveFormat /&gt;
    /// &lt;/HtmlEditor&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorRemoveFormat : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "removeFormat";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Remove styling"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Remove styling";
    }
}
