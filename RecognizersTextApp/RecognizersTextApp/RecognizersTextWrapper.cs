﻿using BenchmarkDotNet.Attributes;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.Choice;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.NumberWithUnit;
using Microsoft.Recognizers.Text.Sequence;
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
    public class RecognizersTextWrapper
    {
        public RecognizersTextWrapper()
        {

        }

        [Benchmark]
        //[ArgumentsSource(nameof(Data))]
        public void DateExtractor(string input, string lang, string scale)
        {
            DateTimeRecognizer.RecognizeDateTime(input, lang);
        }

        //public IEnumerable<object[]> Data()
        //{
        //    foreach (var lang in Program.Languages)
        //    {
        //        //yield return new object[] { ComposeText(Program.Texts[lang.Key], 1), lang.Key, "x1" };
        //        //yield return new object[] { ComposeText(Program.Texts[lang.Key], 10), lang.Key, "x10" };
        //        //yield return new object[] { ComposeText(Program.Texts[lang.Key], 100), lang.Key, "x100" };
        //    }
        //}

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

        private IEnumerable<ModelResult> ParseAll(string query, string culture)
        {
            return MergeResults(
                // Number recognizer will find any number from the input
                // E.g "I have two apples" will return "2".
                NumberRecognizer.RecognizeNumber(query, culture),

                // Ordinal number recognizer will find any ordinal number
                // E.g "eleventh" will return "11".
                NumberRecognizer.RecognizeOrdinal(query, culture),

                // Percentage recognizer will find any number presented as percentage
                // E.g "one hundred percents" will return "100%"
                NumberRecognizer.RecognizePercentage(query, culture),

                // Number Range recognizer will find any cardinal or ordinal number range
                // E.g. "between 2 and 5" will return "(2,5)"
                NumberRecognizer.RecognizeNumberRange(query, culture),

                // Age recognizer will find any age number presented
                // E.g "After ninety five years of age, perspectives change" will return "95 Year"
                NumberWithUnitRecognizer.RecognizeAge(query, culture),

                // Currency recognizer will find any currency presented
                // E.g "Interest expense in the 1988 third quarter was $ 75.3 million" will return "75300000 Dollar"
                NumberWithUnitRecognizer.RecognizeCurrency(query, culture),

                // Dimension recognizer will find any dimension presented
                // E.g "The six-mile trip to my airport hotel that had taken 20 minutes earlier in the day took more than three hours." will return "6 Mile"
                NumberWithUnitRecognizer.RecognizeDimension(query, culture),

                // Temperature recognizer will find any temperature presented
                // E.g "Set the temperature to 30 degrees celsius" will return "30 C"
                NumberWithUnitRecognizer.RecognizeTemperature(query, culture),

                // Datetime recognizer This model will find any Date even if its write in coloquial language 
                // E.g "I'll go back 8pm today" will return "2017-10-04 20:00:00"
                DateTimeRecognizer.RecognizeDateTime(query, culture),

                // PhoneNumber recognizer will find any phone number presented
                // E.g "My phone number is ( 19 ) 38294427."
                SequenceRecognizer.RecognizePhoneNumber(query, culture),

                // Add IP recognizer - This recognizer will find any Ipv4/Ipv6 presented
                // E.g "My Ip is 8.8.8.8"
                SequenceRecognizer.RecognizeIpAddress(query, culture),

                // Mention recognizer will find all the mention usages
                // E.g "@Cicero"
                SequenceRecognizer.RecognizeMention(query, culture),

                // Hashtag recognizer will find all the hash tag usages
                // E.g "task #123"
                SequenceRecognizer.RecognizeHashtag(query, culture),

                // Email recognizer will find all the emails
                // E.g "a@b.com"
                SequenceRecognizer.RecognizeEmail(query, culture),

                // URL recognizer will find all the urls
                // E.g "bing.com"
                SequenceRecognizer.RecognizeURL(query, culture),

                // Add Boolean recognizer - This model will find yes/no like responses, including emoji -
                // E.g "yup, I need that" will return "True"
                ChoiceRecognizer.RecognizeBoolean(query, culture)
                );
        }

        private IEnumerable<ModelResult> MergeResults(params List<ModelResult>[] results)
        {
            return results.SelectMany(o => o);
        }
    }
}
