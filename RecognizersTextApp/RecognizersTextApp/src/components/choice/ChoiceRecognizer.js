import React from 'react';
import Papa from 'papaparse';

class ChoiceRecognizer extends React.Component {

  constructor() {
    super();
    Papa.parse("http://localhost:8080/bin/Release/BenchmarkDotNet.Artifacts/results/RecognizersTextApp.ChoiceRecognizerMeasure-report.csv", 
    {
      config: {
        header: false,
      },
      download: true,
      complete: function(results) {
        console.log("Finished:", results.data);
        results.data.shift();

        const x1 = results.data.filter(d => d[0] == 'RecognizeBoolean' && d[42] == 'x1');
        const x10 = results.data.filter(d => d[0] == 'RecognizeBoolean' && d[42] == 'x10');
        const x100 = results.data.filter(d => d[0] == 'RecognizeBoolean' && d[42] == 'x100');

        renderX1(x1);
        renderX10(x10);
        renderX100(x100);

      }
    });
  }

	componentDidMount() {

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
  let name = '';
  let dataPoints = [];

  x1.forEach((d, i) => {
      const method = d[0], lang = d[41], mean = d[44];
      name = method;
      dataPoints.push({
        y: parseFloat(mean, 10), label: lang
      });
  });

  data.push({
    type: "spline",
    name: name,
    showInLegend: true,
    dataPoints: dataPoints
  });

  const chartX1 = new CanvasJS.Chart("chartX1", {
    title:{
      text: "choice recognizer x1"
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
  let name = '';
  let dataPoints = [];

  x10.forEach((d, i) => {
      const method = d[0], lang = d[41], mean = d[44];
      name = method;
      dataPoints.push({
        y: parseFloat(mean, 10), label: lang
      });
  });

  data.push({
    type: "spline",
    name: name,
    showInLegend: true,
    dataPoints: dataPoints
  });

  const chartX10 = new CanvasJS.Chart("chartX10", {
    title:{
      text: "choice recognizer x10"
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
  let name = '';
  let dataPoints = [];

  x100.forEach((d, i) => {
      const method = d[0], lang = d[41], mean = d[44];
      name = method;
      dataPoints.push({
        y: parseFloat(mean, 10), label: lang
      });
  });

  data.push({
    type: "spline",
    name: name,
    showInLegend: true,
    dataPoints: dataPoints
  });

  const chartX100 = new CanvasJS.Chart("chartX100", {
    title:{
      text: "choice recognizer x100"
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

export default ChoiceRecognizer;
