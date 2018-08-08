const REQUEST_PRODUCTS_TYPE = 'REQUEST_PRODUCTS';
const RECEIVE_PRODUCTS_TYPE = 'RECEIVE_PRODUCTS';
const GET_PRODUCT_TYPE = 'GET_PRODUCT';
const ADD_PRODUCT_TYPE = 'ADD_PRODUCT';
const UPDATE_PRODUCT_TYPE = 'UPDATE_PRODUCT';
const DELETE_PRODUCT_TYPE = 'DELETE_PRODUCT';
const initialState = { products: [], isLoading: false };

export const actionCreators = {
  requestProducts,
  addProduct,
  getProduct,
  deleteProduct
};

function requestProducts() {
  return async dispatch => {
    dispatch({ type: REQUEST_PRODUCTS_TYPE });

    const url = `api/Products/`;
    const response = await fetch(url);
    const products = await response.json();

    dispatch({ type: RECEIVE_PRODUCTS_TYPE, products });
  }
}

function getProduct(id) {
  return async dispatch => {
    const url = `api/Products/` + id;
    const response = await fetch(url);
    const product = await response.json();

    dispatch({ type: GET_PRODUCT_TYPE, product });
  }
}

function deleteProduct(id) {
  return async dispatch => {
    dispatch({ type: DELETE_PRODUCT_TYPE });
      const url = `api/Products/` + id;
      fetch(url,
      {
        method: 'DELETE'
      }).then(async function() {
        const url1 = `api/Products/`;
        const response = await fetch(url1);
        const products = await response.json();
  
        dispatch({ type: RECEIVE_PRODUCTS_TYPE, products });
      });
  }
}

function addProduct(product) {
  return async dispatch => {
    console.log(product.get('id'))
    if (product.get('id') == 0 || product.get('id') == '') {
      dispatch({ type: ADD_PRODUCT_TYPE, product });
      const url = `api/Products/Create`;
      fetch(url,
      {
        method: 'POST',
        body: product
      }).then(async function() {
        const url1 = `api/Products/`;
        const response = await fetch(url1);
        const products = await response.json();
  
        dispatch({ type: RECEIVE_PRODUCTS_TYPE, products });
      });
    }
    else {
      dispatch({ type: UPDATE_PRODUCT_TYPE, product });
      const url = `api/Products/` + product.id;
      fetch(url,
      {
        method: 'PUT',
        body: product
      }).then(async function() {
        const url1 = `api/Products/`;
        const response = await fetch(url1);
        const products = await response.json();
  
        dispatch({ type: RECEIVE_PRODUCTS_TYPE, products });
      });
    }
  }
}

export const reducer = (state, action) => {
  state = state || initialState;

  switch (action.type) {
    case REQUEST_PRODUCTS_TYPE:
      return {
        ...state,
        isLoading: true
      };
    case RECEIVE_PRODUCTS_TYPE:
      return {
        ...state,
        products: action.products,
        isLoading: false
      };
    case ADD_PRODUCT_TYPE:
      return {
        ...state
      };
    case GET_PRODUCT_TYPE:
      return {
        ...state,
        current: action.product
      };
    case DELETE_PRODUCT_TYPE:
      return {
        ...state
      };
    default: return { ...state, isLoading: false };
  }
  return state;
};
