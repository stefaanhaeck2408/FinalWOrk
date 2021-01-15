import React from "react";
import { ElementsConsumer, CardElement } from "@stripe/react-stripe-js";
import axios from "axios";
import { Message, Input, Button } from "semantic-ui-react";

import CardSection from "./CardSection";

class CheckoutForm extends React.Component {
  state = {
    message: null,
    errorMessage: null,
    name: ""
  }
  componentDidMount = () => {
    axios
      .get(
        "https://makeaquizapi.azurewebsites.net/api/Account/PaymentIntent?emailCreator=" +
          this.props.selectedQuiz.emailCreator +
          "&fee=" +
          this.props.selectedQuiz.entryFee
      )
      .then((result) => this.setState({ clientSecret: result.data }));
  };

  handleSubmit = async (event) => {
    // We don't want to let default form submission happen here,
    // which would refresh the page.
    event.preventDefault();

    const { stripe, elements } = this.props;

    if (!stripe || !elements) {
      // Stripe.js has not yet loaded.
      // Make  sure to disable form submission until Stripe.js has loaded.
      return;
    }

    const result = await stripe.confirmCardPayment(this.state.clientSecret, {
      payment_method: {
        card: elements.getElement(CardElement),
        billing_details: {
          name: this.state.name + " from team with name " + this.props.selectedTeam.naam,
        },
      },
    });

    if (result.error) {
      // Show error to your customer (e.g., insufficient funds)
      console.log(result.error.message);
      this.setState({errorMessage: "Payment failed because " + result.error.message});
    } else {
      // The payment has been processed!
      if (result.paymentIntent.status === "succeeded") {
        // Show a success message to your customer
        // There's a risk of the customer closing the window before callback
        // execution. Set up a webhook or plugin to listen for the
        // payment_intent.succeeded event that handles any business critical
        // post-payment actions.
        var newTeam = this.props.selectedTeam;
        newTeam.teamPaidAllready = true;
        axios
          .put("https://makeaquizapi.azurewebsites.net/api/Team/Update", newTeam)
          .then((result) =>
            result.status === 200
              ? this.props.setPaidAllready(true)
              : this.props.setPaidAllready(false)
          );
      }
    }
  };

  handleChangeName = (event) => {
    this.setState({name: event.target.value})
  }

  render() {
    return (
      <div>
        <Message color="green">
          Warning you still have to pay a fee of{" "}
          {this.props.selectedQuiz.entryFee}
          {"€ before you can start the quiz with name " +
            this.props.selectedQuiz.naam}
        </Message>
        <form onSubmit={this.handleSubmit}>
        <h5>Card holder information:</h5>
        <Input style={{display: "block"}} type="text" placeholder="Enter name here" onChange={this.handleChangeName} value={this.state.name}></Input>
        <CardSection style={{display: "block"}}  />
        <Button style={{display: "block"}}  disabled={!this.props.stripe}>Confirm order</Button>
      </form>
    {this.state.message != null ? <Message color="green">{this.state.message}</Message> : <div></div>}
    {this.state.errorMessage != null ? <Message color="red">{this.state.errorMessage}</Message> : <div></div>}
      </div>

      
    );
  }
}

export default function InjectedCheckoutForm(props) {
  var quiz = props.selectQuiz;
  var team = props.selectedTeam;
  var paidAllready = props.setPaidAllready;
  return (
    <ElementsConsumer>
      {({ stripe, elements }) => (
        <CheckoutForm
          stripe={stripe}
          elements={elements}
          selectedQuiz={quiz}
          selectedTeam={team}
          setPaidAllready={paidAllready}
        />
      )}
    </ElementsConsumer>
  );
}
