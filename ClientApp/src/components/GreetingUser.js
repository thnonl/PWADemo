import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Counter';

const GreetingUser = props => (
  <div>
    <span>Hello, {props.user.username}</span>
  </div>
);

export default connect(
  state => state.user,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Counter);
