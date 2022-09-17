using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HtmlEditor.Blazor.Rendering;
using System.Threading.Tasks;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// HtmlEditorCheckBox component.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <example>
    /// <code>
    /// &lt;HtmlEditorCheckBox @bind-Value=@someValue TValue="bool" Change=@(args => Console.WriteLine($"Is checked: {args}")) /&gt;
    /// </code>
    /// </example>
    public partial class HtmlEditorCheckBox<TValue> : FormComponent<TValue>
    {
        /// <summary>
        /// Gets or sets a value indicating whether is tri-state (true, false or null).
        /// </summary>
        /// <value><c>true</c> if tri-state; otherwise, <c>false</c>.</value>
        [Parameter]
        public bool TriState { get; set; } = false;

        ClassList BoxClassList => ClassList.Create("html-editor-chkbox-box")
                                           .Add("html-editor-state-active", !object.Equals(Value, false))
                                           .AddDisabled(Disabled);

        ClassList IconClassList => ClassList.Create("html-editor-chkbox-icon")
                                            .Add("hei hei-check", object.Equals(Value, true))
                                            .Add("hei hei-times", object.Equals(Value, null));

        string CheckBoxValue => CheckBoxChecked ? "true" : "false";

        bool CheckBoxChecked => object.Equals(Value, true);

        /// <inheritdoc />
        protected override string GetComponentCssClass()
        {
            return GetClassList("html-editor-chkbox").ToString();
        }

        async Task OnKeyPress(KeyboardEventArgs args)
        {
            if (args.Code == "Space")
            {
                await Toggle();
            }
        }

        async Task Toggle()
        {
            if (Disabled)
            {
                return;
            }

            if (object.Equals(Value, false))
            {
                if (TriState)
                {
                    Value = default;
                }
                else
                {
                    Value = (TValue)(object)true;
                }
            }
            else if (Value == null)
            {
                Value = (TValue)(object)true;
            }
            else if (object.Equals(Value, true))
            {
                Value = (TValue)(object)false;
            }

            await ValueChanged.InvokeAsync(Value);
            if (FieldIdentifier.FieldName != null) { EditContext?.NotifyFieldChanged(FieldIdentifier); }
            await Change.InvokeAsync(Value);
        }
    }
}
