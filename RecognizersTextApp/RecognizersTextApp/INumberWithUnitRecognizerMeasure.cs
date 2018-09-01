namespace RecognizersTextApp
{
    public interface INumberWithUnitRecognizerMeasure
    {
        void RecognizeAge(string text, string culture, string scale, string typeName);
        void RecognizeCurrency(string text, string culture, string scale, string typeName);
        void RecognizeDimension(string text, string culture, string scale, string typeName);
        void RecognizeTemperature(string text, string culture, string scale, string typeName);
    }
}
