using System;
using System.Linq;

using Xunit;

namespace Tedd.LinqUtils.Tests;

public class LinqListManipulationTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void MinusTest(int size)
    {
        var rnd = new Random();
        var objs = new string[size];
        var delObjs = new string[size];
        for (int i = 0; i < size; i++)
            objs[i] = i.ToString();
        for (int i = 0; i < size; i++)
            if (rnd.NextBoolean(0.3))
                delObjs[i] = i.ToString();
            else
                delObjs[i] = (-i).ToString();

        var result = objs.Minus(delObjs).ToArray();

        Assert.Equal(size - delObjs.Where(w => int.Parse(w) >= 0).Count(), result.Length);
        var delObjsHS = delObjs.ToHashSet();
        foreach (var obj in result)
            Assert.True(!delObjsHS.Contains(obj));
    }

    [Fact]
    public void MinusComparerTest()
    {
        var size = 100;
        var rnd = new Random();
        var objs = new ComparableObjectInt[size];
        var otherObjs = new ComparableObjectInt[size];
        for (int i = 0; i < size; i++)
            objs[i] = new(i);
        for (int i = 0; i < size; i++)
            if (rnd.NextBoolean(0.3))
                otherObjs[i] = new(i);
            else
                otherObjs[i] = new(-i);

        var result = objs.Minus(otherObjs, new ComparableObjectInt(0)).ToArray();

        otherObjs.ToHashSet().Except(result);

    }

}
