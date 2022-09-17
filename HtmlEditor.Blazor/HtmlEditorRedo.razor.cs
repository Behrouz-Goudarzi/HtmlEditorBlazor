using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which repeats the last undone command.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorRedo /&gt;
    /// &lt;/HtmlEditor&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorRedo : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "redo";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Redo"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Redo";
    }
}
