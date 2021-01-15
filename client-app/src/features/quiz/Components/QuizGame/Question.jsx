import React, { Component } from "react";
import TypeVraagId1 from "./TypeVraagId1";
import TypeVraagId2 from "./TypeVraagId2";
import TypeVraagId3 from "./TypeVraagId3";
import axios from 'axios';

export class Question extends Component {
  state = {
    time: 0,
    normalStyle: true,
  };

  componentWillUnmount = () => {
    clearInterval(this.timerID);
  }

  mouseEnter = () => {
    if(!this.props.receiveScoreboard){
      if(!this.state.userLeftGame){
        clearInterval(this.timerID);
        console.log("mouse entered the div at " + new Date().toLocaleTimeString());
        this.setState({ time: null, normalStyle: true});
        this.props.mouseLeftDiv(false);
      }
    }    
  };

  mouseLeave = () => {
    if(!this.props.receiveScoreboard){
      this.timerID = setInterval(() => this.tick(), 1000);
      this.props.mouseLeftDiv(true);
      console.log("the mouse left the div");
      this.setState({normalStyle: false})
    }    
  };

  tick() {
    if(!this.props.receiveScoreboard){
      if (this.state.time > 3) {
        this.setState({ userLeftGame: true });
        this.props.userLeftGame();
        clearInterval(this.timerID);
  
        var teamThatHasBeenKickedOut = this.props.selectedTeam;
        teamThatHasBeenKickedOut.PIN = "";
        axios.put("https://makeaquizapi.azurewebsites.net/api/Team/Update/", teamThatHasBeenKickedOut );
      } else {
        let newTime = this.state.time;
        this.setState({
          time: ++newTime
        });
      }
    }else{
      clearInterval(this.timerID);
    }    
  }

  typeAnswer = (question) => {
    if (question != null) {
      if (question.typeVraagId === 1) {
        return <TypeVraagId1 submitAnswer={this.props.submitAnswer} />;
      }
      if (question.typeVraagId === 2) {
        var answersArray = question.jsonMogelijkeAntwoorden.split(",");
        var allAnswersArray = [...answersArray, question.jsonCorrecteAntwoord];
        var arrayShuffle = this.shuffleArray(allAnswersArray);

        return (
          <TypeVraagId2
            array={arrayShuffle}
            submitAnswer={this.props.submitAnswer}
          />
        );
      }
      if (question.typeVraagId === 3) {
        var array = question.jsonCorrecteAntwoord.split(",");

        arrayShuffle = this.shuffleArray(array);

        return (
          <TypeVraagId3
            array={arrayShuffle}
            setPosition={this.props.setPosition}
            positions={this.props.positions}
            resetPosition={this.props.resetPosition}
            submitAnswer={this.props.submitAnswer}
          />
        );
      }
    }

    return <p>Geen vraag</p>;
  };

  shuffleArray = (array) => {
    for (let i = array.length - 1; i > 0; i--) {
      let j = Math.floor(Math.random() * (i + 1));
      [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
  };

  render() {    
    var backgroundImage = {
      backgroundImage: `url(${require("../../../../img/audience.jpg")})`,
      backgroundSize: "cover",
      backgroundRepeat: "no-repeat",
      position: "absolute",
      backgroundPosition: "center",
      height: "60%",
      width: "60%",
      borderStyle: "solid",
      borderRadius: "5px",
      borderColor: "#7cadf2",
      borderWidth: "4px",
    };
    if (!this.state.normalStyle) {
      backgroundImage = {
        backgroundImage: `url(${require("../../../../img/audience.jpg")})`,
        backgroundSize: "cover",
        backgroundRepeat: "no-repeat",
        position: "absolute",
        backgroundPosition: "center",
        height: "60%",
        width: "60%",
        borderStyle: "solid",
        borderRadius: "5px",
        borderColor: "Red",
        borderWidth: "4px",
      };
    }

    return (
      <div>
        <div
          style={backgroundImage}
          onMouseEnter={this.mouseEnter}
          onMouseLeave={this.mouseLeave}
        >
          {this.state.userLeftGame ? (
            <div>Error user left game</div>
          ) : (
            <div>
              {this.props.question != null ? (
                <div>
                  <h2
                    style={{
                      textAlign: "center",
                      marginTop: "50px",
                      marginBottom: "30px",
                      fontFamily: "Dancing Script",
                      fontSize: "40px",
                      color: "#193670",
                    }}
                  >
                    {this.props.question.vraagStelling}
                  </h2>
                  {this.typeAnswer(this.props.question)}
                </div>
              ) : (
                <div>No question</div>
              )}
            </div>
          )}
        </div>
        {this.state.time != null ? (
              <div>{this.state.time.toString()}</div>
            ) : (
              <div>Mouse is in the div</div>
            )}
      </div>
    );
  }
}

export default Question;
