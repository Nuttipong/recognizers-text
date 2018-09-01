using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Recognizers.Text.Sequence;
using RecognizersTextApp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextApp
{
    [MedianColumn]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 10)]
    public class SequenceRecognizerMeasure : ISequenceRecognizerMeasure
    {
        private List<DataModel> PhoneDataList = new List<DataModel>();
        private List<DataModel> IpAddressDataList = new List<DataModel>();
        private List<DataModel> MentionDataList = new List<DataModel>();
        private List<DataModel> HashtagDataList = new List<DataModel>();
        private List<DataModel> EmailDataList = new List<DataModel>();
        private List<DataModel> URLDataList = new List<DataModel>();

        public SequenceRecognizerMeasure()
        {
            var tasks = new Task[]
            {
                Task.Run(() => PreparePhoneDataList()),
                Task.Run(() => PrepareIpAddressDataList()),
                Task.Run(() => PrepareMentionDataList()),
                Task.Run(() => PrepareHashtagDataList()),
                Task.Run(() => PrepareEmailDataList()),
                Task.Run(() => PrepareURLDataList())
            };
            Task.WaitAll(tasks);
        }

        [Benchmark]
        [ArgumentsSource(nameof(EmailModel))]
        public void RecognizeEmail(string text, string culture, string scale, string typeName)
        {
            SequenceRecognizer.RecognizeEmail(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(HashtagModel))]
        public void RecognizeHashtag(string text, string culture, string scale, string typeName)
        {
            SequenceRecognizer.RecognizeHashtag(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(IpAddressModel))]
        public void RecognizeIpAddress(string text, string culture, string scale, string typeName)
        {
            SequenceRecognizer.RecognizeIpAddress(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(MentionModel))]
        public void RecognizeMention(string text, string culture, string scale, string typeName)
        {
            SequenceRecognizer.RecognizeMention(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(PhoneModel))]
        public void RecognizePhoneNumber(string text, string culture, string scale, string typeName)
        {
            SequenceRecognizer.RecognizePhoneNumber(text, culture);
        }

        [Benchmark]
        [ArgumentsSource(nameof(URLModel))]
        public void RecognizeURL(string text, string culture, string scale, string typeName)
        {
            SequenceRecognizer.RecognizeURL(text, culture);
        }

        public IEnumerable<object[]> EmailModel()
        {
            foreach (var data in EmailDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> HashtagModel()
        {
            foreach (var data in HashtagDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> IpAddressModel()
        {
            foreach (var data in IpAddressDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> MentionModel()
        {
            foreach (var data in MentionDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> PhoneModel()
        {
            foreach (var data in PhoneDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        public IEnumerable<object[]> URLModel()
        {
            foreach (var data in URLDataList)
            {
                yield return new object[] { data.Text, data.Lang, data.Scale, data.Type };
            }
        }

        private void PreparePhoneDataList()
        {
            foreach (var lang in Program.SequenceDict.Keys)
            {
                var specModel = Program.SequenceDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("phonenumber", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    PhoneDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareIpAddressDataList()
        {
            foreach (var lang in Program.SequenceDict.Keys)
            {
                var specModel = Program.SequenceDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("ip", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    IpAddressDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareMentionDataList()
        {
            foreach (var lang in Program.SequenceDict.Keys)
            {
                var specModel = Program.SequenceDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("mention", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    MentionDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareHashtagDataList()
        {
            foreach (var lang in Program.SequenceDict.Keys)
            {
                var specModel = Program.SequenceDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("hashtag", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    HashtagDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareEmailDataList()
        {
            foreach (var lang in Program.SequenceDict.Keys)
            {
                var specModel = Program.SequenceDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("email", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    EmailDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }

        private void PrepareURLDataList()
        {
            foreach (var lang in Program.SequenceDict.Keys)
            {
                var specModel = Program.SequenceDict[lang]
                    .Where(typeName => typeName.TypeName.Equals("url", System.StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();

                if (specModel != null)
                    URLDataList.AddRange(Program.DoScale(specModel, lang));
            }
        }
        
    }
}
