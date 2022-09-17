using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor tool which inserts an ordered list (<c>ol</c>).
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorOrderedList /&gt;
    /// &lt;/HtmlEditor&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorOrderedList : HtmlEditorButtonBase
    {
        /// <inheritdoc />
        protected override string CommandName => "insertOrderedList";

        /// <summary>
        /// Specifies the title (tooltip) displayed when the user hovers the tool. Set to <c>"Ordered list"</c> by default.
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "Ordered list";
    }
}
