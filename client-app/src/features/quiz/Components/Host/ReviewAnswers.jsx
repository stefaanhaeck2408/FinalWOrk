import React, { Component } from "react";
import {
  Button,
} from "semantic-ui-react";

export class ReviewAnswers extends Component {
  answerReview = (answer, verdict) => {
    this.props.answerReview(answer, verdict);
  };

  render() {
    return (
      <div>
        <h4 style={{ color: "rgb(56 140 212)", fontWeight:"bold" }}>
          Answers that need to be reviewed
        </h4>
        <div
          style={
            {
              /*background: "#333",
            padding: 10,
            color: "white",
            borderRadius: "5px",*/
            }
          }
        >
          {this.props.answersUnderReview.map((question, index) => (
            <div
              style={{
                padding: 20,
                background: "rgb(231 239 247)",
                marginBottom: "5px",
                color: "black",
                borderRadius: "5px",
                boxShadow:
                  "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)",
                  margin: "10px"
              }}
              key={index}
            >
              <p style={{fontWeight:"bold"}}>Question: {this.props.selectedQuestion[0].vraagStelling}</p>
              <p style={{fontStyle:"italic"}}>
                The correct answer:{" "}
                {this.props.selectedQuestion[0].jsonCorrecteAntwoord}
              </p>
            <p style={{fontStyle:"italic"}}>{"The answer from team: "}{question.answer}</p>
              <div
                style={{
                  display: "block",
                  marginLeft: "auto",
                  marginRight: 0,
                }}
              >
                <Button
                  size="mini"
                  color="green"
                  onClick={() => this.answerReview(question, true)}
                >
                  Correct
                </Button>
                <Button
                  size="mini"
                  color="red"
                  onClick={() => this.answerReview(question, false)}
                >
                  Wrong
                </Button>
              </div>
            </div>
          ))}
        </div>
      </div>
    );
  }
}

export default ReviewAnswers;
