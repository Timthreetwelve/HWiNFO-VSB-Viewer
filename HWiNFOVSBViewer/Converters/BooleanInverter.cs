﻿// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Converters;

/// <summary>
/// Inverts a boolean value. True becomes False. False becomes True.
/// </summary>
/// <seealso cref="System.Windows.Data.IValueConverter" />
public class BooleanInverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return !(bool)value!;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return !(bool)value!;
    }
}
