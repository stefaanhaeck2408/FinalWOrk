import React, { Component } from "react";
import { Button } from "semantic-ui-react";

export class Questions extends Component {
  state = {
    questions: [],
    selectedQuestion: null,
    selectedRound: null,
    totalPointsQuiz: 0,
  };

  //Sending the question to the players playing the quiz
  sendQuestion = (event) => {
    let question = this.props.questions.filter(
      (question) => question.id == event.target.value
    );

    console.log(question[0]);

    //Put the send question in the state as selectedQuestion
    this.setState(
      {
        selectedQuestion: question,
      },
      function () {
        this.props.hubConnection
          .invoke(
            "SendQuestion",
            this.state.selectedQuestion[0],
            this.props.selectedQuiz
          )
          .catch((err) => console.error(err));
      }
    );

    this.props.updateScore(question);
  };

  render() {
    return (
      <div>

          <h4 style={{color:"rgb(56 140 212)" , fontWeight:"bold"}}>
            Questions for this round
          </h4>
            <div
              style={{
                background: "rgb(221 229 236)",
                padding: 10,
                color: "white",
                borderRadius: "5px",
              }}
            >
              <div>
                {this.props.questions.map((question, index) => (
                  <div key={index}>
                    <div
                      style={{
                        padding: 9,
                        background: "#e0dcdc",
                        marginBottom: "5px",
                        color: "black",
                        borderRadius: "5px",
                        boxShadow: "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)"
                      }}
                      key={question.id}
                    >
                      {question.vraagStelling}
                      <Button
                        size="mini"
                        key={question.id}
                        color="blue"
                        value={question.id}
                        onClick={this.sendQuestion}
                        floated="right"
                        style={{
                          display: "block",
                          marginLeft: "auto",
                          marginRight: 0,
                        }}
                      >
                        Send question
                      </Button>
                    </div>
                  </div>
                ))}
              </div>
            </div>
      </div>
    );
  }
}

export default Questions;
