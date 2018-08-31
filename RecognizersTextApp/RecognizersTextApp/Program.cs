using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.Choice;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.NumberWithUnit;
using Microsoft.Recognizers.Text.Sequence;
using Newtonsoft.Json;
using RecognizersTextApp.Methods;
using RecognizersTextApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecognizersTextApp
{
    public class Program
    {
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

        private static Dictionary<string, string> GetSubMethods(string method, string lang)
        {
            var methods = new Dictionary<string, string>();
            var langDirectory = Path.Combine("..", "..", "Specs", method, lang);
            var specsFiles = Directory.GetFiles(langDirectory);
            foreach (var specsFile in specsFiles)
            {
                var data = File.ReadAllText(specsFile);
                var spec = JsonConvert.DeserializeObject<IList<SpecModel>>(data).Take(1).ToArray();
                methods.Add(Path.GetFileNameWithoutExtension(specsFile), spec[0].Input);
            }
            return methods;
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

        public static readonly Dictionary<string, Dictionary<string, string>> NumberDict = new Dictionary<string, Dictionary<string, string>>();

        public static void Main(string[] args)
        {
            var tasks = new Task[]
            {
                Task.Run(() => SetNumberDictSpec())
            };
            Task.WaitAll(tasks);

            BenchmarkRunner.Run<NumberRecognizerMeasure>();
        }
    }
}
