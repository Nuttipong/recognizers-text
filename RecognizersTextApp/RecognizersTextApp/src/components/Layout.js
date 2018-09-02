import React, {PropTypes} from 'react';
import {connect} from 'react-redux';
import Header from './common/Header';
import {Link} from 'react-router';

class Layout extends React.Component {
  render() {
    return (
      <div className="container">
        <Header
          loading={this.props.loading}
        />
        <div className="row">
          <div className="col-sm-2">
            <ul className="nav flex-column">
              <li className="nav-item"><Link to="" className="nav-link active">Number</Link></li>
              <li className="nav-item"><Link to="number-with-unit">NumberWithUnit</Link></li>
              <li className="nav-item"><Link to="datetime">DateTime</Link></li>
              <li className="nav-item"><Link to="choice">Choice</Link></li>
              <li className="nav-item"><Link to="sequence">Sequence</Link></li>
            </ul>
          </div>
          <div className="col-sm-10">
              {this.props.children}
          </div>
        </div>
      </div>
    );
  }
}

Layout.propTypes = {
  children: PropTypes.object.isRequired,
  loading: PropTypes.bool.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    loading: state.ajaxCallsInProgress > 0
  };
}

export default connect(mapStateToProps)(Layout);
