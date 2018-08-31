using BenchmarkDotNet.Attributes;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Newtonsoft.Json;
using RecognizersTextApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextApp.Methods
{
    public class DateExtractorWrapper
    {
        private readonly Dictionary<string, string> Texts = new Dictionary<string, string>()
        {
            //{ Culture.Chinese, "农历2015年十月初一" },
            //{ Culture.Dutch, "Ik ga terug op 22 april" },
            { Culture.English, "I'll go back on 15" },
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

        public DateExtractorWrapper()
        {

        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void DateExtractor(string input, string lang, string scale)
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
