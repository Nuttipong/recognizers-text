import React from 'react';
import { Route, IndexRoute } from 'react-router';
import Layout from './components/Layout';
import NumberRecognizer from './components/numberRecognizer/NumberRecognizer';
import AboutPage from './components/about/AboutPage';
import CoursesPage from './components/course/CoursesPage';
import ManageCoursePage from './components/course/ManageCoursePage'; //eslint-disable-line import/no-named-as-default

export default (
  <Route path="/" component={Layout}>
    <IndexRoute component={NumberRecognizer} />
    <Route path="about" component={AboutPage} />
    <Route path="courses" component={CoursesPage} />
    <Route path="course" component={ManageCoursePage} />
    <Route path="course/:id" component={ManageCoursePage} />
  </Route>
);
