﻿#pragma checksum "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80ABD895ECBA5068CFBE94006EAEDA2B044707E4"
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
    /// AddEditGroupsView
    /// </summary>
    public partial class AddEditGroupsView : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 23 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NumberTB;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SportsDisciplineCB;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PerformanceTypeCB;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Category1CB;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Category2CB;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DancesDG;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PairsDG;
        
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
            System.Uri resourceLocater = new System.Uri("/DanceApp;component/view/secondwindow/distributionplaces/addeditgroupsview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
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
            this.NumberTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.SportsDisciplineCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 46 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            this.SportsDisciplineCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SportsDisciplineCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PerformanceTypeCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 54 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            this.PerformanceTypeCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PerformanceTypeCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Category1CB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 57 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            this.Category1CB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Category1CB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Category2CB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 60 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            this.Category2CB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Category2CB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DancesDG = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            this.PairsDG = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            
            #line 167 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 168 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
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
            case 7:
            
            #line 75 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.DancesChB_Checked);
            
            #line default
            #line hidden
            
            #line 75 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.DancesChB_Unchecked);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 101 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.PairsChB_Checked);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\..\..\..\View\SecondWindow\DistributionPlaces\AddEditGroupsView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.PairsChB_Unchecked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
