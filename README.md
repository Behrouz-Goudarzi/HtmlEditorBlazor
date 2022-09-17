
HtmlEditor.Blazor Getting Started Instructions
1. Install

HtmlEditor Blazor Components are distributed as the HtmlEditor.Blazor nuget package.

You can add them to your project in one of the following ways

   ##Install the package from command line by running dotnet add package HtmlEditor.Blazor
    Add the project from the Visual Nuget Package Manager.

2. Import the namespace

Open the _Imports.razor file of your Blazor application and add these two lines @using HtmlEditor and @using HtmlEditor.Blazor.
3. Include a theme

Alternatively you can include <link rel="stylesheet" href="_content/HtmlEditor.Blazor/css/default.css"> which embeds Bootstrap.
4. Include HtmlEditor.Blazor.js

Open the _Host.cshtml file (server-side Blazor) or wwwroot/index.html (client-side WebAssembly Blazor) and include this snippet <script src="_content/HtmlEditor.Blazor/HtmlEditor.Blazor.js"></script>
5. Use a component

Use any HtmlEditor Blazor component by typing its tag name in a Blazor page e.g. <HtmlEditorButton Text="Hi"></HtmlEditorButton>
Setting a property

<code>
<HtmlEditorComponent UploadUrl="upload/image" Change="@OnChange" Paste="@OnPaste" Execute="@OnExecute" ></HtmlEditorComponent>


@code {

    void OnPaste(HtmlEditorPasteEventArgs args)
    {
     ////
    }

    void OnChange(string html)
    {
      ////
    }

    void OnExecute(HtmlEditorExecuteEventArgs args)
    {
       ////
    }
}
</code>
