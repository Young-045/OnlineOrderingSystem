
using OnlineOrderingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NL.MidEnd.MenuView.WPF4.Controls
{
    public class MenuSampleItem : RadioButton
    {
        public Geometry Geometry
        {
            get { return (Geometry)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public Visibility TipVisibility
        {
            get { return (Visibility)GetValue(TipVisibilityProperty); }
            set { SetValue(TipVisibilityProperty, value); }
        }
        public string Tip
        {
            get { return (string)GetValue(TipProperty); }
            set { SetValue(TipProperty, value); }
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public NaviPageEnum NaviPage
        {
            get { return (NaviPageEnum)GetValue(NaviPageProperty); }
            set { SetValue(NaviPageProperty, value); }
        }

        public static readonly DependencyProperty NaviPageProperty =
            DependencyProperty.Register(nameof(NaviPage), typeof(NaviPageEnum), typeof(MenuSampleItem));


        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(MenuSampleItem));
        public static readonly DependencyProperty TipProperty =
            DependencyProperty.Register(nameof(Tip), typeof(string), typeof(MenuSampleItem));
        public static readonly DependencyProperty TipVisibilityProperty =
            DependencyProperty.Register(nameof(TipVisibility), typeof(Visibility), typeof(MenuSampleItem));
        public static readonly DependencyProperty GeometryProperty =
            DependencyProperty.Register(nameof(Geometry), typeof(Geometry), typeof(MenuSampleItem));
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(MenuSampleItem));

        
    }
}
