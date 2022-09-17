using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using HtmlEditor.Blazor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace HtmlEditor
{

 
    /// <summary>
    /// Supplies information about a <see cref="Menu.Click" /> event that is being raised.
    /// </summary>
    public class MenuItemEventArgs : MouseEventArgs
    {
        /// <summary>
        /// Gets text of the clicked item.
        /// </summary>
        public string Text { get; internal set; }
        /// <summary>
        /// Gets the value of the clicked item.
        /// </summary>
        public object Value { get; internal set; }
        /// <summary>
        /// Gets the path path of the clicked item.
        /// </summary>
        public string Path { get; internal set; }
    }

    /// <summary>
    /// Supplies information about a <see cref="HtmlEditorUpload.Error" /> event that is being raised.
    /// </summary>
    public class UploadErrorEventArgs
    {
        /// <summary>
        /// Gets a message telling what caused the error.
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Supplies information about a <see cref="HtmlEditorUpload.Change" /> event that is being raised.
    /// </summary>
    public class UploadChangeEventArgs
    {
        /// <summary>
        /// Gets a collection of the selected files.
        /// </summary>
        public IEnumerable<FileInfo> Files { get; set; }
    }

    /// <summary>
    /// Supplies information about a <see cref="HtmlEditorUpload.Progress" /> event that is being raised.
    /// </summary>
    public class UploadProgressArgs
    {
        /// <summary>
        /// Gets or sets the number of bytes that have been uploaded.
        /// </summary>
        public long Loaded { get; set; }
        /// <summary>
        /// Gets the total number of bytes that need to be uploaded.
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// Gets the progress as a percentage value (from <c>0</c> to <c>100</c>).
        /// </summary>
        public int Progress { get; set; }
        /// <summary>
        /// Gets a collection of files that are being uploaded.
        /// </summary>
        public IEnumerable<FileInfo> Files { get; set; }
        /// <summary>
        /// Gets or sets a flag indicating whether the underlying XMLHttpRequest should be aborted.
        /// </summary>
        public bool Cancel { get; set; }
    }

    /// <summary>
    /// Supplies information about a <see cref="HtmlEditorUpload.Complete" /> event that is being raised.
    /// </summary>
    public class UploadCompleteEventArgs
    {
        /// <summary>
        /// Gets the JSON response which the server returned after the upload.
        /// </summary>
        public JsonDocument JsonResponse { get; set; }

        /// <summary>
        /// Gets the raw server response.
        /// </summary>
        public string RawResponse { get; set; }

        /// <summary>
        /// Gets a boolean value indicating if the upload was cancelled by the user.
        /// </summary>
        public bool Cancelled { get; set; }
    }

    /// <summary>
    /// Represents a file which the user selects for upload via <see cref="HtmlEditorUpload" />.
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// Gets the name of the selected file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the size (in bytes) of the selected file.
        /// </summary>
        public long Size { get; set; }
    }

    /// <summary>
    /// Represents the preview which <see cref="HtmlEditorUpload" /> displays for selected files.
    /// </summary>
    public class PreviewFileInfo : FileInfo
    {
        /// <summary>
        /// Gets the URL of the previewed file.
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// Specifies the type of a <see cref="HtmlEditorButton" />. Renders as the <c>type</c> HTML attribute.
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// Generic button which does not submit its parent form.
        /// </summary>
        Button,
        /// <summary>
        /// Clicking a submit button submits its parent form.
        /// </summary>
        Submit,
        /// <summary>
        /// Clicking a reset button clears the value of all inputs in its parent form.
        /// </summary>
        Reset
    }

    /// <summary>
    /// Specifies the size of a <see cref="HtmlEditorButton" />.
    /// </summary>
    public enum ButtonSize
    {
        /// <summary>
        /// The default size of a button.
        /// </summary>
        Medium,
        /// <summary>
        /// A button smaller than the default.
        /// </summary>
        Small
    }

    /// <summary>
    /// Specifies the display style of a <see cref="HtmlEditorButton" />. Affects the visual styling of HtmlEditorButton (background and text color).
    /// </summary>
    public enum ButtonStyle
    {
        /// <summary>
        /// A primary button. Clicking it performs the primary action in a form or dialog (e.g. "save").
        /// </summary>
        Primary,
        /// <summary>
        /// A secondary button. Clicking it performs a secondaryprimary action in a form or dialog (e.g. close a dialog or cancel a form).
        /// </summary>
        Secondary,
        /// <summary>
        /// A button with lighter styling.
        /// </summary>
        Light,
        /// <summary>
        /// A button with success styling.
        /// </summary>
        Success,
        /// <summary>
        /// A button which represents a dangerous action e.g. "delete".
        /// </summary>
        Danger,
        /// <summary>
        /// A button with warning styling.
        /// </summary>
        Warning,
        /// <summary>
        /// A button with informative styling.
        /// </summary>
        Info
    }

    /// <summary>
    /// Specifies the display style of a <see cref="HtmlEditorIcon" />. Affects the visual styling of HtmlEditorIcon (Icon (text) color).
    /// </summary>
    public enum IconStyle
    {
        /// <summary>
        /// Primary styling. Similar to primary buttons.
        /// </summary>
        Primary,
        /// <summary>
        /// Secondary styling. Similar to secondary buttons.
        /// </summary>
        Secondary,
        /// <summary>
        /// Light styling. Similar to light buttons.
        /// </summary>
        Light,
        /// <summary>
        /// Success styling.
        /// </summary>
        Success,
        /// <summary>
        /// Danger styling.
        /// </summary>
        Danger,
        /// <summary>
        /// Warning styling.
        /// </summary>
        Warning,
        /// <summary>
        /// Informative styling.
        /// </summary>
        Info
    }


    /// <summary>
    /// Contains the commands which <see cref="HtmlEditor" /> supports.
    /// </summary>
    public static class HtmlEditorCommands
    {
        /// <summary>
        /// Inserts html at cursor location.
        /// </summary>
        public static string InsertHtml = "insertHtml";
        /// <summary>
        /// Centers the selected text.
        /// </summary>
        public static string AlignCenter = "justifyCenter";
        /// <summary>
        /// Aligns the selected text to the left.
        /// </summary>
        public static string AlignLeft = "justifyLeft";
        /// <summary>
        /// Aligns the selected text to the right.
        /// </summary>
        public static string AlignRight = "justifyRight";
        /// <summary>
        /// Sets the background color of the selected text.
        /// </summary>
        public static string Background = "backColor";
        /// <summary>
        /// Bolds the selected text.
        /// </summary>
        public static string Bold = "bold";
        /// <summary>
        /// Sets the text color of the selection.
        /// </summary>
        public static string Color = "foreColor";
        /// <summary>
        /// Sets the font of the selected text.
        /// </summary>
        public static string FontName = "fontName";
        /// <summary>
        /// Sets the font size of the selected text.
        /// </summary>
        public static string FontSize = "fontSize";
        /// <summary>
        /// Formats the selection as paragraph, heading etc.
        /// </summary>
        public static string FormatBlock = "formatBlock";
        /// <summary>
        /// Indents the selection.
        /// </summary>
        public static string Indent = "indent";
        /// <summary>
        /// Makes the selected text italic.
        /// </summary>
        public static string Italic = "italic";
        /// <summary>
        /// Justifies the selected text.
        /// </summary>
        public static string Justify = "justifyFull";
        /// <summary>
        /// Inserts an empty ordered list or makes an ordered list from the selected text.
        /// </summary>
        public static string OrderedList = "insertOrderedList";
        /// <summary>
        /// Outdents the selected text.
        /// </summary>
        public static string Outdent = "outdent";
        /// <summary>
        /// Repeats the last edit operations.
        /// </summary>
        public static string Redo = "redo";
        /// <summary>
        /// Removes visual formatting from the selected text.
        /// </summary>
        public static string RemoveFormat = "removeFormat";
        /// <summary>
        /// Strikes through the selected text.
        /// </summary>
        public static string StrikeThrough = "strikeThrough";
        /// <summary>
        /// Applies subscript styling to the selected text.
        /// </summary>
        public static string Subscript = "subscript";
        /// <summary>
        /// Applies superscript styling to the selected text.
        /// </summary>
        public static string Superscript = "superscript";
        /// <summary>
        /// Underlines the selected text.
        /// </summary>
        public static string Underline = "underline";
        /// <summary>
        /// Undoes the last edit operation.
        /// </summary>
        public static string Undo = "undo";
        /// <summary>
        /// Unlinks a link.
        /// </summary>
        public static string Unlink = "unlink";
        /// <summary>
        /// Inserts an empty unordered list or makes an unordered list from the selected text.
        /// </summary>
        public static string UnorderedList = "insertUnorderedList";
    }

    /// <summary>
    /// Supplies information about a <see cref="HtmlEditorComponent.Paste" /> event that is being raised.
    /// </summary>
    public class HtmlEditorPasteEventArgs
    {
        /// <summary>
        /// Gets or sets the HTML content that is pasted in HtmlEditor. Use the setter to filter unwanted markup from the pasted value.
        /// </summary>
        /// <value>The HTML.</value>
        public string Html { get; set; }
    }

    /// <summary>
    /// Supplies information about a <see cref="HtmlEditorComponent.Execute" /> event that is being raised.
    /// </summary>
    public class HtmlEditorExecuteEventArgs
    {
        /// <summary>
        /// Gets the HtmlEditor instance which raised the event.
        /// </summary>
        public HtmlEditorComponent Editor { get; set; }

        internal HtmlEditorExecuteEventArgs(HtmlEditorComponent editor)
        {
            Editor = editor;
        }

        /// <summary>
        /// Gets the name of the command which HtmlEditor is executing.
        /// </summary>
        public string CommandName { get; set; }
    }


    /// <summary>
    /// Converts values to different types. Used internally.
    /// </summary>
    public static class ConvertType
    {
        /// <summary>
        /// Changes the type of an object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object</returns>
        public static object ChangeType(object value, Type type)
        {
            if (value == null && Nullable.GetUnderlyingType(type) != null)
            {
                return value;
            }

            if ((Nullable.GetUnderlyingType(type) ?? type) == typeof(Guid) && value is string)
            {
                return Guid.Parse((string)value);
            }

            if (Nullable.GetUnderlyingType(type)?.IsEnum == true)
            {
                return Enum.Parse(Nullable.GetUnderlyingType(type), value.ToString());
            }
            
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                Type itemType = type.GetGenericArguments()[0];
                var enumerable = value as IEnumerable<object>;

                if (enumerable != null)
                {
                    return enumerable.AsQueryable().Cast(itemType);
                }

            }

            return value is IConvertible ? Convert.ChangeType(value, Nullable.GetUnderlyingType(type) ?? type) : value;
        }
    }


    /// <summary>
    /// Represents the common see cref="HtmlEditorTemplateForm{TItem}" /> API used by 
    /// its items. Injected as a cascading property in <see cref="IHtmlEditorFormComponent" />.
    /// </summary>
    public interface IHtmlEditorForm
    {
        /// <summary>
        /// Adds the specified component to the form.
        /// </summary>
        /// <param name="component">The component to add to the form.</param>
        void AddComponent(IHtmlEditorFormComponent component);
        /// <summary>
        /// Removes the component from the form.
        /// </summary>
        /// <param name="component">The component to remove from the form.</param>
        void RemoveComponent(IHtmlEditorFormComponent component);
        /// <summary>
        /// Finds a form component by its name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The component whose <see cref="IHtmlEditorFormComponent.Name" /> equals to <paramref name="name" />; <c>null</c> if such a component is not found.</returns>
        IHtmlEditorFormComponent FindComponent(string name);
    }

    /// <summary>
    /// Specifies the interface that form components must implement in order to be supported by see cref="HtmlEditorTemplateForm{TItem}" />.
    /// </summary>
    public interface IHtmlEditorFormComponent
    {
        /// <summary>
        /// Gets a value indicating whether this component is bound.
        /// </summary>
        /// <value><c>true</c> if this component is bound; otherwise, <c>false</c>.</value>
        bool IsBound { get; }
        /// <summary>
        /// Gets a value indicating whether the component has value.
        /// </summary>
        /// <value><c>true</c> if this instance has value; otherwise, <c>false</c>.</value>
        bool HasValue { get; }

        /// <summary>
        /// Gets the value of the component.
        /// </summary>
        /// <returns>the value of the component - for example the text of HtmlEditorTextBox.</returns>
        object GetValue();

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets the field identifier.
        /// </summary>
        /// <value>The field identifier.</value>
        FieldIdentifier FieldIdentifier { get; }
    }


    /// <summary>
    /// Contains extension methods for <see cref="ParameterView" />.
    /// </summary>
    public static class ParameterViewExtensions
    {
        /// <summary>
        /// Checks if a parameter changed.
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns><c>true</c> if the parameter value has changed, <c>false</c> otherwise.</returns>
        public static bool DidParameterChange<T>(this ParameterView parameters, string parameterName, T parameterValue)
        {
            if (parameters.TryGetValue(parameterName, out T value))
            {
                return !EqualityComparer<T>.Default.Equals(value, parameterValue);
            }

            return false;
        }
    }
}