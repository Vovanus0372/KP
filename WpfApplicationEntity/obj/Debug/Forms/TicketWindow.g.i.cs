﻿#pragma checksum "..\..\..\Forms\TicketWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CFE81B5D7A00C2B9E3324916AB4885A4316DFAE4"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using WpfApplicationEntity.Forms;


namespace WpfApplicationEntity.Forms {
    
    
    /// <summary>
    /// TicketWindow
    /// </summary>
    public partial class TicketWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\Forms\TicketWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBlockAddEditCost;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Forms\TicketWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBlockAddEditAmount;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Forms\TicketWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBlockAddEditStatus;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Forms\TicketWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxAddEditTicket;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Forms\TicketWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAddEditTicket;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplicationEntity;component/forms/ticketwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Forms\TicketWindow.xaml"
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
            
            #line 8 "..\..\..\Forms\TicketWindow.xaml"
            ((WpfApplicationEntity.Forms.TicketWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textBlockAddEditCost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.textBlockAddEditAmount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textBlockAddEditStatus = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ComboBoxAddEditTicket = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.ButtonAddEditTicket = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\Forms\TicketWindow.xaml"
            this.ButtonAddEditTicket.Click += new System.Windows.RoutedEventHandler(this.ButtonAddEditTicket_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
