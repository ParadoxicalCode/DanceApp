﻿#pragma checksum "..\..\..\..\View\GroupsView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F7001B04035FF87D3C0AD43CC8F3302E1B9B294D"
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
        
        
        #line 33 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RoundCB;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FreePairsCountText;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RoundStatusText;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock GroupStatusText;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GroupsDG;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DanceCB;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PerformanceCB;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PerformanceStatusText;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DanceStatusText;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\..\View\GroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PairsDG;
        
        #line default
        #line hidden
        
        
        #line 212 "..\..\..\..\View\GroupsView.xaml"
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
            
            #line 25 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Add_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RoundCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 34 "..\..\..\..\View\GroupsView.xaml"
            this.RoundCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RoundCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 36 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NextRound_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.FreePairsCountText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.RoundStatusText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.GroupStatusText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.GroupsDG = ((System.Windows.Controls.DataGrid)(target));
            
            #line 61 "..\..\..\..\View\GroupsView.xaml"
            this.GroupsDG.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GroupsDG_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.DanceCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 109 "..\..\..\..\View\GroupsView.xaml"
            this.DanceCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DanceCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.PerformanceCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 113 "..\..\..\..\View\GroupsView.xaml"
            this.PerformanceCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PerformanceCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 115 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectPerformance_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.PerformanceStatusText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.DanceStatusText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 15:
            this.PairsDG = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 17:
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
            case 8:
            
            #line 72 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Edit_Click);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 81 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            break;
            case 16:
            
            #line 153 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.PairsChB_Checked);
            
            #line default
            #line hidden
            
            #line 153 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.PairsChB_Unchecked);
            
            #line default
            #line hidden
            break;
            case 18:
            
            #line 223 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.JudgesChB_Checked);
            
            #line default
            #line hidden
            
            #line 223 "..\..\..\..\View\GroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.JudgesChB_Unchecked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
