using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Recognizers.Text.DateTime;
using RecognizersTextApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace RecognizersTextApp
{
    [MedianColumn]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 10)]
    public class DateTimeRecognizerMeasure : IDateTimeRecognizerMeasure
    {
        private List<DataModel> DateTimeDataList = new List<DataModel>();

        public DateTimeRecognizerMeasure()
        {
            PrepareDateTimeDataList();
        }

        [Benchmark]
        [ArgumentsSource(nameof(DateTimeModel))]
        public void RecognizeDateTime(string text, string culture, string scale, string typeName)
        {
            DateTimeRecognizer.RecognizeDateTime(text, culture);
        }

        public IEnumerable<object[]> DateTimeModel()
        {
            foreach (var data in DateTimeDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        private void PrepareDateTimeDataList()
        {
            foreach (var lang in Program.DateTimeDict.Keys)
            {
                var specModel = Program.DateTimeDict[lang].FirstOrDefault();
                if (specModel != null)
                    DateTimeDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

    }
}
