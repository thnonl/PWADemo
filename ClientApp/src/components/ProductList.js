import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/WeatherForecasts';
import { FormGroup, ControlLabel, FormControl, Button } from 'react-bootstrap';

class ProductList extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
  }

  componentWillReceiveProps(nextProps) {
    // This method runs when incoming props (e.g., route params) change
  }

  render() {
    return (
      <div>
        <h1>Products</h1>
        <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
        {renderEditForm(this.props)}
        {renderProductTable(this.props)}
        {renderPagination(this.props)}
      </div>
    );
  }
}

function renderProductTable(props) {
  return (
    <table className='table'>
      <thead>
        <tr>
          <th>Product Id</th>
          <th>Title</th>
          <th>Description</th>
          <th>Price</th>
          <th>Created On</th>
          <th>Updated At</th>
        </tr>
      </thead>
      <tbody>
        {props.products.map(prod =>
          <tr key={prod.productId}>
            <td>{prod.title}</td>
            <td>{prod.description}</td>
            <td>{prod.price}</td>
            <td>{prod.createdOn}</td>
            <td>{prod.updatedAt}</td>
          </tr>
        )}
      </tbody>
    </table>
  );
}

function renderPagination(props) {
  return <p className='clearfix text-center'>
    {/* <Link className='btn btn-default pull-left'>Previous</Link>
    <Link className='btn btn-default pull-right'>Next</Link> */}
    {props.isLoading ? <span>Loading...</span> : []}
  </p>;
}

function renderEditForm(props) {
  return (
    <form>
      <FormGroup>
        <ControlLabel>Title</ControlLabel>
        <FormControl
          type="text"
          value={props.title}
          placeholder="Enter title"
        />
        <ControlLabel>Description</ControlLabel>
        <FormControl
          componentClass="textarea"
          placeholder="Enter description"
          value={props.description}
        />
        <ControlLabel>Price</ControlLabel>
        <FormControl
          type="number"
          value={props.price}
          placeholder="Enter price"
        />
      </FormGroup>
      <Button type="button" onClick={submit()}>Submit</Button>
    </form>)
}

function submit() {
  
}

export default connect(
  state => state.products,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(ProductList);
