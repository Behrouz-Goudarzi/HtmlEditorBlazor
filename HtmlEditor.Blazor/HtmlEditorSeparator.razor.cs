using Microsoft.AspNetCore.Components;

namespace HtmlEditor.Blazor
{
    /// <summary>
    /// A HtmlEditor visual separator.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;HtmlEditor @bind-Value=@html&gt;
    ///  &lt;HtmlEditorLink /&gt;
    ///  &lt;HtmlEditorSeparator /&gt;
    ///  &lt;HtmlEditorJustify /&gt;
    /// &lt;/HtmlEditor&gt;
    /// @code {
    ///   string html = "@lt;strong&gt;Hello&lt;/strong&gt; world!"; 
    /// }
    /// </code>
    /// </example>
    public partial class HtmlEditorSeparator
    {
    }
}
