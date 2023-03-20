import React from 'react';

export default class App extends React.Component {

    render(): JSX.Element {

        return (<>
            
            <button onClick={() => this.placeOrder()}> Place Order </button>
            <br/><br/>
            <button onClick={() => this.placeDelayedOrder()}> Place Delayed Order </button>
            <br/><br/>
            <button onClick={() => this.placeFatalOrder()}> Place Fatal Order </button>
            
        </>);
    }

    private placeOrder() {

        fetch(`${process.env.REACT_APP_BACKEND_BASE_URI}/place-order`)
            .then(response => response.json())
            .then(order => alert(`Order ${order.orderId} sent`), err => alert(`Failed to send an order. ${err.message ?? err}`));
    }

    private placeDelayedOrder() {

        fetch(`${process.env.REACT_APP_BACKEND_BASE_URI}/place-delayed-order`)
            .then(response => response.json())
            .then(order => alert(`Delayed order ${order.orderId} sent`), err => alert(`Failed to send an order. ${err.message ?? err}`));
    }

    private placeFatalOrder() {

        fetch(`${process.env.REACT_APP_BACKEND_BASE_URI}/place-fatal-order`)
            .then(response => response.json())
            .then(order => alert(`Delayed order ${order.orderId} sent`), err => alert(`Failed to send an order. ${err.message ?? err}`));
    }
}
