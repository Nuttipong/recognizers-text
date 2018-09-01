/* eslint-disable import/default */
import React from 'react';
import {render} from 'react-dom';
import {Provider} from 'react-redux';
import { Router, browserHistory} from 'react-router';
import routes from './Routes';
import configureStore from './store/configureStore';
import {loadCourses} from './actions/courseActions';
import {loadAuthors} from './actions/authorActions';

const store = configureStore();
store.dispatch(loadCourses());
store.dispatch(loadAuthors());

render(
    <Provider store={store}>
        <Router history={browserHistory} routes={routes}></Router>
    </Provider>,
    document.getElementById('app')
);
