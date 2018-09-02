# Recognizers Text Experiment

## Getting Started

    git clone git@github.com:Nuttipong/recognizers-text.git
    cd RecognizersTextApp

## Setup
  
    build the solution manually and then choose solution configuration as Release and Start(F5)

## What we do experiment is increase variable text sizes multiply by 1, 10 and 100 and also support multiple langauges.

### Microsoft.Recognizers.Text.Number
```
    public interface INumberRecognizerMeasure
    {
        void RecognizeNumber(string text, string culture, string scale, string typeName);
        void RecognizeOrdinal(string text, string culture, string scale, string typeName);
        void RecognizePercentage(string text, string culture, string scale, string typeName);
        void RecognizeNumberRange(string text, string culture, string scale, string typeName);
    }
```

### Microsoft.Recognizers.Text.NumberWithUnit
```
    public interface INumberWithUnitRecognizerMeasure
    {
        void RecognizeAge(string text, string culture, string scale, string typeName);
        void RecognizeCurrency(string text, string culture, string scale, string typeName);
        void RecognizeDimension(string text, string culture, string scale, string typeName);
        void RecognizeTemperature(string text, string culture, string scale, string typeName);
    }
```

### Microsoft.Recognizers.Text.DateTime
```
    public interface IDateTimeRecognizerMeasure
    {
        void RecognizeDateTime(string text, string culture, string scale, string typeName);
    }
```

### Microsoft.Recognizers.Text.Sequence
```
    public interface ISequenceRecognizerMeasure
    {
        void RecognizePhoneNumber(string text, string culture, string scale, string typeName);
        void RecognizeIpAddress(string text, string culture, string scale, string typeName);
        void RecognizeMention(string text, string culture, string scale, string typeName);
        void RecognizeHashtag(string text, string culture, string scale, string typeName);
        void RecognizeEmail(string text, string culture, string scale, string typeName);
        void RecognizeURL(string text, string culture, string scale, string typeName);
    }
```

### Microsoft.Recognizers.Text.Choice
```
    public interface IChoiceRecognizerMeasure
    {
        void RecognizeBoolean(string text, string culture, string scale, string typeName);
    }
```

## Dependencies

* BenchmarkDotNet:
`Install-Package BenchmarkDotNet -Version 0.11.1`

* Newtonsoft.Json:
`Install-Package Newtonsoft.Json -Version 11.0.2`

* Microsoft.Recognizers.Text:
`Install-Package Microsoft.Recognizers.Text -Version 1.0.11`

* Microsoft.Recognizers.Text.Choice:
`Install-Package Microsoft.Recognizers.Choice -Version 1.0.11`
   
* Microsoft.Recognizers.Text.DateTime:
`Install-Package Microsoft.Recognizers.DateTime -Version 1.0.11`

* Microsoft.Recognizers.Text.Number:
`Install-Package Microsoft.Recognizers.Number -Version 1.0.11`

* Microsoft.Recognizers.Text.NumberWithUnit:
`Install-Package Microsoft.Recognizers.NumberWithUnit -Version 1.0.11`

* Microsoft.Recognizers.Text.Sequence:
`Install-Package Microsoft.Recognizers.Sequence -Version 1.0.11`

## How to represent data
  
    npm install or yarn install
    npm start or yarn start
    
### Samples
![alt text](https://github.com/Nuttipong/recognizers-text/blob/master/RecognizersTextApp/RecognizersTextApp/screenshot.png)

    

