using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which inserts a bullet list (<c>ul</c>).
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorUnorderedList /&gt;
    /// &lt;/HtmlEditor&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorUnorderedList : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "insertUnorderedList";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Bullet list"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Bullet list";
    }
}
