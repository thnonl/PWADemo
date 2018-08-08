import React, { Component } from 'react'
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'
import { actionCreators } from '../store/Products'
import { FormGroup, ControlLabel, FormControl, Button, Grid, Row, Col } from 'react-bootstrap'
import styled from 'styled-components'

class ProductList extends Component {
  constructor () {
    super()
    this.handleSubmit = this.handleSubmit.bind(this)
    this.getProduct = this.getProduct.bind(this)
  }

  componentWillMount () {
    // This method runs when the component is first added to the page
    this.props.requestProducts()
  }

  componentWillReceiveProps (nextProps) {
    // This method runs when incoming props (e.g., route params) change
  }

  handleSubmit (event) {
    const data = new FormData(event.target)
    this.props.addProduct(data)
    event.preventDefault()
  }

  getProduct (event) {
    this.props.getProduct(event.target.text)
    event.preventDefault()
  }

  deleteProduct (id) {
    this.props.deleteProduct(id);
  }

  render () {
    return (
      <div>
        <h1>Products</h1>
        <p>
          This component demonstrates fetching data from the server and working with URL parameters.
        </p>
        {/* {renderEditForm(this.handleSubmit)} */}
        <form onSubmit={this.handleSubmit}>
          <Grid fluid>
            <Row>
              <Col sm={5} smOffset={3}>
              <BorderForm>
                <FormControl type='hidden' name='id' />
                <FormGroup>
                  <ControlLabel>
                    Title
                  </ControlLabel>
                  <FormControl
                    type='text'
                    required={true}
                    name='title'
                    placeholder='Enter title' />
                </FormGroup>
                <FormGroup>
                  <ControlLabel>
                    Description
                  </ControlLabel>
                  <FormControl componentClass='textarea' name='description' placeholder='Enter description' />
                </FormGroup>
                <FormGroup>
                  <ControlLabel>
                    Price
                  </ControlLabel>
                  <FormControl type='number' placeholder='Enter price' name='price' />
                </FormGroup>
                <Button type='submit'>
                  Submit
                </Button>
              </BorderForm>
              </Col>
            </Row>
          </Grid>
        </form>
        <table className='table'>
          <thead>
            <tr>
              <th>
                Product Id
              </th>
              <th>
                Title
              </th>
              <th>
                Description
              </th>
              <th>
                Price
              </th>
              <th>
                Created On
              </th>
              <th>
                Updated At
              </th>
              <th>
                Action
              </th>
            </tr>
          </thead>
          <tbody>
            {this.props.products.map(prod => <tr key={prod.id}>
                                               <td>
                                                 {prod.id}
                                               </td>
                                               <td>
                                                 {prod.title}
                                               </td>
                                               <td>
                                                 {prod.description}
                                               </td>
                                               <td>
                                                 {prod.price}
                                               </td>
                                               <td>
                                                 {(new Date(prod.createdOn)).toLocaleDateString()}
                                               </td>
                                               <td>
                                                 {prod.updatedAt ? (new Date(prod.updatedAt)).toLocaleDateString() : ''}
                                               </td>
                                               <td>
                                                 <Button onClick={() => this.deleteProduct(prod.id)}>
                                                   Delete
                                                 </Button>
                                               </td>
                                             </tr>
             )}
          </tbody>
        </table>
        {renderPagination(this.props)}
      </div>
    )
  }
}

function renderPagination (props) {
  return <p className='clearfix text-center'>
           {/* <Link className='btn btn-default pull-left'>Previous</Link> */}
           {/* <Link className='btn btn-default pull-right'>Next</Link> */}
           {props.isLoading ? <span>Loading...</span> : []}
         </p>
}

const BorderForm =
styled.div`
          margin-bottom: 20px
        `

export default connect(
  state => state.products,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(ProductList)
