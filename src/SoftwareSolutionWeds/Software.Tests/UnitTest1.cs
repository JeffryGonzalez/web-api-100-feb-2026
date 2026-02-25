namespace Software.Tests;

public class UnitTest1
{
    [Fact]
    public void TwoPlusTwoEqualsFour()
    {
        var answer = 2 + 2;
        Assert.Equal(4, answer);
    }


    [Theory]
    [InlineData(2,2,4)]
    [InlineData(2,1,3)]
    [InlineData(2,3,5)]
    public void CanAddAnyIntegers(int a, int b, int expected)
    {
        var answer = a + b;
        Assert.Equal(expected, answer);
    }
}
