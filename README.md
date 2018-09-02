# Recognizers Text Experiment

## Introduce

    What I am doing is performance experiment with Microsoft.Recognizers.Text library which is recognition datetime, number, number with unit, sequence and choice from the text. So we want to measure the execution time for each methods.

## What we do experiment is

* perform per methods.
* perform per languages.
* perform variable text sizes to multiply by 1, 10 and 100.
* using .NET Framework (4.6.2).
* for each methods we does iteration 10 times.

## What methods that we do experiment as below

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

## Getting Started

    git clone git@github.com:Nuttipong/recognizers-text.git
    cd RecognizersTextApp
    
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

## How to maesure
  
    build the solution manually and then choose solution configuration as Release and Start(F5). The results we generated in
    bin\Release\BenchmarkDotNet.Artifacts\results path.

## How to represent data
  
    npm install or yarn install
    npm start or yarn start
        
### Sample result
![alt text](https://github.com/Nuttipong/recognizers-text/blob/master/RecognizersTextApp/RecognizersTextApp/screenshot.png)

## Isuue Founds

* Microsoft.Recognizers.Text.Number:
  Scale x1 => No issue
  Scale x10 => Found issue with RecognizeNumberRange method. It take more execution time when text size large.
  Scale x100 => Found issue with RecognizeNumberRange method. It take more execution time when text size large.

* Microsoft.Recognizers.Text.NumberWithUnit:
  Scale x1 => No issue
  Scale x10 => No issue
  Scale x100 => Found issue with RecognizeCurrency method. It take more execution time when text size large.
  
* Microsoft.Recognizers.Text.DateTime:
  Scale x1 => No issue
  Scale x10 => No issue
  Scale x100 => Found issue with Chinese language. It take more execution time when text size large compare with other languages.
  
* Microsoft.Recognizers.Text.Choice:
  Scale x1 => No issue
  Scale x10 => No issue
  Scale x100 => Found issue with English language. It take more execution time when text size large compare with other languages.

* Microsoft.Recognizers.Text.Sequence:
  Scale x1 => No issue
  Scale x10 => No issue
  Scale x100 => No issue


    

