// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Models;

public enum NavPage
{
    Viewer = 0,
    Settings = 1,
    About = 2,
    Exit = 3
}

/// <summary>
/// Theme type, Light, Dark, or System
/// </summary>
[TypeConverter(typeof(EnumDescriptionTypeConverter))]
public enum ThemeType
{
    [LocalizedDescription("SettingsEnum_Theme_Light")]
    Light = 0,
    [LocalizedDescription("SettingsEnum_Theme_Dark")]
    Dark = 1,
    [LocalizedDescription("SettingsEnum_Theme_Darker")]
    Darker = 2,
    [LocalizedDescription("SettingsEnum_Theme_System")]
    System = 3
}

/// <summary>
/// Size of the UI, Smallest, Smaller, Small, Default, Large, Larger, or Largest
/// </summary>
[TypeConverter(typeof(EnumDescriptionTypeConverter))]
public enum MySize
{
    [LocalizedDescription("SettingsEnum_Size_Smallest")]
    Smallest = 0,
    [LocalizedDescription("SettingsEnum_Size_Smaller")]
    Smaller = 1,
    [LocalizedDescription("SettingsEnum_Size_Small")]
    Small = 2,
    [LocalizedDescription("SettingsEnum_Size_Default")]
    Default = 3,
    [LocalizedDescription("SettingsEnum_Size_Large")]
    Large = 4,
    [LocalizedDescription("SettingsEnum_Size_Larger")]
    Larger = 5,
    [LocalizedDescription("SettingsEnum_Size_Largest")]
    Largest = 6
}

/// <summary>
/// One of the 19 predefined Material Design in XAML colors
/// </summary>
[TypeConverter(typeof(EnumDescriptionTypeConverter))]
public enum AccentColor
{
    [LocalizedDescription("SettingsEnum_AccentColor_Red")]
    Red = 0,
    [LocalizedDescription("SettingsEnum_AccentColor_Pink")]
    Pink = 1,
    [LocalizedDescription("SettingsEnum_AccentColor_Purple")]
    Purple = 2,
    [LocalizedDescription("SettingsEnum_AccentColor_DeepPurple")]
    DeepPurple = 3,
    [LocalizedDescription("SettingsEnum_AccentColor_Indigo")]
    Indigo = 4,
    [LocalizedDescription("SettingsEnum_AccentColor_Blue")]
    Blue = 5,
    [LocalizedDescription("SettingsEnum_AccentColor_LightBlue")]
    LightBlue = 6,
    [LocalizedDescription("SettingsEnum_AccentColor_Cyan")]
    Cyan = 7,
    [LocalizedDescription("SettingsEnum_AccentColor_Teal")]
    Teal = 8,
    [LocalizedDescription("SettingsEnum_AccentColor_Green")]
    Green = 9,
    [LocalizedDescription("SettingsEnum_AccentColor_LightGreen")]
    LightGreen = 10,
    [LocalizedDescription("SettingsEnum_AccentColor_Lime")]
    Lime = 11,
    [LocalizedDescription("SettingsEnum_AccentColor_Yellow")]
    Yellow = 12,
    [LocalizedDescription("SettingsEnum_AccentColor_Amber")]
    Amber = 13,
    [LocalizedDescription("SettingsEnum_AccentColor_Orange")]
    Orange = 14,
    [LocalizedDescription("SettingsEnum_AccentColor_DeepOrange")]
    DeepOrange = 15,
    [LocalizedDescription("SettingsEnum_AccentColor_Brown")]
    Brown = 16,
    [LocalizedDescription("SettingsEnum_AccentColor_Gray")]
    Gray = 17,
    [LocalizedDescription("SettingsEnum_AccentColor_BlueGray")]
    BlueGray = 18,
    [LocalizedDescription("SettingsEnum_AccentColor_Black")]
    Black = 19,
    [LocalizedDescription("SettingsEnum_AccentColor_White")]
    White = 20,
}

[TypeConverter(typeof(EnumDescriptionTypeConverter))]
public enum Weight
{
    [LocalizedDescription("SettingsEnum_FontWeight_Thin")]
    Thin = 0,
    [LocalizedDescription("SettingsEnum_FontWeight_Regular")]
    Regular = 1,
    [LocalizedDescription("SettingsEnum_FontWeight_SemiBold")]
    SemiBold = 2,
    [LocalizedDescription("SettingsEnum_FontWeight_Bold")]
    Bold = 3
}

/// <summary>
/// Space between rows in the DataGrid
/// </summary>
[TypeConverter(typeof(EnumDescriptionTypeConverter))]
public enum Spacing
{
    [LocalizedDescription("SettingsEnum_Spacing_Compact")]
    Compact = 0,
    [LocalizedDescription("SettingsEnum_Spacing_Comfortable")]
    Comfortable = 1,
    [LocalizedDescription("SettingsEnum_Spacing_Wide")]
    Wide = 2
}
