#pragma checksum "..\..\..\UserControls\VariablesWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3DCE33533A088DE3E59C6282B42CA7FE578068730362BE9270743D0A1DCC5424"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TesisAnaReglas.UserControls;


namespace TesisAnaReglas.UserControls {
    
    
    /// <summary>
    /// VariablesWindow
    /// </summary>
    public partial class VariablesWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\UserControls\VariablesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTest;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\UserControls\VariablesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_add;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\UserControls\VariablesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_remove;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\UserControls\VariablesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_modify;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\UserControls\VariablesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_acept;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TesisAnaReglas;component/usercontrols/variableswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\VariablesWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dgTest = ((System.Windows.Controls.DataGrid)(target));
            
            #line 24 "..\..\..\UserControls\VariablesWindow.xaml"
            this.dgTest.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.DgTest_OnSelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.button_add = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\UserControls\VariablesWindow.xaml"
            this.button_add.Click += new System.Windows.RoutedEventHandler(this.add_variable);
            
            #line default
            #line hidden
            return;
            case 3:
            this.button_remove = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\UserControls\VariablesWindow.xaml"
            this.button_remove.Click += new System.Windows.RoutedEventHandler(this.remove_variable);
            
            #line default
            #line hidden
            return;
            case 4:
            this.button_modify = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\UserControls\VariablesWindow.xaml"
            this.button_modify.Click += new System.Windows.RoutedEventHandler(this.modify_variable);
            
            #line default
            #line hidden
            return;
            case 5:
            this.button_acept = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\UserControls\VariablesWindow.xaml"
            this.button_acept.Click += new System.Windows.RoutedEventHandler(this.button_acept_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

