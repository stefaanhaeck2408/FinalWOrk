import React, { Component } from "react";
import {Button} from "semantic-ui-react";

export class LoginNeeded extends Component {
  render() {
    const divstyle = {
        padding: 9,
        background: "#e0dcdc",
        marginBottom: "5px",
        color: "black",
        borderRadius: "5px",
        boxShadow: "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)",
        width: "50%",
        textAlign: "center"
    };
    return <div style={ divstyle }>
        <h2>You need to login before this page is visible</h2>
        <Button
              size="massive"
              inverted
              color="red"
              onClick={this.props.redirect}
              style={{}}
            >
              Click here to login!
            </Button>
    </div>;
  }
}

export default LoginNeeded;
