using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using RecognizersTextApp.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextApp
{
    public class Program
    {
        public static readonly Dictionary<string, IList<SpecModel>> NumberDict = new Dictionary<string, IList<SpecModel>>();
        public static readonly Dictionary<string, IList<SpecModel>> NumberWithUnitDict = new Dictionary<string, IList<SpecModel>>();
        public static readonly Dictionary<string, IList<SpecModel>> DateTimeDict = new Dictionary<string, IList<SpecModel>>();
        public static readonly Dictionary<string, IList<SpecModel>> SequenceDict = new Dictionary<string, IList<SpecModel>>();
        public static readonly Dictionary<string, IList<SpecModel>> ChoiceDict = new Dictionary<string, IList<SpecModel>>();

        public static void Main(string[] args)
        {
            var tasks = new Task[]
            {
                Task.Run(() => SetNumberDictSpec()),
                Task.Run(() => SetNumberWithUnitDictSpec()),
                Task.Run(() => SetDateTimeDictSpec()),
                Task.Run(() => SetSequenceDictSpec()),
                Task.Run(() => SetChoiceDictSpec())
            };
            Task.WaitAll(tasks);

            Parallel.Invoke(
                () => BenchmarkRunner.Run<NumberRecognizerMeasure>(),
                () => BenchmarkRunner.Run<NumberWithUnitRecognizerMeasure>(),
                () => BenchmarkRunner.Run<DateTimeRecognizerMeasure>(),
                () => BenchmarkRunner.Run<SequenceRecognizerMeasure>(),
                () => BenchmarkRunner.Run<ChoiceRecognizerMeasure>()
            );
        }

        private static IList<string> GetLanguages(string method)
        {
            var languages = new List<string>();
            var methodDirectory = Path.Combine("..", "..", "Specs", method);
            var langDirectory = Directory.GetDirectories(methodDirectory);
            foreach (var lang in langDirectory)
            {
                languages.Add(Path.GetFileName(lang));
            }
            return languages;
        }

        private static IList<SpecModel> GetSubMethods(string method, string lang)
        {
            var specModels = new List<SpecModel>();
            var langDirectory = Path.Combine("..", "..", "Specs", method, lang);
            var specsFiles = Directory.GetFiles(langDirectory);
            foreach (var specsFile in specsFiles)
            {
                var data = File.ReadAllText(specsFile);
                var spec = JsonConvert.DeserializeObject<IList<SpecModel>>(data).FirstOrDefault();
                if (spec != null && spec.Results.Any())
                {
                    spec.Lang = lang;
                    spec.Method = Path.GetFileNameWithoutExtension(specsFile);
                    spec.TypeName = spec.Results.Select(a => a.TypeName).Distinct().First();
                    specModels.Add(spec);
                }
            }
            return specModels;
        }

        private static IList<SpecModel> GetSubDateTimeMethods(string method, string lang)
        {
            var specModels = new List<SpecModel>();
            var langDirectory = Path.Combine("..", "..", "Specs", method, lang);
            var specsFiles = Directory.GetFiles(langDirectory, "DateExtractor.json");
            foreach (var specsFile in specsFiles)
            {
                var data = File.ReadAllText(specsFile);
                var spec = JsonConvert.DeserializeObject<IList<SpecModel>>(data).FirstOrDefault();
                if (spec != null && spec.Results.Any())
                {
                    spec.Lang = lang;
                    spec.Method = Path.GetFileNameWithoutExtension(specsFile);
                    spec.TypeName = spec.Results.Select(a => a.Type).Distinct().First();
                    specModels.Add(spec);
                }
            }
            return specModels;
        }

        private static string ComposeText(string text, string lang, int scale)
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

        public static IList<DataModel> DoScale(SpecModel specModel, string lang)
        {
            var dataModels = new List<DataModel>();
            var scales = new int[] { 1, 10, 100 };
            foreach (var scale in scales)
            {
                dataModels.Add(new DataModel()
                {
                    Text = ComposeText(specModel.Input, lang, scale),
                    Lang = lang,
                    Scale = $"x{scale}",
                    Type = specModel.TypeName
                });
            }
            return dataModels;
        }

        private static void SetNumberDictSpec()
        {
            var langs = GetLanguages("Number");
            foreach (var lang in langs)
            {
                var dict = GetSubMethods("Number", lang);
                NumberDict.Add(lang, dict);
            }
        }

        private static void SetNumberWithUnitDictSpec()
        {
            var langs = GetLanguages("NumberWithUnit");
            foreach (var lang in langs)
            {
                var dict = GetSubMethods("NumberWithUnit", lang);
                NumberWithUnitDict.Add(lang, dict);
            }
        }

        private static void SetDateTimeDictSpec()
        {
            var langs = GetLanguages("DateTime");
            foreach (var lang in langs)
            {
                var dict = GetSubDateTimeMethods("DateTime", lang);
                DateTimeDict.Add(lang, dict);
            }
        }

        private static void SetSequenceDictSpec()
        {
            var langs = GetLanguages("Sequence");
            foreach (var lang in langs)
            {
                var dict = GetSubMethods("Sequence", lang);
                SequenceDict.Add(lang, dict);
            }
        }

        private static void SetChoiceDictSpec()
        {
            var langs = GetLanguages("Choice");
            foreach (var lang in langs)
            {
                var dict = GetSubMethods("Choice", lang);
                ChoiceDict.Add(lang, dict);
            }
        }
    }
}
