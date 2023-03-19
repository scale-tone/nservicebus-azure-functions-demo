import React from 'react';

export default class App extends React.Component {

    render(): JSX.Element {

        const uri = `${process.env.REACT_APP_BACKEND_BASE_URI}/PlaceOrder`;

        return (<button onClick={() => {
            fetch(uri)
                .then(response => response.json())
                .then(order => alert(`Order ${order.orderId} sent`), err => alert(`Failed to send an order. ${err.message ?? err}`))
        }}>
            Place Order
        </button>);
    }
}
