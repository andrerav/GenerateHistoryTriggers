﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace GenerateHistoryTriggers
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class Template : TemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n");
            
            #line 7 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
 foreach (var schema in Tables.Select(t => t.HistorySchemaName).Distinct())
{ 
            
            #line default
            #line hidden
            this.Write("CREATE SCHEMA IF NOT EXISTS ");
            
            #line 9 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(schema));
            
            #line default
            #line hidden
            this.Write(";\r\n");
            
            #line 10 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"

}

            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 14 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
 foreach (var table in Tables)
 { 
            
            #line default
            #line hidden
            this.Write("\r\nCREATE TABLE IF NOT EXISTS ");
            
            #line 17 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.HistorySchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 17 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_history\r\n(\r\n    ");
            
            #line 19 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(",\r\n    ", table.PrimaryKeys.Select(pk => pk.PrimaryKeyName + " " + pk.SqlType + " NOT NULL"))));
            
            #line default
            #line hidden
            this.Write(",\r\n    row_data jsonb NOT NULL,\r\n    md5_hash varchar(32) NOT NULL,\r\n    revision" +
                    " integer NOT NULL DEFAULT 1,\r\n    active_from timestamp with time zone NOT NULL " +
                    "DEFAULT now(),\r\n    active_to timestamp with time zone,\r\n    CONSTRAINT ");
            
            #line 25 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_history_pkey PRIMARY KEY (\r\n        ");
            
            #line 26 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
 
        var pkNames = table.PrimaryKeys.Select(pk => pk.PrimaryKeyName).ToList();
        pkNames.Add("revision");
        
            
            #line default
            #line hidden
            this.Write("        ");
            
            #line 30 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", pkNames)));
            
            #line default
            #line hidden
            this.Write("\r\n    )\r\n) TABLESPACE pg_default;\r\n\r\n-- Trigger function for ");
            
            #line 34 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.HistorySchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 34 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_history\r\nCREATE OR REPLACE FUNCTION fn_");
            
            #line 35 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_update_history() RETURNS trigger AS $fn_");
            
            #line 35 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_update_history$\r\n\tDECLARE\r\n\t\tcurrentRevision integer := (select coalesce(max(rev" +
                    "ision) + 1, 1) from ");
            
            #line 37 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.HistorySchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 37 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_history where ");
            
            #line 37 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(" AND ", table.PrimaryKeys.Select(pk => pk.PrimaryKeyName + " = OLD." + pk.PrimaryKeyName))));
            
            #line default
            #line hidden
            this.Write(");\r\n\t\tpreviousRevision integer := (select max(revision) from ");
            
            #line 38 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.HistorySchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 38 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_history where ");
            
            #line 38 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(" AND ", table.PrimaryKeys.Select(pk => pk.PrimaryKeyName + " = OLD." + pk.PrimaryKeyName))));
            
            #line default
            #line hidden
            this.Write(");\r\n\t\tpreviousValidTo timestamp with time zone := (select max(active_to) from ");
            
            #line 39 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.HistorySchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 39 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_history where ");
            
            #line 39 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(" AND ", table.PrimaryKeys.Select(pk => pk.PrimaryKeyName + " = OLD." + pk.PrimaryKeyName))));
            
            #line default
            #line hidden
            this.Write(@");
		newHash varchar(32) := (select md5(to_jsonb(NEW.*)::text));
		currentHash varchar(32) := (select md5(to_jsonb(OLD.*)::text));
		rightnow timestamp with time zone := NOW();
	BEGIN		
		-- Set active_from to the created date if this is the first row
		IF previousRevision IS NULL THEN
			previousValidTo = COALESCE(");
            
            #line 46 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.PreviousValidToColumnName));
            
            #line default
            #line hidden
            this.Write(", NOW());\r\n\t\tEND IF;\r\n\t\t\r\n\t\tIF newHash IS NULL OR newHash <> currentHash THEN\r\n\t\t" +
                    "\tINSERT INTO ");
            
            #line 50 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.HistorySchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 50 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_history VALUES(");
            
            #line 50 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(", ", table.PrimaryKeys.Select(pk => "OLD." + pk.PrimaryKeyName))));
            
            #line default
            #line hidden
            this.Write(", to_jsonb(OLD.*), currentHash, currentRevision, previousValidTo, rightnow);\r\n\t\tE" +
                    "ND IF;\r\n\t\tRETURN NEW;\r\n    END;\r\n$fn_");
            
            #line 54 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_update_history$ LANGUAGE plpgsql;\r\n\r\n-- Trigger for ");
            
            #line 56 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.HistorySchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 56 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_history\r\nDROP TRIGGER IF EXISTS trg_");
            
            #line 57 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_on_update ON ");
            
            #line 57 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.SchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 57 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write(";\r\nCREATE TRIGGER trg_");
            
            #line 58 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_on_update\r\n    AFTER UPDATE OR DELETE ON ");
            
            #line 59 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.SchemaName));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 59 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("\r\n    FOR EACH ROW\r\n        EXECUTE FUNCTION fn_");
            
            #line 61 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.TableName));
            
            #line default
            #line hidden
            this.Write("_update_history();\r\n");
            
            #line 62 "I:\Projects\GenerateHistoryTriggers\src\GenerateHistoryTriggers\Template.tt"

}

            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class TemplateBase
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
        public System.Text.StringBuilder GenerationEnvironment
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
