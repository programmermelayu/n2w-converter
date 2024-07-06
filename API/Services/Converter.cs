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
            for (int i = 0; i < Illions.Count; i++)
            {
                var divName = Illions[i];
                var div = GetDollarDivision(dollar, charLength, divName);
                if (string.IsNullOrEmpty(div.OneInWord)
                && string.IsNullOrEmpty(div.TenInWord)
                && string.IsNullOrEmpty(div.OneInWord)) break;

                var pair = new KeyValuePair<int, DollarDivision>(i, div);
                allPairs.Add(pair);
                charLength += 3;
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
        if (!allowRounding && HasMoreThanTwoDecimalPlaces(value)) return false;
        if (value < 0) return false; // do not allow negative number
        return true;
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
        var dollarString = "";
        for (int i = allPairs.Count - 1; i >= 0; i--)
        {
            var storedDiv = allPairs[i].Value;

            if (allPairs[i].Value != null)
            {
                if (!string.IsNullOrEmpty(storedDiv.HundredInWord) && storedDiv.HundredInWord != "zero")
                {
                    dollarString = $"{dollarString} {storedDiv.HundredInWord} hundred";
                }
                if (!string.IsNullOrEmpty(storedDiv.TenInWord) && storedDiv.TenInWord != "zero")
                {
                    dollarString = $"{dollarString} {storedDiv.TenInWord}";
                }
                if (!string.IsNullOrEmpty(storedDiv.OneInWord) && storedDiv.OneInWord != "zero")
                {
                    var teenVal = Teens.FirstOrDefault(x => x.Value == storedDiv.TenInWord);
                    if (!string.IsNullOrEmpty(storedDiv.TenInWord) && (storedDiv.TenInWord != "zero") &&
                    teenVal.Equals(default(KeyValuePair<int, string>)))
                    {
                        dollarString = $"{dollarString}-{storedDiv.OneInWord}";
                    }
                    else
                    {
                        dollarString = $"{dollarString} {storedDiv.OneInWord}";
                    }
                }
                if (!string.IsNullOrEmpty(storedDiv.IllionName))
                {
                    if (!SkipOnZeroDollar(storedDiv))
                    {
                        dollarString = $"{dollarString} {storedDiv.IllionName}";
                    }

                }
            }
        }
        return dollarString.Trim();
    }
    private static bool SkipOnZeroDollar(DollarDivision div)
    {
        return (string.IsNullOrEmpty(div.HundredInWord) || div.HundredInWord == "zero") &&
        (string.IsNullOrEmpty(div.TenInWord) || div.TenInWord == "zero") &&
        (string.IsNullOrEmpty(div.OneInWord) || div.OneInWord == "zero");
    }
    private static bool SkipOnZeroCent(CentDivision div)
    {
        return ((string.IsNullOrEmpty(div.TenInWord) || div.TenInWord == "zero") &&
        (string.IsNullOrEmpty(div.OneInWord) || div.OneInWord == "zero"));
    }
    private static CentDivision GetCentDivision(string partitionValue)
    {
        var centPartition = new CentDivision();

        int oneIdx = -1;
        var one = "";
        if ((partitionValue.Length - 1) >= 0)
        {
            oneIdx = int.Parse(partitionValue[partitionValue.Length - (1)].ToString());
            one = Ones[oneIdx];
            centPartition.OneInWord = one;
        }

        int tenIdx = -1;
        var ten = "";
        if (partitionValue.Length - (2) >= 0)
            tenIdx = int.Parse(partitionValue[partitionValue.Length - 2].ToString());
        if (tenIdx > 1)
        {
            ten = Tens[tenIdx];
            centPartition.TenInWord = ten;
            centPartition.OneInWord = one;
        }
        else if (tenIdx == 1)
        {
            var key = 10 + oneIdx;
            centPartition.TenInWord = Teens.FirstOrDefault(x => x.Key == key).Value;
            centPartition.OneInWord = "";
        }

        return centPartition;
    }
    private static DollarDivision GetDollarDivision(string divValue, int k, string partitionName)
    {
        // var thousands = Thousands.ToList();
        // var oneNumbers = Ones.ToList();
        // var tensNumbers = Tens.ToList();
        // var teenPairs = Teens.ToList();

        var dollarDiv = new DollarDivision();

        int oneIdx = -1;
        var one = "";
        if (divValue.Length - (k - 2) >= 0)
        {
            oneIdx = int.Parse(divValue[divValue.Length - (k - 2)].ToString());
            one = Ones[oneIdx];
            dollarDiv.OneInWord = one;
        }

        int tenIdx = -1;
        var ten = "";
        if (divValue.Length - (k - 1) >= 0)
            tenIdx = int.Parse(divValue[divValue.Length - (k - 1)].ToString());
        if (tenIdx > 1)
        {
            ten = Tens[tenIdx];
            dollarDiv.TenInWord = ten;
            dollarDiv.OneInWord = one;
        }
        else if (tenIdx == 1)
        {
            var key = 10 + oneIdx;
            dollarDiv.TenInWord = Teens.FirstOrDefault(x => x.Key == key).Value;
            dollarDiv.OneInWord = "";
        }

        int hundredIdx = -1;
        var hundred = "";
        if ((divValue.Length - k) >= 0)
        {
            hundredIdx = int.Parse(divValue[divValue.Length - (k)].ToString());
            hundred = Ones[hundredIdx];
        }

        dollarDiv.IllionName = partitionName;
        dollarDiv.HundredInWord = hundred;

        return dollarDiv;

    }
}
