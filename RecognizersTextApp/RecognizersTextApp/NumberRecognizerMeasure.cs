using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Recognizers.Text.Number;
using RecognizersTextApp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextApp
{
    [MedianColumn]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 10)]
    public class NumberRecognizerMeasure : INumberRecognizerMeasure
    {
        private List<DataModel> NumberDataList = new List<DataModel>();
        private List<DataModel> NumberRangeDataList = new List<DataModel>();
        private List<DataModel> OrdinalDataList = new List<DataModel>();
        private List<DataModel> PercentageDataList = new List<DataModel>();

        public NumberRecognizerMeasure()
        {
            var tasks = new Task[]
            {
                Task.Run(() => PrepareNumberDataList()),
                Task.Run(() => PrepareNumberRangeDataList()),
                Task.Run(() => PrepareOrdinalDataList()),
                Task.Run(() => PreparePercentageDataList())
            };
            Task.WaitAll(tasks);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumberModel))]
        public void RecognizeNumber(string text, string culture, string scale, string typeName)
        {
            NumberRecognizer.RecognizeNumber(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(NumberRangeModel))]
        public void RecognizeNumberRange(string text, string culture, string scale, string typeName)
        {
            NumberRecognizer.RecognizeNumberRange(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(OrdinalModel))]
        public void RecognizeOrdinal(string text, string culture, string scale, string typeName)
        {
            NumberRecognizer.RecognizeOrdinal(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(PercentageModel))]
        public void RecognizePercentage(string text, string culture, string scale, string typeName)
        {
            NumberRecognizer.RecognizePercentage(text, culture);
        }

        public IEnumerable<object[]> NumberModel()
        {
            foreach (var data in NumberDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> NumberRangeModel()
        {
            foreach (var data in NumberRangeDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> OrdinalModel()
        {
            foreach (var data in OrdinalDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> PercentageModel()
        {
            foreach (var data in PercentageDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        private void PrepareNumberDataList()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                var specModel = Program.NumberDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("number", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    NumberDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareNumberRangeDataList()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                var specModel = Program.NumberDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("numberrange", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    NumberRangeDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareOrdinalDataList()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                var specModel = Program.NumberDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("ordinal", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    OrdinalDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PreparePercentageDataList()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                var specModel = Program.NumberDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("percentage", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    PercentageDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }
    }
}
