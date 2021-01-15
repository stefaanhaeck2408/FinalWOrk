import React, { Component } from "react";

export class TypeVraagId1 extends Component {
  state = {
    answerTypeVraag1: "",
    hover: false,
  };

  toggleHover = () => {
    this.setState({ hover: !this.state.hover });
  };

  render() {
    var buttonStyle;
    if (this.state.hover) {
      buttonStyle = {
        display: "block",
        margin: "0 auto",
        marginTop: "10px",
        width: "171px",
        height: "50px",
        background: "#eaf3fa",
        borderRadius: "2px",
        zIndex: 3,
        border: "2px solid",
        borderImage: "linear-gradient(#193670, #7cadf2) 20",
        textAlign: "center",
        fontFamily: "Sacremento",
        left: "56px",
        fontSize: "35px",
        lineHeight: "50px",
        color: "#193670",
        bottom: "176px",
        transition: "all 1s ease-in-out",
        boxShadow: "0px 0px 36px #193670",
      };
    } else {
      buttonStyle = {
        display: "block",
        margin: "0 auto",
        marginTop: "10px",
        boxShadow: "0px 0px 16px #193670",
        transition: "all 1s ease-in-out",
        width: "171px",
        height: "50px",
        background: "#eaf3fa",
        borderRadius: "2px",
        zIndex: 3,
        border: "2px solid",
        borderImage: "linear-gradient(#193670, #7cadf2) 20",
        textAlign: "center",
        fontFamily: "Sacremento",
        left: "56px",
        fontSize: "35px",
        lineHeight: "50px",
        color: "#193670",
      };
    }
    return (
      <div>
        <form
          onSubmit={(e) =>
            this.props.submitAnswer(e, this.state.answerTypeVraag1)
          }
        >
          <input
            style={{
              borderRadius: "5px",
              width: "75%",
              display: "block",
              margin: "0 auto",
              height: "60px",
              fontSize: "30px",
              color: "rgb(117 119 117)"
            }}
            type="text"
            placeholder="Your answer..."
            value={this.state.answerTypeVraag1}
            onChange={(e) =>
              this.setState({ answerTypeVraag1: e.target.value })
            }
          ></input>
          <input
            style={buttonStyle}
            onMouseEnter={this.toggleHover}
            onMouseLeave={this.toggleHover}
            type="submit"
            value="Submit"
          />
        </form>
      </div>
    );
  }
}

export default TypeVraagId1;
