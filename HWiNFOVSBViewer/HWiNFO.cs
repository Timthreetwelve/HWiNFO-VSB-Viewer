// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer
{
#pragma warning disable S101 // Types should be named in PascalCase
    public class HWiNFO
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public int Index { get; set; }

        public string Sensor { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public string ValueRaw { get; set; }

        public static List<HWiNFO> HWList { get; set; } = [];

        public static string RegistryKey { get; set; }
    }
}
