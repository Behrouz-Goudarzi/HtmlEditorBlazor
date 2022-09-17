using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which sets the background color of the selection.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorBackground /&gt;
    /// &lt;/HtmlEdito&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorBackground : HtmlEditorColorBase
    {
        /// <inheritdoc />
        protected override string CommandName => "backColor";

        /// <summary>
        /// Specifies the default background color. Set to <c>"rgb(0, 0, 255)"</c> by default;
        /// </summary>
        [Parameter]
        public string Value { get; set; } = "rgb(0, 0, 255)";
        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Background color"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Background color";
    }
}
