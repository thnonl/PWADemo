import React from 'react'
import { connect } from 'react-redux'
import { Field, reduxForm } from 'redux-form'
import { actionCreators } from '../store/Products'
import { bindActionCreators } from 'redux'
import { FormGroup, ControlLabel, FormControl, Button, Grid, Row, Col } from 'react-bootstrap'
import styled from 'styled-components'

let ProductForm = props => {
  const { handleSubmit, current } = props
  return (
    <form onSubmit={handleSubmit}>
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
  )
}

const BorderForm =
styled.div`
          margin-bottom: 20px
        `

// Decorate with reduxForm(). It will read the initialValues prop provided by connect()
ProductForm = reduxForm({
  form: 'ProductForm',
  fields: ['id', 'title', 'description', 'number']
})(ProductForm)

// You have to connect() to any reducers that you wish to connect to yourself
ProductForm = connect(
  state => ({
    initialValues: state.current
  })
)(ProductForm)

export default ProductForm
