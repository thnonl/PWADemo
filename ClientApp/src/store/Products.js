const REQUEST_PRODUCTS_TYPE = 'REQUEST_PRODUCTS';
const RECEIVE_PRODUCTS_TYPE = 'RECEIVE_PRODUCTS';
const ADD_PRODUCT_TYPE = 'ADD_PRODUCT';
const initialState = { products: [], isLoading: false };

export const actionCreators = {
  requestProducts,
  addProduct
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

function addProduct(product) {
  return async dispatch => {
    dispatch({ type: ADD_PRODUCT_TYPE, product });

    const url = `api/Products/Create`;
    fetch(url,
    {
      method: 'POST',
      body: product
    });
    await requestProducts();
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
    default: return { ...state, isLoading: false };
  }
  return state;
};
