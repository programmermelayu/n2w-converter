using System.Runtime.CompilerServices;

namespace API.Tests;

public class ConverterTests
{
    private readonly Converter _converter;


    public ConverterTests()
    {
        _converter = new Converter();
    }

    [Fact]
    public void Convert_OneThousandPlus_ShouldReturnExpectedResult()
    {
        decimal value = 1234.32M;
        string result = _converter.Convert(value);
        Assert.Equal("ONE THOUSAND TWO HUNDRED THIRTY-FOUR DOLLARS AND THIRTY-TWO CENTS", result);
    }

    [Fact]
    public void Convert_OneMillionPlus_ShouldReturnExpectedResult()
    {
        decimal value = 12345678.75M;
        string result = _converter.Convert(value);
        Assert.Equal("TWELVE MILLION THREE HUNDRED FOURTY-FIVE THOUSAND SIX HUNDRED SEVENTY-EIGHT DOLLARS AND SEVENTY-FIVE CENTS", result);
    }

    [Fact]
    public void Convert_NinetyTrillionPlus_ShouldReturnExpectedResult()
    {
        decimal value = 99999999999999.99M;
        string result = _converter.Convert(value);
        Assert.Equal("NINETY-NINE TRILLION NINE HUNDRED NINETY-NINE BILLION NINE HUNDRED" +
         " NINETY-NINE MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE DOLLARS AND NINETY-NINE CENTS", result);
    }

    [Fact]
    public void Convert_NineHundredTrillionPlus_ShouldReturnExpectedResult()
    {
        decimal value = 999999999999999.99M;
        string result = _converter.Convert(value);
        Assert.Equal("NINE HUNDRED NINETY-NINE TRILLION NINE HUNDRED NINETY-NINE BILLION NINE HUNDRED" +
         " NINETY-NINE MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE DOLLARS AND NINETY-NINE CENTS", result);
    }

    [Fact]
    public void Convert_NineHundredTrillionNine_ShouldReturnExpectedResult()
    {
        decimal value = 900000000000009.99M;
        string result = _converter.Convert(value);
        Assert.Equal("NINE HUNDRED TRILLION NINE DOLLARS AND NINETY-NINE CENTS", result);
    }

    [Fact]
    public void Convert_OnePointNinetyNine_ShouldReturnExpectedResult()
    {
        decimal value = 1.99M;
        string result = _converter.Convert(value);
        Assert.Equal("ONE DOLLAR AND NINETY-NINE CENTS", result);
    }

    [Fact] 
    public void Convert_One_ShouldReturnOneDollar()
    {
        decimal value = 1;
        string result = _converter.Convert(value);
        Assert.Equal("ONE DOLLAR", result);
    }

    
    [Fact] 
    public void Convert_OnePointZeroOne_ShouldReturnOneDollarOneCent()
    {
        decimal value = 1.01M;
        string result = _converter.Convert(value);
        Assert.Equal("ONE DOLLAR AND ONE CENT", result);
    }

    [Fact]
    public void Convert_PointNinetyNine_ShouldReturnNinetyNineCents()
    {
        decimal value = 0.99M;
        string result = _converter.Convert(value);
        Assert.Equal("NINETY-NINE CENTS", result);
    }

    [Fact]
    public void Convert_PointZeroOne_ShouldReturnOneCent()
    {
        decimal value = 0.01M;
        string result = _converter.Convert(value);
        Assert.Equal("ONE CENT", result);
    }


    [Fact] 
    public void Convert_PointOne_ShouldReturnTenCents()
    {
        decimal value = 0.1M;
        string result = _converter.Convert(value);
        Assert.Equal("TEN CENTS", result);
    }

    [Fact]
    public void Convert_PointOneFive_ShouldReturnFifteenCents()
    {
        decimal value = 0.15M;
        string result = _converter.Convert(value);
        Assert.Equal("FIFTEEN CENTS", result);
    }

    [Fact] 
    public void Convert_ThreeDecimalPlaces_ShouldReturnInvalidInput()
    {
        //note: allowRounding for more than 2 decimal place is always OFF by default
        decimal value = 1.123M;
        var exception = Assert.Throws<Exception>(() => _converter.Convert(value));
        Assert.Equal("INVALID INPUT", exception.Message);
    }

     [Fact] 
    public void Convert_ThreeDecimalPlacesWithAllowRounding_ShouldReturnExpectedResult()
    {   
        //note: allowRounding for more than 2 decimal place is always OFF by default
        decimal value = 1.123M;
        string result = _converter.Convert(value, allowRounding: true);
        Assert.Equal("ONE DOLLAR AND TWELVE CENTS", result);
    }

    [Fact] 
    public void Convert_NegativeValue_ShouldReturnInvalidInput()
    {
        decimal value = -1M;
        var exception = Assert.Throws<Exception>(() => _converter.Convert(value));
        Assert.Equal("INVALID INPUT", exception.Message);
    }

}