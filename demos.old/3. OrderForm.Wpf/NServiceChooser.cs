﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

using NReact;

namespace NReact.Demos
{
  public partial class NServiceChooser : NClass
  {
    #region must be autogenerated from CSX

    public static class Properties
    {
      public static readonly NProperty Total = new NProperty(nameof(Total));
      public static readonly NProperty Items = new NProperty(nameof(Items));
    }

    protected double Total { get { return GetState(Properties.Total, 0.0); } set { SetState(Properties.Total, value); } }
    public IEnumerable Items { get { return Get(Properties.Items, default(IEnumerable)); } set { Set(Properties.Items, value); } }

    public override NElement Render()
    {
      var services = from dynamic s in Items select new NService { Name = s.Name, Price = s.Price, AddTotal = AddTotal };

      return new NXaml<StackPanel>().
                   HorizontalAlignment(HorizontalAlignment.Center).
                   VerticalAlignment(VerticalAlignment.Center).
                   Children(new NXaml<TextBlock>().
                                  FontSize(18).
                                  Text("Out services"),
                            
                            new NXaml<StackPanel>().
                                  Children(services),

                            new NXaml<TextBlock>().
                                  Text("Total $" + Total).
                                  HorizontalAlignment(HorizontalAlignment.Center));
    }

    #endregion

    void AddTotal(double price)
    {
      Total += price;
    }
  }

  public class NService : NClass
  {
    #region must be autogenerated from CSX

    public static class Properties
    {
      public readonly static NProperty Name = new NProperty(nameof(Name));
      public readonly static NProperty Active = new NProperty(nameof(Active));
      public readonly static NProperty Price = new NProperty(nameof(Price));
      public readonly static NProperty AddTotal = new NProperty(nameof(AddTotal));
    }

    protected bool Active { get { return GetState(Properties.Active, false); } set { SetState(Properties.Active, value); } }
    public string Name { get { return Get(Properties.Name, ""); } set { Set(Properties.Name, value); } }
    public double Price { get { return Get(Properties.Price, 0.0); } set { Set(Properties.Price, value); } }
    public Action<double> AddTotal { get { return Get(Properties.AddTotal, default(Action<double>)); } set { Set(Properties.AddTotal, value); } }

    public override NElement Render()
    {
      return new NXaml<Button>().Style(Active ? "Active" : null).Padding(8, 4).Click(Click).Content(Name + " $" + Price);
    }

    #endregion

    void Click()
    {
      var active = Active = !Active;

      AddTotal(active ? Price : -Price);
    }
  }
}