#pragma checksum "..\..\..\UserControls\VarCauRec2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4DA266B44B2984F68CC3D12CBF82E596"
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
    /// VarCauRec2
    /// </summary>
    public partial class VarCauRec2 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_cause;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_rec;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBox_recomen;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_acep;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_causa;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_add;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_delete;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\UserControls\VarCauRec2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button1;
        
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
            System.Uri resourceLocater = new System.Uri("/TesisAnaReglas;component/usercontrols/varcaurec2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\VarCauRec2.xaml"
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
            this.label_cause = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.label_rec = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.comboBox_recomen = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.button_acep = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\UserControls\VarCauRec2.xaml"
            this.button_acep.Click += new System.Windows.RoutedEventHandler(this.button_acep_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.label_causa = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.button_add = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\UserControls\VarCauRec2.xaml"
            this.button_add.Click += new System.Windows.RoutedEventHandler(this.button_add_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.button_delete = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\UserControls\VarCauRec2.xaml"
            this.button_delete.Click += new System.Windows.RoutedEventHandler(this.button_delete_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\UserControls\VarCauRec2.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.button1 = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

