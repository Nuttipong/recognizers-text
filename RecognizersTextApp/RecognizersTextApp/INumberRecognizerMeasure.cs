namespace RecognizersTextApp
{
    public interface INumberRecognizerMeasure
    {
        void RecognizeNumber(string text, string culture, string scale, string typeName);
        void RecognizeOrdinal(string text, string culture, string scale, string typeName);
        void RecognizePercentage(string text, string culture, string scale, string typeName);
        void RecognizeNumberRange(string text, string culture, string scale, string typeName);
    }
}
