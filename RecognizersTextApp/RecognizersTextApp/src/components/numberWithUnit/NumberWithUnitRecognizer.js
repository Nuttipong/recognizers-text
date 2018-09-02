import React from 'react';
import Papa from 'papaparse';

class NumberWithUnitRecognizer extends React.Component {

  constructor() {
    super();
    Papa.parse("http://localhost:8080/bin/Release/BenchmarkDotNet.Artifacts/results/RecognizersTextApp.NumberWithUnitRecognizerMeasure-report.csv", 
    {
      config: {
        header: false,
      },
      download: true,
      complete: function(results) {
        console.log("Finished:", results.data);
        results.data.shift();

        const x1 = results.data.filter(d => d[42] == 'x1');
        const x10 = results.data.filter(d => d[42] == 'x10');
        const x100 = results.data.filter(d => d[42] == 'x100');

        renderX1(x1);
        renderX10(x10);
        renderX100(x100);

      }
    });
  }

  render() {
    return (
      <div>
        <h4 style={{textAlign: 'right'}}>IterationCount=10  RunStrategy=ColdStart</h4>
        <div className="row" style={{marginBottom: '45%'}}>
          <div id="chartX1"></div>
        </div>
        <div className="row" style={{marginBottom: '45%'}}>
          <div id="chartX10"></div>
        </div>
        <div className="row" style={{marginBottom: '45%'}}>
          <div id="chartX100"></div>
        </div>
      </div>
    );
  }
}

function renderX1(x1) {

  let data = [];
  const methods = ['RecognizeAge', 'RecognizeCurrency', 'RecognizeDimension', 'RecognizeTemperature'];
  const langs = ['Chinese', 'Dutch', 'English', 'French', 'German', 'Italian', 'Japanese', 'Korean', 'Portuguese', 'Spanish'];

  methods.forEach((method) => {
    let b = [];

    langs.forEach((lang) => {
      let a = x1.filter(d => d[0] == method && d[41] == lang);
      if (a === undefined || a === null || a.length === 0) {
        b.push(
          { y: 0, label: lang }
        );
      } else {
        a.forEach((d) => {
          const l = d[41], mean = d[44];
          b.push(
            { y: parseFloat(mean.replace(',', ''), 10), label: l }
          );
        });
      }
    });

    data.push({
      type: "spline",
      name: method,
      showInLegend: true,
      dataPoints: b
    });

  });

  const chartX1 = new CanvasJS.Chart("chartX1", {
    title:{
      text: "number with unit recognizer x1"
    },
    axisY : {
      title: "execution time",
      includeZero: false
    },
    toolTip: {
      shared: true
    },
    data: data
  });
  chartX1.render();
}

function renderX10(x10) {

  let data = [];
  const methods = ['RecognizeAge', 'RecognizeCurrency', 'RecognizeDimension', 'RecognizeTemperature'];
  const langs = ['Chinese', 'Dutch', 'English', 'French', 'German', 'Italian', 'Japanese', 'Korean', 'Portuguese', 'Spanish'];

  methods.forEach((method) => {
    let b = [];

    langs.forEach((lang) => {
      let a = x10.filter(d => d[0] == method && d[41] == lang);
      if (a === undefined || a === null || a.length === 0) {
        b.push(
          { y: 0, label: lang }
        );
      } else {
        a.forEach((d) => {
          const l = d[41], mean = d[44];
          b.push(
            { y: parseFloat(mean.replace(',', ''), 10), label: l }
          );
        });
      }
    });

    data.push({
      type: "spline",
      name: method,
      showInLegend: true,
      dataPoints: b
    });

  });

  const chartX10 = new CanvasJS.Chart("chartX10", {
    title:{
      text: "number with unit recognizer x10"
    },
    axisY : {
      title: "execution time",
      includeZero: false
    },
    toolTip: {
      shared: true
    },
    data: data
  });
  chartX10.render();
}

function renderX100(x100) {

  let data = [];
  const methods = ['RecognizeAge', 'RecognizeCurrency', 'RecognizeDimension', 'RecognizeTemperature'];
  const langs = ['Chinese', 'Dutch', 'English', 'French', 'German', 'Italian', 'Japanese', 'Korean', 'Portuguese', 'Spanish'];

  methods.forEach((method) => {
    let b = [];

    langs.forEach((lang) => {
      let a = x100.filter(d => d[0] == method && d[41] == lang);
      if (a === undefined || a === null || a.length === 0) {
        b.push(
          { y: 0, label: lang }
        );
      } else {
        a.forEach((d) => {
          const l = d[41], mean = d[44];
          b.push(
            { y: parseFloat(mean.replace(',', ''), 10), label: l }
          );
        });
      }
    });

    data.push({
      type: "spline",
      name: method,
      showInLegend: true,
      dataPoints: b
    });

  });

  const chartX100 = new CanvasJS.Chart("chartX100", {
    title:{
      text: "number with unit recognizer x100"
    },
    axisY : {
      title: "execution time",
      includeZero: false
    },
    toolTip: {
      shared: true
    },
    data: data
  });
  chartX100.render();
}

export default NumberWithUnitRecognizer;
