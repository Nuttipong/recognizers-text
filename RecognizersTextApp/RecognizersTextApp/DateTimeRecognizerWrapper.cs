using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Newtonsoft.Json;

namespace RecognizersTextApp
{
    [MinColumn, MaxColumn]
    [Config(typeof(Config))]
    public class DateTimeRecognizerWrapper
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.Dry);
            }
        }

        private readonly Dictionary<string, string> Languages = new Dictionary<string, string>()
        {
            { Culture.Chinese, "Chinese" },
            { Culture.Dutch, "Dutch" },
            { Culture.English, "English" },
            { Culture.EnglishOthers, "EnglishOthers" },
            { Culture.French, "French" },
            { Culture.German, "German" },
            { Culture.Italian, "Italian" },
            { Culture.Japanese, "Japanese" },
            { Culture.Korean, "Korean" },
            { Culture.Portuguese, "Portuguese" },
            { Culture.Spanish, "Spanish" }
        };

        private readonly IList<SpecModel> SpecModelList;

        public DateTimeRecognizerWrapper()
        {
            var t1 = Task.Run(() => GetDateTimeSpec(Culture.English));
            var t2 = Task.Run(() => GetDateTimeSpec(Culture.Chinese));
            var t3 = Task.Run(() => GetDateTimeSpec(Culture.Dutch));
            var t4 = Task.Run(() => GetDateTimeSpec(Culture.EnglishOthers));
            var t5 = Task.Run(() => GetDateTimeSpec(Culture.French));
            var t6 = Task.Run(() => GetDateTimeSpec(Culture.German));
            var t7 = Task.Run(() => GetDateTimeSpec(Culture.Italian));
            var t8 = Task.Run(() => GetDateTimeSpec(Culture.Japanese));
            var t9 = Task.Run(() => GetDateTimeSpec(Culture.Korean));
            var t10 = Task.Run(() => GetDateTimeSpec(Culture.Portuguese));
            var t11 = Task.Run(() => GetDateTimeSpec(Culture.Spanish));
            Task.WaitAll(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
            SpecModelList = MergeResults(t1.Result, 
                t2.Result,
                t3.Result, 
                t4.Result, 
                t5.Result, 
                t6.Result,
                t7.Result, 
                t8.Result, 
                t9.Result, 
                t10.Result,
                t11.Result).ToList();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void RecognizeDateTime(string subMethod, string lang, string text, int scale)
        {
            DateTimeRecognizer.RecognizeDateTime(text, lang);
        }

        public IEnumerable<object[]> Data()
        {
            yield return new object[] { "DateExtractor", Culture.English, ComposeText("DateExtractor", Culture.English, 1), 1 };
            yield return new object[] { "DateExtractor", Culture.English, ComposeText("DateExtractor", Culture.English, 10), 10 };
            yield return new object[] { "MergedExtractor", Culture.English, ComposeText("MergedExtractor", Culture.English, 1), 1 };
            yield return new object[] { "MergedExtractor", Culture.English, ComposeText("MergedExtractor", Culture.English, 10), 10 };
          

            yield return new object[] { "DateExtractor", Culture.Chinese, ComposeText("DateExtractor", Culture.Chinese, 1), 1 };
            yield return new object[] { "DateExtractor", Culture.Chinese, ComposeText("DateExtractor", Culture.Chinese, 10), 10 };
            yield return new object[] { "MergedExtractor", Culture.Chinese, ComposeText("MergedExtractor", Culture.Chinese, 1), 1 };
            yield return new object[] { "MergedExtractor", Culture.Chinese, ComposeText("MergedExtractor", Culture.Chinese, 10), 10 };
        }

        private string ComposeText(string method, string lang, int scale)
        {
            StringBuilder sb = new StringBuilder();
            while (scale > 0)
            {
                var filterLang = SpecModelList.Where(spec => spec.Lang == lang && spec.Method == method).ToList();
                filterLang.ForEach(s =>
                {
                    sb.Append(s.Input);
                    sb.Append(string.Empty);
                });                
                scale -= 1;
            }
            return sb.ToString();
        }

        private IList<SpecModel> GetDateTimeSpec(string lang)
        {
            var modelResults = new List<SpecModel>();
            var directorySpecs = Path.Combine("..", "..", "Specs", "DateTime", Languages[lang]);
            var specsFiles = Directory.GetFiles(directorySpecs);
            foreach (var specsFile in specsFiles)
            {
                var data = File.ReadAllText(specsFile);
                var specs = JsonConvert.DeserializeObject<IList<SpecModel>>(data);
                specs.Select(r => {
                    r.Lang = lang;
                    r.Method = Path.GetFileNameWithoutExtension(specsFile);
                    return r;
                }).ToList();
                modelResults.AddRange(specs);
            }
            return modelResults;
        }

        private IEnumerable<SpecModel> MergeResults(params IList<SpecModel>[] results)
        {
            return results.SelectMany(o => o);
        }
    }

    public class SpecModel
    {
        public string Input { get; set; }
        public IEnumerable<object> Results { get; set; }
        public string Lang { get; set; }
        public string Method { get; set; }
    }
}
