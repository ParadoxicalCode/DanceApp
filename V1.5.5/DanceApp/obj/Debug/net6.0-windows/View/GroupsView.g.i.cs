﻿#pragma checksum "..\..\..\..\View\GroupsView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27CAC56E2484D4C470BD5ADE17C3FEDEF30A4953"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DanceApp.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace DanceApp.View {
    
    
    /// <summary>
    /// GroupsView
    /// </summary>
    public partial class GroupsView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 29 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TourCB;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GroupsDG;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DanceCB;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PerformanceCB;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PairsDG;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid JudgesDG;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DanceApp;component/view/groupsview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\GroupsView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 21 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TourCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 30 "..\..\..\..\View\GroupsView.xaml"
            this.TourCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TourCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.GroupsDG = ((System.Windows.Controls.DataGrid)(target));
            
            #line 37 "..\..\..\..\View\GroupsView.xaml"
            this.GroupsDG.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GroupsDG_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DanceCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 95 "..\..\..\..\View\GroupsView.xaml"
            this.DanceCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DanceCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PerformanceCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 99 "..\..\..\..\View\GroupsView.xaml"
            this.PerformanceCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PerformanceCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 101 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectPerformance_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.PairsDG = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.JudgesDG = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 4:
            
            #line 48 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Edit_Click);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 57 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 128 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.PairsChB_Checked);
            
            #line default
            #line hidden
            
            #line 128 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.PairsChB_Unchecked);
            
            #line default
            #line hidden
            break;
            case 12:
            
            #line 198 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.JudgesChB_Checked);
            
            #line default
            #line hidden
            
            #line 198 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.JudgesChB_Unchecked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

