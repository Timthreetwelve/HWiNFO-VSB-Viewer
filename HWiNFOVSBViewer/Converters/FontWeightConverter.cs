// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Converters;

internal sealed class FontWeightConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Weight weight)
        {
            switch (weight)
            {
                case Weight.Thin:
                    return FontWeights.Thin;
                case Weight.Regular:
                    return FontWeights.Regular;
                case Weight.SemiBold:
                    return FontWeights.SemiBold;
                case Weight.Bold:
                    return FontWeights.Bold;
            }
        }
        return FontWeights.Regular;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
