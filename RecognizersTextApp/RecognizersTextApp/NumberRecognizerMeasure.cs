using BenchmarkDotNet.Attributes;
using Microsoft.Recognizers.Text.Number;
using System.Collections.Generic;
using System.Text;

namespace RecognizersTextApp
{
    [MinColumn, MaxColumn]
    public class NumberRecognizerMeasure : INumberRecognizerMeasure
    {
        public NumberRecognizerMeasure()
        {

        }

        [Benchmark]
        [ArgumentsSource(nameof(NumberModel))]
        public void RecognizeNumber(string text, string culture, string scale)
        {
            NumberRecognizer.RecognizeNumber(text, culture);
        }

        //[Benchmark]
        //[ArgumentsSource(nameof(Data))]
        public void RecognizeNumberRange(string text, string culture)
        {
            NumberRecognizer.RecognizeNumberRange(text, culture);
        }

        //[Benchmark]
        //[ArgumentsSource(nameof(Data))]
        public void RecognizeOrdinal(string text, string culture)
        {
            NumberRecognizer.RecognizeOrdinal(text, culture);
        }

        //[Benchmark]
        //[ArgumentsSource(nameof(Data))]
        public void RecognizePercentage(string text, string culture)
        {
            NumberRecognizer.RecognizePercentage(text, culture);
        }

        public IEnumerable<object[]> NumberModel()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                if (Program.NumberDict[lang].ContainsKey("NumberModel"))
                {
                    var text = Program.NumberDict[lang]["NumberModel"];
                    var scales = new int[] { 1 };
                    foreach (var scale in scales)
                    {
                        yield return new object[] {
                            ComposeText(text, lang, scale),
                            lang,
                            $"x{scale}"
                        };
                    }
                }
            }
        }

        private string ComposeText(string text, string lang, int scale)
        {
            var sb = new StringBuilder();
            while (scale > 0)
            {
                sb.Append(text);
                sb.Append(string.Empty);
                scale -= 1;
            }
            return sb.ToString();
        }
    }
}
