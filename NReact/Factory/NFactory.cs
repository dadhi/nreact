﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NReact
{
  public static partial class NFactory
  {
    public static NElement CreateClass<T>(object key = null) where T : NClass, new()
    {
      var result = Ctor<T>.New();

      if (key != null)
        return result.Key(key);

      return result;
    }

    public static NElement CreateElement<T>(object key = null) where T : new()
    {
      var result = default(NElement);

      if (typeof(T).IsSubclassOf(typeof(NClass)))
        result = Ctor<T>.New() as NElement;
      else
        result = new NXaml<T>();

      if (key != null)
        return result.Key(key);

      return result;
    }

    public static NElement CreateElement(Type type, object key = null)
    {
      var result = default(NElement);

      if (type.IsSubclassOf(typeof(NClass)))
        result = Activator.CreateInstance(type) as NElement;
      else
        result = new NXaml(type);

      if (key != null)
        return result.Key(key);

      return result;
    }


    public static NElement Key(this NElement self, object key)
    {
      self._key = key;
      return self;
    }

    public static NElement Ref(this NElement self, Action<object> refCallback)
    {
      self._refCallback = refCallback;
      return self;
    }

    public static NElement Ref<T>(this NElement self, Action<T> refCallback)
    {
      self._refCallback = i => refCallback((T)i);
      return self;
    }

    public static NElement Children(this NElement self, params object[] children)
    {
      return self.Set(Properties.Children, EnsureKeys(Flatten(children).ToArray()));
    }

    public static NElement Children(this NElement self, params NElement[] children)
    {
      return self.Set(Properties.Children, EnsureKeys(children));
    }

    public static NElement Child(this NElement self, NElement child)
    {
      return self.Set(Properties.Child, child);
    }

    public static IEnumerable<NElement> Flatten(params object[] content)
    {
      return Flatten((IEnumerable<object>)content);
    }

    public static IEnumerable<NElement> Flatten(IEnumerable<object> content)
    {
      foreach (var i in content)
      {
        if (i == null)
          continue;

        {
          var e = i as NElement;
          if (e != null)
          {
            yield return e;
            continue;
          }
        }

        {
          var enumerable = i as IEnumerable<NElement>;
          if (enumerable != null)
          {
            foreach (var e in enumerable)
              if (e != null)
                yield return e;

            continue;
          }
        }

        {
          var array = i as IEnumerable<object>;
          if (array != null)
          {
            foreach (var e in Flatten(array))
              if (e != null)
                yield return e;

            continue;
          }
        }

        Debug.WriteLine($"Cannot convert {i} to NElement");
      }
    }

    public static NElement[] EnsureKeys(NElement[] children)
    {
      if (Array.IndexOf(children, null) < 0)
        return children;

      return children.Where(i => i != null).ToArray();
      //for (var i = 0; i < children.Length; i++)
      //{
      //  var c = children[i];
      //  var k = c.Key;

      //  if (k != null)
      //    c.Id = k;
      //  else
      //    c.Id = -i;
      //}

      //return children;
    }

    /// <summary>
    /// Autogenerated container for all assignable properties
    /// </summary>
    public static readonly NProperties Properties = new NProperties();

    /// <summary>
    /// List of NClass properties that are transparently forwarded on rendered element
    /// </summary>
    public static readonly HashSet<NProperty> Ambients = new HashSet<NProperty>
    {
      Properties.Margin,
      Properties.Width, Properties.Height,
      Properties.MinWidth, Properties.MinHeight,
      Properties.MaxWidth, Properties.MaxHeight,
      Properties.HorizontalAlignment, Properties.VerticalAlignment,
      Properties.GridColumn, Properties.GridColumnSpan,
      Properties.GridRow, Properties.GridRowSpan,
      Properties.CanvasLeft, Properties.CanvasTop, Properties.CanvasZIndex,
      Properties.ToolTipServicePlacement, Properties.ToolTipServiceToolTip
    };
  }
}
