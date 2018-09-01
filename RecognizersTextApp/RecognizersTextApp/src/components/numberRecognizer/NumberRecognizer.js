import React from 'react';
 
class NumberRecognizer extends React.Component {

	componentDidMount() {
    const chartX1 = new CanvasJS.Chart("chartX1", {
      title:{
        text: "number recognizer x1"
      },
      axisY : {
        title: "number of execution time",
        includeZero: false
      },
      toolTip: {
        shared: true
      },
      data: [{
        type: "spline",
        name: "RecognizeNumber",
        showInLegend: true,
        dataPoints: [
          { y: 155, label: "Chinese" },
          { y: 150, label: "Dutch" },
          { y: 152, label: "English" },
          { y: 148, label: "French" },
          { y: 142, label: "German" },
          { y: 150, label: "Italian" },
          { y: 146, label: "Japanese" },
          { y: 149, label: "Korean" },
          { y: 153, label: "Portuguese" },
          { y: 158, label: "Spanish" },
        ]
      },
      {
        type: "spline",
        name: "RecognizeOrdinal",
        showInLegend: true,
        dataPoints: [
          { y: 155, label: "Chinese" },
          { y: 150, label: "Dutch" },
          { y: 152, label: "English" },
          { y: 148, label: "French" },
          { y: 142, label: "German" },
          { y: 150, label: "Italian" },
          { y: 146, label: "Japanese" },
          { y: 149, label: "Korean" },
          { y: 153, label: "Portuguese" },
          { y: 158, label: "Spanish" },
        ]
      },
      {
        type: "spline",
        name: "RecognizePercentage",
        showInLegend: true,
        dataPoints: [
          { y: 155, label: "Chinese" },
          { y: 150, label: "Dutch" },
          { y: 152, label: "English" },
          { y: 148, label: "French" },
          { y: 142, label: "German" },
          { y: 150, label: "Italian" },
          { y: 146, label: "Japanese" },
          { y: 149, label: "Korean" },
          { y: 153, label: "Portuguese" },
          { y: 158, label: "Spanish" },
        ]
      },
      {
        type: "spline",
        name: "RecognizeNumberRange",
        showInLegend: true,
        dataPoints: [
          { y: 172, label: "Chinese" },
          { y: 173, label: "Dutch" },
          { y: 175, label: "English" },
          { y: 172, label: "French" },
          { y: 162, label: "German" },
          { y: 165, label: "Italian" },
          { y: 172, label: "Japanese" },
          { y: 168, label: "Korean" },
          { y: 175, label: "Portuguese" },
          { y: 170, label: "Spanish" },
        ]
      }]
    });
    chartX1.render();
    const chartX10 = new CanvasJS.Chart("chartX10", {
      title:{
        text: "number recognizer x10"
      },
      axisY : {
        title: "number of execution time",
        includeZero: false
      },
      toolTip: {
        shared: true
      },
      data: [{
        type: "spline",
        name: "RecognizeNumber",
        showInLegend: true,
        dataPoints: [
          { y: 155, label: "Chinese" },
          { y: 150, label: "Dutch" },
          { y: 152, label: "English" },
          { y: 148, label: "French" },
          { y: 142, label: "German" },
          { y: 150, label: "Italian" },
          { y: 146, label: "Japanese" },
          { y: 149, label: "Korean" },
          { y: 153, label: "Portuguese" },
          { y: 158, label: "Spanish" },
        ]
      },
      {
        type: "spline",
        name: "RecognizeOrdinal",
        showInLegend: true,
        dataPoints: [
          { y: 155, label: "Chinese" },
          { y: 150, label: "Dutch" },
          { y: 152, label: "English" },
          { y: 148, label: "French" },
          { y: 142, label: "German" },
          { y: 150, label: "Italian" },
          { y: 146, label: "Japanese" },
          { y: 149, label: "Korean" },
          { y: 153, label: "Portuguese" },
          { y: 158, label: "Spanish" },
        ]
      },
      {
        type: "spline",
        name: "RecognizePercentage",
        showInLegend: true,
        dataPoints: [
          { y: 155, label: "Chinese" },
          { y: 150, label: "Dutch" },
          { y: 152, label: "English" },
          { y: 148, label: "French" },
          { y: 142, label: "German" },
          { y: 150, label: "Italian" },
          { y: 146, label: "Japanese" },
          { y: 149, label: "Korean" },
          { y: 153, label: "Portuguese" },
          { y: 158, label: "Spanish" },
        ]
      },
      {
        type: "spline",
        name: "RecognizeNumberRange",
        showInLegend: true,
        dataPoints: [
          { y: 172, label: "Chinese" },
          { y: 173, label: "Dutch" },
          { y: 175, label: "English" },
          { y: 172, label: "French" },
          { y: 162, label: "German" },
          { y: 165, label: "Italian" },
          { y: 172, label: "Japanese" },
          { y: 168, label: "Korean" },
          { y: 175, label: "Portuguese" },
          { y: 170, label: "Spanish" },
        ]
      }]
    });
    chartX10.render();
	}

  render() {
    return (
      <div>
        <div className="row" style={{marginBottom: '45%'}}>
          <div id="chartX1"></div>
        </div>
        <div className="row">
          <div id="chartX10"></div>
        </div>
      </div>
    );
  }
}

export default NumberRecognizer;
