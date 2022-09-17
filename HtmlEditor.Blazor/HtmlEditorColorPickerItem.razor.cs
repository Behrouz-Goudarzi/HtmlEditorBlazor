using Microsoft.AspNetCore.Components;
using HtmlEditor.Blazor.Rendering;
using System.Threading.Tasks;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// HtmlEditorColorPickerItem component.
    /// </summary>
    public partial class HtmlEditorColorPickerItem
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Parameter]
        public string Value { get; set; }

        string Background
        {
            get
            {
                RGB rgb = RGB.Parse(Value);

                return rgb?.ToCSS();
            }
        }

        /// <summary>
        /// Gets or sets the color picker.
        /// </summary>
        /// <value>The color picker.</value>
        [CascadingParameter]
        public HtmlEditorColorPicker ColorPicker { get; set; }

        async Task OnClick()
        {
            await ColorPicker.SelectColor(Value);
        }
    }
}
