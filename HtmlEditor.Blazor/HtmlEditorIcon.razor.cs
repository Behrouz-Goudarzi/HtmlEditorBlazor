using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// HtmlEditorIcon component. Displays icon from Material Icons font.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditorIcon Icon="3d_rotation" /&gt;
    /// </code>
    /// </example>
    public partial class HtmlEditorIcon : HtmlEditorComponentBase
    {
        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        [Parameter]
        public string Icon { get; set; }

        /// <summary>
        /// Specifies the display style of the icon.
        /// </summary>
        [Parameter]
        public IconStyle? IconStyle { get; set; }

        /// <inheritdoc />
        protected override string GetComponentCssClass()
        {
            return $"hei {(IconStyle.HasValue ? $"hei-{IconStyle.Value.ToString().ToLowerInvariant()} " : "")}d-inline-flex justify-content-center align-items-center";
        }
    }
}
