using System;
using System.Collections.Generic;
using System.Text;

namespace Tedd;
public struct DeltaResult<T>
{
    public readonly List<T> OnlyInFirst;
    public readonly List<T> OnlyInSecond;
    public readonly List<T> InBoth;

    public DeltaResult(List<T> onlyInFirst, List<T> onlyInSecond, List<T> inBoth)
    {
        OnlyInFirst = onlyInFirst;
        OnlyInSecond = onlyInSecond;
        InBoth = inBoth;
    }
}
