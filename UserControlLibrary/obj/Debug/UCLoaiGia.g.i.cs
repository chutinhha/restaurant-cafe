﻿#pragma checksum "..\..\UCLoaiGia.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0F69E8F728F7A226A41C0C77B77714F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ControlLibrary;
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


namespace UserControlLibrary {
    
    
    /// <summary>
    /// UCLoaiGia
    /// </summary>
    public partial class UCLoaiGia : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\UCLoaiGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLoaiGia;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\UCLoaiGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDienGiai;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\UCLoaiGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lbStatus;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\UCLoaiGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ControlLibrary.POSButtonIconHorizontal btnMoi;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\UCLoaiGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ControlLibrary.POSButtonIconHorizontal btnThem;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\UCLoaiGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ControlLibrary.POSButtonIconHorizontal btnXoa;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\UCLoaiGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvData;
        
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
            System.Uri resourceLocater = new System.Uri("/UserControlLibrary;component/ucloaigia.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UCLoaiGia.xaml"
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
            
            #line 7 "..\..\UCLoaiGia.xaml"
            ((UserControlLibrary.UCLoaiGia)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtLoaiGia = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtDienGiai = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.lbStatus = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.btnMoi = ((ControlLibrary.POSButtonIconHorizontal)(target));
            
            #line 29 "..\..\UCLoaiGia.xaml"
            this.btnMoi.Click += new System.Windows.RoutedEventHandler(this.btnTaoMoi_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnThem = ((ControlLibrary.POSButtonIconHorizontal)(target));
            
            #line 30 "..\..\UCLoaiGia.xaml"
            this.btnThem.Click += new System.Windows.RoutedEventHandler(this.btnThemMoi_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnXoa = ((ControlLibrary.POSButtonIconHorizontal)(target));
            
            #line 31 "..\..\UCLoaiGia.xaml"
            this.btnXoa.Click += new System.Windows.RoutedEventHandler(this.btnXoa_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lvData = ((System.Windows.Controls.ListView)(target));
            
            #line 34 "..\..\UCLoaiGia.xaml"
            this.lvData.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lvData_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

