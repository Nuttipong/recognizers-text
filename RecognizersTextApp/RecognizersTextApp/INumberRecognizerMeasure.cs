namespace RecognizersTextApp
{
    public interface INumberRecognizerMeasure
    {
        void RecognizeNumber(string text, string culture, string scale);
        void RecognizeOrdinal(string text, string culture);
        void RecognizePercentage(string text, string culture);
        void RecognizeNumberRange(string text, string culture);
    }
}
