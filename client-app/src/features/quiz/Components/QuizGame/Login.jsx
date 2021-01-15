import React, { Component } from "react";
import { Form, Input } from "semantic-ui-react";

export class Login extends Component {
  state = {
    password: "",
  };
  render() {
      var divStyle = {
        background: "#41c4de",
        height: "300px",
        width: "500px",
        borderStyle: "outset",
        borderWidth: "10px",
        borderColor: "white",
        boxShadow:
          "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)",
      }

    const formStyle = {
      paddingLeft: "20%",
      paddingRight: "20%",
      paddingBottom: "20px",
    };
    return (
      <div
        style={divStyle}
        
      >
        
          <div>
            <h1
              style={{
                color: "rgb(234 234 234)",
                textAlign: "center",
                padding: "25px",
                fontFamily: "Dancing Script",
              }}
            >
              Play a quiz!
            </h1>
            <Form
              onSubmit={(e) => this.props.login(e, this.state.password)}
              style={formStyle}
            >
              <Form.Field>
                <Input
                  type="password"
                  placeholder="Team PIN"
                  onChange={(e) => this.setState({ password: e.target.value })}
                ></Input>
              </Form.Field>
              <Form.Field>
                <Input
                  style={{ textAlign: "center" }}
                  type="submit"
                  value="Submit"
                ></Input>
              </Form.Field>
            </Form>            
          </div>
  
      </div>
    );
  }
}

export default Login;
