﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// HtmlEditorButton component.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditorButton Click=@(args => Console.WriteLine("Button clicked")) Text="Button" /&gt;
    /// </code>
    /// </example>
    public partial class HtmlEditorButton : HtmlEditorComponentBase
    {
        private string getButtonSize()
        {
            return Size == ButtonSize.Medium ? "md" : "sm";
        }

        /// <summary>
        /// Gets or sets the child content.
        /// </summary>
        /// <value>The child content.</value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Parameter]
        public string Text { get; set; } = "";

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        [Parameter]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        [Parameter]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the button style.
        /// </summary>
        /// <value>The button style.</value>
        [Parameter]
        public ButtonStyle ButtonStyle { get; set; } = ButtonStyle.Primary;

        /// <summary>
        /// Gets or sets the type of the button.
        /// </summary>
        /// <value>The type of the button.</value>
        [Parameter]
        public ButtonType ButtonType { get; set; } = ButtonType.Button;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        [Parameter]
        public ButtonSize Size { get; set; } = ButtonSize.Medium;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HtmlEditorButton"/> is disabled.
        /// </summary>
        /// <value><c>true</c> if disabled; otherwise, <c>false</c>.</value>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the click callback.
        /// </summary>
        /// <value>The click callback.</value>
        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance busy text is shown.
        /// </summary>
        /// <value><c>true</c> if this instance busy text is shown; otherwise, <c>false</c>.</value>
        [Parameter]
        public bool IsBusy { get; set; }

        /// <summary>
        /// Gets or sets the busy text.
        /// </summary>
        /// <value>The busy text.</value>
        [Parameter]
        public string BusyText { get; set; } = "";

        /// <summary>
        /// Gets a value indicating whether this instance is disabled.
        /// </summary>
        /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
        public bool IsDisabled { get => Disabled || IsBusy; }


        bool clicking;
        /// <summary>
        /// Handles the <see cref="E:Click" /> event.
        /// </summary>
        /// <param name="args">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public async Task OnClick(MouseEventArgs args)
        {
            if (clicking)
            {
                return;
            }

            try
            {
                clicking = true;

                await Click.InvokeAsync(args);
            }
            finally
            {
                clicking = false;
            }
        }

        /// <inheritdoc />
        protected override string GetComponentCssClass()
        {
            return $"html-editor-button html-editor-button-{getButtonSize()} btn-{Enum.GetName(typeof(ButtonStyle), ButtonStyle).ToLowerInvariant()}{(IsDisabled ? " html-editor-state-disabled" : "")}{(string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(Icon) ? " html-editor-button-icon-only" : "")}";
        }
    }
}
