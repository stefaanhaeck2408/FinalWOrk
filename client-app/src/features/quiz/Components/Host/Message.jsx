import React, { Component } from 'react'
import {
    Button,
    Input,
  } from "semantic-ui-react";

export class Message extends Component {

    state = {
        message : "",
    }

    handleChangeMessage = (event) => {
        this.setState({message : event.target.value})
    }

    sendMessage = () => {
        this.props.sendMessage(this.state.message);
    }
    render() {
        return (
            <div>
                <Input
                            type="text"
                            onChange={this.handleChangeMessage}
                            required
                            value={this.state.message}
                            style={{width: "500px", paddingRight:"10px"}}
                          ></Input>
                <Button onClick={this.sendMessage}>Send message to teams in Quiz</Button>
            </div>
        )
    }
}

export default Message
