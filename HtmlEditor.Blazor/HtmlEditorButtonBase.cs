using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// Base class that HtmlEditor color picker tools inherit from.
    /// </summary>
    public abstract class HtmlEditorButtonBase : ComponentBase
    {
        /// <summary>
        /// The HtmlEditor component which this tool is part of.
        /// </summary>
        [CascadingParameter]
        public HtmlEditorComponent Editor { get; set; }

        /// <summary>
        /// Specifies the name of the command. It is available as <see cref="HtmlEditorExecuteEventArgs.CommandName" /> when
        /// <see cref="HtmlEditorComponent.Execute" /> is raised.
        /// </summary>
        protected virtual string CommandName { get; }

        /// <summary>
        /// Handles the click event of the button. Executes the command.
        /// </summary>
        protected virtual async Task OnClick()
        {
            await Editor.ExecuteCommandAsync(CommandName);
        }
    }
}
