using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

public static class EnumExtensions
{
    /// <summary>
    /// Get the Description from the DescriptionAttribute.
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
                   .GetMember(enumValue.ToString())
                   .First()
                   .GetCustomAttribute<DisplayNameAttribute>()?
                   .DisplayName ?? string.Empty;
    }
}

