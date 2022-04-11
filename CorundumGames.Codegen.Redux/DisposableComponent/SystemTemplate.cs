﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CorundumGames.Codegen.Redux.DisposableComponent
{
    using JCMG.EntitasRedux;
    using Genesis.Plugin;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    internal partial class SystemTemplate : SystemTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(" ");
            
            #line 2 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
 /* So Rider highlights the templated parts as C# */ 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 6 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"

    var contextName = _contextName.RemoveContextSuffix();
    var entityName = contextName.AddEntitySuffix();
    var matcherName = contextName.AddMatcherSuffix();
    var systemName = SystemName;

            
            #line default
            #line hidden
            this.Write("\r\npublic sealed class ");
            
            #line 13 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(systemName));
            
            #line default
            #line hidden
            this.Write(" : JCMG.EntitasRedux.IInitializeSystem, JCMG.EntitasRedux.ITearDownSystem\r\n{\r\n    private static readonly JCMG.EntitasRedux.GroupChanged<");
            
            #line 15 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("> _OnEntityRemoved = OnEntityRemoved;\r\n    private static readonly JCMG.EntitasRedux.GroupUpdated<");
            
            #line 16 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("> _OnEntityUpdated = OnEntityUpdated;\r\n    private static readonly JCMG.EntitasRedux.ContextEntityChanged _OnEntityWillBeDestroyed = OnEntityWillBeDestroyed;\r\n    private readonly JCMG.EntitasRedux.IContext<");
            
            #line 18 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("> _context;\r\n    private readonly JCMG.EntitasRedux.IGroup<");
            
            #line 19 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("> _group;\r\n\r\n    public ");
            
            #line 21 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(systemName));
            
            #line default
            #line hidden
            this.Write("(Contexts contexts)\r\n    {\r\n        _context = contexts.");
            
            #line 23 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(contextName.LowercaseFirst()));
            
            #line default
            #line hidden
            this.Write(";\r\n        _group = _context.GetGroup(");
            
            #line 24 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(matcherName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 24 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_componentName.RemoveComponentSuffix()));
            
            #line default
            #line hidden
            this.Write(");\r\n    }\r\n\r\n    public void Initialize()\r\n    {\r\n        _group.OnEntityRemoved += _OnEntityRemoved;\r\n        _group.OnEntityUpdated += _OnEntityUpdated;\r\n        _context.OnEntityWillBeDestroyed += _OnEntityWillBeDestroyed;\r\n    }\r\n\r\n    public void TearDown()\r\n    {\r\n        using var _ = UnityEngine.Pool.ListPool<");
            
            #line 36 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write(">.Get(out var buffer);\r\n        foreach (var e in _group.GetEntities(buffer))\r\n        {\r\n            e.");
            
            #line 39 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_componentName.RemoveComponentSuffix().LowercaseFirst()));
            
            #line default
            #line hidden
            this.Write(".Dispose();\r\n        }\r\n\r\n        _group.OnEntityRemoved -= _OnEntityRemoved;\r\n        _group.OnEntityUpdated -= _OnEntityUpdated;\r\n        _context.OnEntityWillBeDestroyed -= _OnEntityWillBeDestroyed;\r\n    }\r\n\r\n    private static void OnEntityRemoved(\r\n        JCMG.EntitasRedux.IGroup<");
            
            #line 48 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("> group,\r\n        ");
            
            #line 49 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write(" entity,\r\n        int index,\r\n        JCMG.EntitasRedux.IComponent component\r\n    )\r\n    {\r\n        if (component is System.IDisposable disposable)\r\n        {\r\n            disposable.Dispose();\r\n        }\r\n    }\r\n\r\n    private static void OnEntityUpdated(\r\n        JCMG.EntitasRedux.IGroup<");
            
            #line 61 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("> group,\r\n        ");
            
            #line 62 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write(" entity,\r\n        int index,\r\n        JCMG.EntitasRedux.IComponent previousComponent,\r\n        JCMG.EntitasRedux.IComponent newComponent\r\n    )\r\n    {\r\n        if (previousComponent is System.IDisposable disposable)\r\n        {\r\n            disposable.Dispose();\r\n        }\r\n    }\r\n\r\n    private static void OnEntityWillBeDestroyed(JCMG.EntitasRedux.IContext context, JCMG.EntitasRedux.IEntity entity)\r\n    {\r\n        if (entity is ");
            
            #line 76 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write(" e && e.has");
            
            #line 76 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_componentName.RemoveComponentSuffix()));
            
            #line default
            #line hidden
            this.Write(")\r\n        {\r\n            e.");
            
            #line 78 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_componentName.RemoveComponentSuffix().LowercaseFirst()));
            
            #line default
            #line hidden
            this.Write(".Dispose();\r\n        }\r\n    }\r\n}\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 83 "C:\Users\Jesse\Projects\CorundumGames.Codegen.Redux\CorundumGames.Codegen.Redux\DisposableComponent\SystemTemplate.tt"

    private readonly string _componentName;
    private readonly string _contextName;

    public string SystemName => $"DisposeOf{_contextName.RemoveContextSuffix()}{_componentName.RemoveComponentSuffix()}System";

    internal SystemTemplate(string componentName, string contextName)
    {
        _componentName = componentName ?? throw new ArgumentNullException(nameof(componentName));
        _contextName = contextName ?? throw new ArgumentNullException(nameof(contextName));
    }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    internal class SystemTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
