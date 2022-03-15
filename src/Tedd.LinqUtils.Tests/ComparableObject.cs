using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tedd.LinqUtils.Tests
{
    [DebuggerDisplay("{Value}")]
    internal class ComparableObjectStr 
    {
        public ComparableObjectStr(string value) => Value = value;
        public string Value;
    }
    internal class ComparableObjectStrComparer : IEqualityComparer<ComparableObjectStr>
    {
        public ComparableObjectStrComparer() { }
        public string Value;
        public bool Equals(ComparableObjectStr? x, ComparableObjectStr? y) => x.Value.Equals(y.Value);

        public int GetHashCode([DisallowNull] ComparableObjectStr obj) => obj.Value.GetHashCode();
    }
    [DebuggerDisplay("{Value}")]
    internal class ComparableObjectInt 
    {
        public ComparableObjectInt(int value) => Value = value;
        public int Value;
    }
    internal class ComparableObjectIntComparer : IEqualityComparer<ComparableObjectInt>
    {
        public ComparableObjectIntComparer() { }
        public int Value;
        public bool Equals(ComparableObjectInt? x, ComparableObjectInt? y) => x.Value.Equals(y.Value);

        public int GetHashCode([DisallowNull] ComparableObjectInt obj) => obj.Value.GetHashCode();
    }
    internal class ComparableObject : IEqualityComparer<ComparableObject>
    {
        public ComparableObject() { }
        public ComparableObject(string value) => Value = value;
        public string Value;
        public bool Equals(ComparableObject? x, ComparableObject? y) => x.Equals(y);

        public int GetHashCode([DisallowNull] ComparableObject obj) => obj.GetHashCode();
    }
}
