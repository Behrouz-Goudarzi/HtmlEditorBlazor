using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which sets the text color of the selection.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorColor /&gt;
    /// &lt;/HtmlEditor /&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorColor : HtmlEditorColorBase
    {
        /// <inheritdoc />
        protected override string CommandName => "foreColor";

        /// <summary>
        /// Specifies the default text color. Set to <c>"rgb(255, 0, 0)"</c> by default;
        /// </summary>
        [Parameter]
        public string Value { get; set; } = "rgb(255, 0, 0)";
        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Text color"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Text color";
    }
}
