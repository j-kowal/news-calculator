using NewsCalculatorApp.Services;
using NewsCalculatorApp.DTOs;
namespace NewsCalculatorAppTests;

public class NewsServiceTest
{
    private readonly NewsService _newsService = new();

    [Fact]
    public void TestValidIo()
    {
        var result = _newsService.GetNewsScore(new NewsInput(39f, 43, 19));
        Assert.Equal(2, result);
    }

    [Fact]
    public void TestInvalidInput()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _newsService.GetNewsScore(new NewsInput(100f, 43, 19)));
    }
}