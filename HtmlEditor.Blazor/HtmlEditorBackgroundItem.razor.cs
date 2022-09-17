using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// Adds a custom color to <see cref="HtmlEditorBackground" />.
    /// </summary>
    /// <example>
    /// <code>
    ///  &lt;HtmlEditorBackground &gt;
    ///     &lt;HtmlEditorBackgroundItem Value="red" /&gt;
    ///     &lt;HtmlEditorBackgroundItem Value="green" /&gt;
    ///  &lt;/HtmlEditorBackground &gt;
    /// </code>
    /// </example>
    public partial class HtmlEditorBackgroundItem
    {
        /// <summary>
        /// The custom color to add.
        /// </summary>
        [Parameter]
        public string Value { get; set; }
    }
}
