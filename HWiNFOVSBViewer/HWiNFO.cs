// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace HWiNFOVSBViewer
{
    public class HWiNFO : IComparable<HWiNFO>
    {
        public int Index { get; set; }

        public string Sensor { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public string ValueRaw { get; set; }

        public static List<HWiNFO> HWList = new();

        public static string RegistryKey { get; set; }

        public int CompareTo(HWiNFO other)
        {
            // A null value means that this object is greater.
            return other == null ? 1 : Index.CompareTo(other.Index);
        }
    }
}
