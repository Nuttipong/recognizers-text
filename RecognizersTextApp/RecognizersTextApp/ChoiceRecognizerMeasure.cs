using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Recognizers.Text.Choice;
using RecognizersTextApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace RecognizersTextApp
{
    [MedianColumn]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 10)]
    public class ChoiceRecognizerMeasure : IChoiceRecognizerMeasure
    {
        private List<DataModel> ChoiceDataList = new List<DataModel>();

        public ChoiceRecognizerMeasure()
        {
            PrepareChoiceDataList();
        }

        [Benchmark]
        [ArgumentsSource(nameof(ChoiceModel))]
        public void RecognizeBoolean(string text, string culture, string scale, string typeName)
        {
            ChoiceRecognizer.RecognizeBoolean(text, culture);
        }

        public IEnumerable<object[]> ChoiceModel()
        {
            foreach (var data in ChoiceDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        private void PrepareChoiceDataList()
        {
            foreach (var lang in Program.ChoiceDict.Keys)
            {
                var specModel = Program.ChoiceDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("boolean", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    ChoiceDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }
    }
}
