import {combineReducers} from 'redux';
import ajaxCallsInProgress from './ajaxStatusReducer';
import courses from './courseReducer';
import authors from './authorReducer';

const rootReducer = combineReducers({
    ajaxCallsInProgress,
    courses,
    authors
});

export default rootReducer;
