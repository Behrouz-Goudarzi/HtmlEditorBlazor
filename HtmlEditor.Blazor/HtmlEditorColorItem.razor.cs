using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// Adds a custom color to <see cref="HtmlEditorColor" />.
    /// </summary>
    /// <example>
    /// <code>
    ///  &lt;HtmlEditorColor &gt;
    ///     &lt;HtmlEditorColorItem Value="red" /&gt;
    ///     &lt;HtmlEditorColorItem Value="green" /&gt;
    ///  &lt;/HtmlEditorColor &gt;
    /// </code>
    /// </example>
    public partial class HtmlEditorColorItem
    {
        /// <summary>
        /// The custom color to add.
        /// </summary>
        [Parameter]
        public string Value { get; set; }
    }
}
