﻿using System;
using System.Linq;

#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Controls;
#endif

namespace NReact.Demos
{
  using static NFactory;

  class TodoList : NClass
  {
    #region must be autogenerated from CSX

    public string[] Items { get { return Get(Properties.Items, default(string[])); } set { Set(Properties.Items, value); } }

    public override NElement Render()
    {
      return new NXaml<StackPanel>().
                    Children(Items.Select((i, idx) => new NXaml<TextBlock>(idx).Text("*" + i)));
    }

    #endregion

    public TodoList()
    {
      Items = new string[0];
    }
  }

  class TodoApp : NClass
  {
    #region must be autogenerated from CSX

    /* In CSX this line should look like this:
       
    protected string Text { get; set; }

    */
    protected string Text { get { return GetState(Properties.Text, ""); } set { SetState(Properties.Text, value); } }

    /* In CSX this line should look like this:

    protected string[] Items { get; set; }

    */
    protected string[] Items { get { return GetState(Properties.Items, default(string[])); } set { SetState(Properties.Items, value); } }

    /* In CSX this code should look like this:

    public override NElement Render()
    {
      return
        <StackPanel HorizontalAlignment="Center">
           <TextBlock Text="TODO" FontSize="24" HorizontalAlignment="Center">
              <TodoList Items={Items} />
              <StackPanel Orientation="Horizontal">
                 <TextBox Text={Text} TextChanged={OnChange} Width="200" />
                 <Button Content={"Add #" + (Items.Length + 1)} Click={OnAdd}" />
              </StackPanel>
           </TextBlock>
        </StackPanel>
    }

    */
    public override NElement Render()
    {
      return
        new NXaml<StackPanel>().
              HorizontalAlignment(HorizontalAlignment.Center).
              Children(new NXaml<TextBlock>().
                              Text("TODO").
                              FontSize(24).
                              HorizontalAlignment(HorizontalAlignment.Center),

                       new TodoList { Items = this.Items },

                       new NXaml<StackPanel>().
                              Orientation(Orientation.Horizontal).
                              Children(new NXaml<TextBox>().
                                              Text(Text).
                                              TextChanged(OnChange).
                                              Width(200),

                                       new NXaml<Button>().
                                              Content("Add #" + (Items.Length + 1)).
                                              Click(OnAdd)));
    }

    #endregion

    protected override void InitState()
    {
      Items = new string[0];
      Text = "";
    }

    void OnChange(object sender)
    {
      Text = ((TextBox)sender).Text;
    }

    void OnAdd(object sender, EventArgs args)
    {
      Items = Items.Concat(Text);
      Text = "";
    }
  }

  static class Extensions
  {
    public static string[] Concat(this string[] source, string element)
    {
      return source.Concat(new[] { element }).ToArray();
    }
  }
}