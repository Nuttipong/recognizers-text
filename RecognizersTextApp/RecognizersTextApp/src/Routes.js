import React from 'react';
import { Route, IndexRoute } from 'react-router';
import Layout from './components/Layout';
import NumberRecognizer from './components/number/NumberRecognizer';
import NumberWithUnitRecognizer from './components/numberWithUnit/NumberWithUnitRecognizer';
import ChoiceRecognizer from './components/choice/ChoiceRecognizer';
import DateTimeRecognizer from './components/datetime/DateTimeRecognizer';
import SequenceRecognizer from './components/sequence/SequenceRecognizer';

export default (
  <Route path="/" component={Layout}>
    <IndexRoute component={NumberRecognizer} />
    <Route path="number-with-unit" component={NumberWithUnitRecognizer} />
    <Route path="choice" component={ChoiceRecognizer} />
    <Route path="datetime" component={DateTimeRecognizer} />
    <Route path="sequence" component={SequenceRecognizer} />
  </Route>
);
