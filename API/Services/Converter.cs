using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;

namespace API;

public class Converter : IConverter
{
    private bool allowRounding = false;
    private static List<KeyValuePair<int, string>> Teens
    {
        get
        {
            return
            [
                new(10, "ten"),
                new(11, "eleven"),
                new(12, "twelve"),
                new(13, "thirteen"),
                new(14, "fourteen"),
                new(15, "fifteen"),
                new(16, "sixteen"),
                new(17, "seventeen"),
                new(18, "eighteen"),
                new(19, "nineteen"),
                new(20, "fifteen")
            ];
        }
    }
    private static List<string> Ones
    {
        get
        {
            return ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"]; ;
        }
    }
    private static List<string> Tens
    {
        get
        {
            return ["zero", "ten", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eightty", "ninety"];
        }
    }
    private static List<string> Illions
    {
        get
        {
            return ["", "thousand", "million", "billion", "trillion", "quadrillion", "quantillion", "quantillion", "sextillion", "octillion"];
        }
    }
    private class DollarDivision
    {
        public string? IllionName { get; set; }
        public string? OneInWord { get; set; }
        public string? TenInWord { get; set; }
        public string? HundredInWord { get; set; }
        public string? DollarInWords { get; set; }
    }
    private class CentDivision
    {
        public string? OneInWord { get; set; }
        public string? TenInWord { get; set; }
    }
    public string Convert(decimal value, bool allowRounding = false)
    {
        this.allowRounding = allowRounding;

        if (!ValidateInput(value))
            throw new Exception("INVALID INPUT");

        try
        {
            var dollarAndCent = value.ToString("F2").Split('.');
            var dollar = dollarAndCent[0];
            var cent = dollarAndCent[1];

            var allPairs = new List<KeyValuePair<int, DollarDivision>>();

            int charLength = 3;
            for (int i = 0; i < Illions.Count; i++, charLength += 3)
            {
                var dollarDiv = GetDollarDivision(dollar, charLength, Illions[i]);
                if (dollarDiv == null) break;
                allPairs.Add(new KeyValuePair<int, DollarDivision>(i, dollarDiv));
            }

            var dollarInWords = GetDollarInWords(allPairs);
            if (!string.IsNullOrEmpty(dollarInWords))
                dollarInWords = $"{dollarInWords} dollar{(dollarInWords == "one" ? "" : "s")}";

            var centDiv = GetCentDivision(cent);
            var dollarAndCentInWords = GetDollarAndCentInWords(centDiv, dollarInWords);

            return dollarAndCentInWords.ToUpper();

        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    private bool ValidateInput(decimal value)
    {
        return !(value < 0 || (!allowRounding && HasMoreThanTwoDecimalPlaces(value)));
    }
    private static bool HasMoreThanTwoDecimalPlaces(decimal value)
    {
        var decimalString = value.ToString().Split('.');
        return decimalString.Length > 1 && decimalString[1].Length > 2;
    }
    private static string GetDollarAndCentInWords(CentDivision centDiv, string dollarInWords)
    {
        if (SkipOnZeroCent(centDiv))
            return dollarInWords.Trim();

        string dollarAndCentInWords = string.IsNullOrEmpty(dollarInWords) ? "" : $"{dollarInWords} and ";
        dollarAndCentInWords += GetCentInWords(centDiv);

        return dollarAndCentInWords.Trim();
    }
    private static string GetCentInWords(CentDivision centDiv)
    {
        string? centString = !string.IsNullOrEmpty(centDiv.TenInWord)
            ? GetTeenOrTensValueInWords(centDiv)
            : centDiv.OneInWord;

        return $"{centString?.Trim()} cent{(centDiv.OneInWord == "one" ? "" : "s")}";
    }
    private static string GetTeenOrTensValueInWords(CentDivision centDiv)
    {
        var teenVal = Teens.FirstOrDefault(x => x.Value == centDiv.TenInWord);
        return teenVal.Equals(default(KeyValuePair<int, string>))
            ? $"{centDiv.TenInWord}-{centDiv.OneInWord}"
            : $"{centDiv.TenInWord} {centDiv.OneInWord}";
    }
    private static string GetDollarInWords(List<KeyValuePair<int, DollarDivision>> allPairs)
    {
        var dollarString = new StringBuilder();

        for (int i = allPairs.Count - 1; i >= 0; i--)
        {
            var storedDiv = allPairs[i].Value;

            if (storedDiv != null)
            {
                if (!string.IsNullOrEmpty(storedDiv.HundredInWord) && storedDiv.HundredInWord != "zero")
                {
                    dollarString.Append($" {storedDiv.HundredInWord} hundred");
                }
                if (!string.IsNullOrEmpty(storedDiv.TenInWord) && storedDiv.TenInWord != "zero")
                {
                    dollarString.Append($" {storedDiv.TenInWord}");
                }
                if (!string.IsNullOrEmpty(storedDiv.OneInWord) && storedDiv.OneInWord != "zero")
                {
                    var teenVal = Teens.FirstOrDefault(x => x.Value == storedDiv.TenInWord);
                    dollarString.Append(!string.IsNullOrEmpty(storedDiv.TenInWord) && storedDiv.TenInWord != "zero" && teenVal.Equals(default(KeyValuePair<int, string>))
                        ? $"-{storedDiv.OneInWord}"
                        : $" {storedDiv.OneInWord}");
                }
                if (!string.IsNullOrEmpty(storedDiv.IllionName) && !SkipOnZeroDollar(storedDiv))
                {
                    dollarString.Append($" {storedDiv.IllionName}");
                }
            }
        }

        return dollarString.ToString().Trim();
    }
    private static bool SkipOnZeroDollar(DollarDivision div) =>
    div == null ||
    (string.IsNullOrEmpty(div.HundredInWord) || div.HundredInWord == "zero") &&
    (string.IsNullOrEmpty(div.TenInWord) || div.TenInWord == "zero") &&
    (string.IsNullOrEmpty(div.OneInWord) || div.OneInWord == "zero");
    private static bool SkipOnZeroCent(CentDivision div) =>
    div == null ||
    ((string.IsNullOrEmpty(div.TenInWord) || div.TenInWord == "zero") &&
    (string.IsNullOrEmpty(div.OneInWord) || div.OneInWord == "zero"));
    private static CentDivision GetCentDivision(string divValue)
    {
        var centDiv = new CentDivision();

        int GetIndex(int pos) => divValue.Length - pos >= 0 ? int.Parse(divValue[divValue.Length - pos].ToString()) : -1;

        int oneIdx = GetIndex(1);
        int tenIdx = GetIndex(2);

        if (oneIdx >= 0) centDiv.OneInWord = Ones[oneIdx];
        if (tenIdx > 1) centDiv.TenInWord = Tens[tenIdx];
        else if (tenIdx == 1) centDiv.TenInWord = Teens.FirstOrDefault(x => x.Key == 10 + oneIdx).Value;

        if (tenIdx == 1) centDiv.OneInWord = string.Empty;

        return centDiv;
    }
    private static DollarDivision? GetDollarDivision(string divValue, int k, string illionName)
    {
        var dollarDiv = new DollarDivision { IllionName = illionName };

        int GetIndex(int pos) => divValue.Length - pos >= 0 ? int.Parse(divValue[divValue.Length - pos].ToString()) : -1;

        int oneIdx = GetIndex(k - 2);
        int tenIdx = GetIndex(k - 1);
        int hundredIdx = GetIndex(k);

        if (oneIdx >= 0) dollarDiv.OneInWord = Ones[oneIdx];
        if (tenIdx > 1) dollarDiv.TenInWord = Tens[tenIdx];
        else if (tenIdx == 1)
        {
            var teen = Teens.FirstOrDefault(x => x.Key == 10 + oneIdx);
            dollarDiv.TenInWord = teen.Value;
            dollarDiv.OneInWord = string.Empty; //override any existing value here coz teen value is enough
        }
        if (hundredIdx >= 0) dollarDiv.HundredInWord = Ones[hundredIdx];

        dollarDiv.DollarInWords = $"{dollarDiv.HundredInWord}{dollarDiv.TenInWord}{dollarDiv.OneInWord}".Trim();

        return string.IsNullOrEmpty(dollarDiv.DollarInWords) ? null : dollarDiv;
    }

}
