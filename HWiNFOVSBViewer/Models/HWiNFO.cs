// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Models
{
    public class HWiNFO
    {
        public int Index { get; set; }

        public string? Sensor { get; set; }

        public string? Label { get; set; }

        public string? Value { get; set; }

        public string? ValueRaw { get; set; }

        public static List<HWiNFO> HWList { get; set; } = [];

        public static string? RegistryKey { get; set; }
    }
}
