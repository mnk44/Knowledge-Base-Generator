﻿#pragma checksum "..\..\..\UserControls\GeneralDataWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ACD6F781CC8457146DED6735CE8284DAD703D83F04ED467762CBB32D57DFAA43"
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
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\UserControls\GeneralDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\UserControls\GeneralDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_process_name;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\UserControls\GeneralDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_data_name;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\UserControls\GeneralDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_data_name;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\UserControls\GeneralDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_acept;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\UserControls\GeneralDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_cancel;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\UserControls\GeneralDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\UserControls\GeneralDataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordBox;
        
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
            System.Uri resourceLocater = new System.Uri("/TesisAnaReglas;component/usercontrols/generaldatawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\GeneralDataWindow.xaml"
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
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.textBox_process_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.label_data_name = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.textBox_data_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.button_acept = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\UserControls\GeneralDataWindow.xaml"
            this.button_acept.Click += new System.Windows.RoutedEventHandler(this.button_add);
            
            #line default
            #line hidden
            return;
            case 6:
            this.button_cancel = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\UserControls\GeneralDataWindow.xaml"
            this.button_cancel.Click += new System.Windows.RoutedEventHandler(this.button_cancel_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.passwordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
