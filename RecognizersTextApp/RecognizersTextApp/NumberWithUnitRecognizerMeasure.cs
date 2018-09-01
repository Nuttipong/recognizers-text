using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Recognizers.Text.NumberWithUnit;
using RecognizersTextApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecognizersTextApp
{
    [MedianColumn]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 10)]
    public class NumberWithUnitRecognizerMeasure : INumberWithUnitRecognizerMeasure
    {
        private List<DataModel> AgeDataList = new List<DataModel>();
        private List<DataModel> CurrencyDataList = new List<DataModel>();
        private List<DataModel> DimensionDataList = new List<DataModel>();
        private List<DataModel> TemperatureDataList = new List<DataModel>();

        public NumberWithUnitRecognizerMeasure()
        {
            var tasks = new Task[]
            {
                Task.Run(() => PrepareAgeDataList()),
                Task.Run(() => PrepareCurrencyDataList()),
                Task.Run(() => PrepareDimensionDataList()),
                Task.Run(() => PrepareTemperatureDataList())
            };
            Task.WaitAll(tasks);
        }

        [Benchmark]
        [ArgumentsSource(nameof(AgeModel))]
        public void RecognizeAge(string text, string culture, string scale, string typeName)
        {
            NumberWithUnitRecognizer.RecognizeAge(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(CurrencyModel))]
        public void RecognizeCurrency(string text, string culture, string scale, string typeName)
        {
            NumberWithUnitRecognizer.RecognizeCurrency(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(DimensionModel))]
        public void RecognizeDimension(string text, string culture, string scale, string typeName)
        {
            NumberWithUnitRecognizer.RecognizeDimension(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(TemperatureModel))]
        public void RecognizeTemperature(string text, string culture, string scale, string typeName)
        {
            NumberWithUnitRecognizer.RecognizeTemperature(text, culture);
        }

        public IEnumerable<object[]> AgeModel()
        {
            foreach (var data in AgeDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> CurrencyModel()
        {
            foreach (var data in CurrencyDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> DimensionModel()
        {
            foreach (var data in DimensionDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> TemperatureModel()
        {
            foreach (var data in TemperatureDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        private void PrepareAgeDataList()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                var specModel = Program.NumberDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("age", StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    AgeDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareCurrencyDataList()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                var specModel = Program.NumberDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("currency", StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    CurrencyDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareDimensionDataList()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                var specModel = Program.NumberDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("dimension", StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    DimensionDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareTemperatureDataList()
        {
            foreach (var lang in Program.NumberDict.Keys)
            {
                var specModel = Program.NumberDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("temperature", StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    TemperatureDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }
    }
}
