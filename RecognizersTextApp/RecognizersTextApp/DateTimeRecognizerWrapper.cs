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
using RecognizersTextApp.Model;

namespace RecognizersTextApp
{
    [MinColumn, MaxColumn]
    [Config(typeof(Config))]
    public class DateTimeRecognizerWrapper
    {
        private readonly List<SpecModel> SpecModelList = new List<SpecModel>();
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
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.Dry);
            }
        }


        public DateTimeRecognizerWrapper()
        {
            SetSpecModel();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void RecognizeDateTime(string subMethod, string lang, string text, string scale)
        {
            DateTimeRecognizer.RecognizeDateTime(text, lang);
        }

        public IEnumerable<object[]> Data()
        {
            foreach (var lang in Languages)
            {
                var subMethods = SpecModelList.Where(m => m.Lang == lang.Key);
                if (subMethods.Any())
                {
                    foreach (var subMethod in subMethods)
                    {
                        var scales = new int[] { 1 };
                        foreach (var scale in scales)
                        {
                            yield return new object[] {
                                subMethod.Method,
                                lang.Key,
                                ComposeText(subMethod.Method, lang.Key, scale),
                                $"x{scale}"
                            };
                        }
                    }
                }
            }
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

        private void SetSpecModel()
        {
            var tasks = new Task<IList<SpecModel>>[] {
                Task.Run(() => GetDateTimeSpec(Culture.English)),
                Task.Run(() => GetDateTimeSpec(Culture.Chinese)),
                Task.Run(() => GetDateTimeSpec(Culture.Dutch)),
                Task.Run(() => GetDateTimeSpec(Culture.EnglishOthers)),
                Task.Run(() => GetDateTimeSpec(Culture.French)),
                Task.Run(() => GetDateTimeSpec(Culture.German)),
                Task.Run(() => GetDateTimeSpec(Culture.Italian)),
                Task.Run(() => GetDateTimeSpec(Culture.Japanese)),
                Task.Run(() => GetDateTimeSpec(Culture.Korean)),
                Task.Run(() => GetDateTimeSpec(Culture.Portuguese)),
                Task.Run(() => GetDateTimeSpec(Culture.Spanish))
            };
            Task.WaitAll(tasks);
            foreach (var t in tasks)
            {
                SpecModelList.AddRange(MergeResults(t.Result));
            }
        }
    }
}
