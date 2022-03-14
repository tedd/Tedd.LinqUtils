using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tedd.LinqUtils.Tests
{
    internal class ComparableObjectStr : IEqualityComparer<ComparableObjectStr>
    {
        public ComparableObjectStr() { }
        public ComparableObjectStr(string value) => Value = value;
        public string Value;
        public bool Equals(ComparableObjectStr? x, ComparableObjectStr? y) => x.Value.Equals(y.Value);

        public int GetHashCode([DisallowNull] ComparableObjectStr obj) => obj.Value.GetHashCode();
    }
    internal class ComparableObjectInt : IEqualityComparer<ComparableObjectInt>
    {
        public ComparableObjectInt() { }
        public ComparableObjectInt(int value) => Value = value;
        public int Value;
        public bool Equals(ComparableObjectInt? x, ComparableObjectInt? y) => x.Value.Equals(y.Value);

        public int GetHashCode([DisallowNull] ComparableObjectInt obj) => obj.Value.GetHashCode();
    }
}
