import React, { Component } from "react";
import QuestionTypeVraagId3 from "./QuestionTypeVraagId3";
import { Grid } from "semantic-ui-react";

export class TypeVraagId3 extends Component {
  render() {
    const divStyle = {
      borderRadius: "5px",
      width: "75%",
      display: "block",
      margin: "0 auto",
      height: "60px",
      color: "rgb(117 119 117)",
    };

    const itemStyle = {
      background: "rgb(213 229 252)",
      borderRadius: "5px",
      margin: "5px",
      //marginLeft: "125px",
      //marginRight: "125px",
      color: "#193670",
      paddingLeft: "25px",
      height: "25px",
      fontSize: "18px",
    };
    return (
      <div style={divStyle}>
        <Grid columns={2}>
          <Grid.Column>
            {this.props.array.map((answer, index) => (
              <div
                style={itemStyle}
                key={index}
                onClick={() => this.props.setPosition(answer)}
              >
                {answer}
              </div>
            ))}
            <hr></hr>
          </Grid.Column>
          <Grid.Column>
            <QuestionTypeVraagId3
              positions={this.props.positions}
              resetPosition={this.props.resetPosition}
              submitAnswer={this.props.submitAnswer}
            />
          </Grid.Column>
        </Grid>
      </div>
    );
  }
}

export default TypeVraagId3;
