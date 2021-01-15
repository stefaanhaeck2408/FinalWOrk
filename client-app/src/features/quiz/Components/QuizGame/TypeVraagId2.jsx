import React, { Component } from "react";
import MultipleChoiseItem from "./MultipleChoiseItem.jsx";

export class TypeVraagId2 extends Component {
  state = {
    selection: null,
  };

  selectAnswer = (e, answer) => {
    this.setState({ event: e, selection: answer });
    //this.props.submitAnswer(e,answer);
  };

  submitAnswer = () => {
    this.props.submitAnswer(this.state.event, this.state.selection);
  };

  render() {
    const answerSelectedStyle = {
      color: "#193670",
      marginLeft: "13%",
      fontWeight: "bold",
      display: "inline-block",
      margin: 0,
      width: "400px",
      background: "white",
      borderRadius: "3px",
      padding: "2px",
    };

    const buttonStyle = {
      float: "right",
      marginRight: "200px",
      display: "block",
      marginTop: "10px",
      boxShadow: "0px 0px 16px #193670",
      transition: "all 1s ease-in-out",
      width: "100px",
      height: "25px",
      background: "#eaf3fa",
      borderRadius: "2px",
      zIndex: 3,
      border: "2px solid",
      borderImage: "linear-gradient(#193670, #7cadf2) 25",
      textAlign: "center",
      fontFamily: "Sacremento",
      left: "56px",
      fontSize: "18px",
      lineHeight: "25px",
      color: "#193670",
    };
    return (
      <div>
        {this.props.array.map((answer, i) => (
          <MultipleChoiseItem
            key={i}
            value={answer}
            answer={answer}
            answerSelected={this.selectAnswer}
          ></MultipleChoiseItem>
        ))}
        {this.state.selection != null ? (
          <div>
            <p style={answerSelectedStyle}>
              you have selected the answer: {this.state.selection}{" "}
            </p>{" "}
            <button style={buttonStyle} onClick={this.submitAnswer}>
              Submit!
            </button>
          </div>
        ) : (
          <p />
        )}
      </div>
    );
  }
}

export default TypeVraagId2;
