using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// Adds a custom font to a <see cref="HtmlEditorFontName" />.
    /// </summary>
    /// <example>
    /// <code>
    ///  &lt;HtmlEditorFontName&gt;
    ///  &lt;HtmlEditorFontNameItem Text="Times New Roman" Value='"Times New Roman"' /&gt;
    ///  &lt;/HtmlEditorFontName&gt;
    /// </code>
    /// </example>
    public partial class HtmlEditorFontNameItem
    {
        /// <summary>
        /// The name of the font e.g. <c>"Times New Roman"</c>.
        /// </summary>
        [Parameter]
        public string Text { get; set; }

        /// <summary>
        /// The CSS value of the font. Use quotes if it contains spaces.
        /// </summary>
        [Parameter]
        public string Value { get; set; }

        /// <summary>
        /// The HtmlEditorFontName tool which this tool belongs to.
        /// </summary>
        [CascadingParameter]
        public HtmlEditorFontName Parent { get; set; }

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            Parent.AddFont(this);
        }
    }
}
