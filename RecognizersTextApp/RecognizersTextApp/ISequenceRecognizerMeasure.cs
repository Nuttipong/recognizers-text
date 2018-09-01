namespace RecognizersTextApp
{
    public interface ISequenceRecognizerMeasure
    {
        void RecognizePhoneNumber(string text, string culture, string scale, string typeName);
        void RecognizeIpAddress(string text, string culture, string scale, string typeName);
        void RecognizeMention(string text, string culture, string scale, string typeName);
        void RecognizeHashtag(string text, string culture, string scale, string typeName);
        void RecognizeEmail(string text, string culture, string scale, string typeName);
        void RecognizeURL(string text, string culture, string scale, string typeName);
    }
}
