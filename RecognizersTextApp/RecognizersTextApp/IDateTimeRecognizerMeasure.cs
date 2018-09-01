namespace RecognizersTextApp
{
    public interface IDateTimeRecognizerMeasure
    {
        void RecognizeDateTime(string text, string culture, string scale, string typeName);
    }
}
