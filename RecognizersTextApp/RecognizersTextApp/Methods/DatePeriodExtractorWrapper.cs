using BenchmarkDotNet.Attributes;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextApp.Methods
{
    public class DatePeriodExtractorWrapper
    {
        private readonly Dictionary<string, string> Texts = new Dictionary<string, string>()
        {
            //{ Culture.Chinese, "农历2015年十月初一" },
            //{ Culture.Dutch, "Ik ga terug op 22 april" },
            { Culture.English, "I'll be out the month of Jan" },
            //{ Culture.EnglishOthers, "EnglishOthers" },
            //{ Culture.French, "French" },
            //{ Culture.German, "German" },
            //{ Culture.Italian, "Italian" },
            //{ Culture.Japanese, "Japanese" },
            //{ Culture.Korean, "Korean" },
            //{ Culture.Portuguese, "Portuguese" },
            //{ Culture.Spanish, "Spanish" }
        };

        private readonly Dictionary<string, string> Languages = new Dictionary<string, string>()
        {
            //{ Culture.Chinese, "Chinese" },
            //{ Culture.Dutch, "Dutch" },
            { Culture.English, "English" },
            //{ Culture.EnglishOthers, "EnglishOthers" },
            //{ Culture.French, "French" },
            //{ Culture.German, "German" },
            //{ Culture.Italian, "Italian" },
            //{ Culture.Japanese, "Japanese" },
            //{ Culture.Korean, "Korean" },
            //{ Culture.Portuguese, "Portuguese" },
            //{ Culture.Spanish, "Spanish" }
        };

        public DatePeriodExtractorWrapper()
        {

        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void DatePeriodExtractor(string input, string lang, string scale)
        {
            DateTimeRecognizer.RecognizeDateTime(input, lang);
        }

        public IEnumerable<object[]> Data()
        {
            foreach (var lang in Languages)
            {
                yield return new object[] { ComposeText(Texts[lang.Key], 1), lang.Key, "x1" };
                yield return new object[] { ComposeText(Texts[lang.Key], 10), lang.Key, "x10" };
                yield return new object[] { ComposeText(Texts[lang.Key], 100), lang.Key, "x100" };
            }
        }

        private string ComposeText(string text, int scale)
        {
            var sb = new StringBuilder();
            while (scale > 0)
            {
                sb.Append(text);
                scale -= 1;
            }
            return sb.ToString();
        }
    }
}
