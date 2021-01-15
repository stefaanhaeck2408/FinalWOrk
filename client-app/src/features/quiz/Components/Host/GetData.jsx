import React, { Component } from "react";
import axios from "axios";
import {
  Button,
  Dropdown,

} from "semantic-ui-react";

export class GetData extends Component {
  state = {
    quizes: [],
    selectedQuiz: null,
    selectedRoud: null,
    rounds: []
  };

  //Get the selected quiz
  quizSelected = (event, data) => {
    this.setState({ selectedQuiz: data.value });
    console.log("selected quiz id " + data.value);
    this.props.setQuiz(data.value);

    this.getAllRounds(data.value);
  };

  //Get the selected round
  roundSelected = (event, data) => {
    this.props.selectRound(data.value);
    this.setState({ selectedRound: data.value });
    console.log("selected round id " + data.value);
  };
  //Getting all rounds if a quiz is selected
  getAllQuizes = () => {
    fetch(
      "https://makeaquizapi.azurewebsites.net/api/Quiz/GetAllQuizesForOneUser/" +
        this.props.user.email
    )
      .then((res) => res.json())
      .then(
        (result) => {
          let newArray = [];
          result.forEach((quiz) => {
            const newValue = {
              key: quiz.naam,
              text: quiz.naam,
              value: quiz.id,
            };
            newArray = [...newArray, newValue];
          });
          this.setState({
            isLoaded: true,
            quizes: newArray,
          });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error,
          });
        }
      );
  };

  getAllRounds = (quizId) => {
    if (quizId != null) {
      console.log("Getting all rounds for quiz " + quizId);

      fetch("https://makeaquizapi.azurewebsites.net/api/Ronde/GetAllRondesInAQuiz/" + quizId)
        .then((res) => res.json())
        .then((result) => {
          let newArray = [];
          result.forEach((round) => {
            const newValue = {
              key: round.id,
              text: round.naam,
              value: round.id,
            };
            newArray = [...newArray, newValue];
          });
          this.setState({
            isLoaded: true,
            rounds: newArray,
          });
        });
      axios
        .get("https://makeaquizapi.azurewebsites.net/api/Quiz/GetAllTeamsFromQuiz/" + quizId)
        .then((res) => {
          let data = res.data.map((team) => {
            team.score = 0;
            team.questionsAnswered = 0;
            team.totalQuestionsOfQuiz = 0;
            team.maxScoreQuiz = 0;
            return team;
          });
          this.setState({ teams: data });
          this.props.setTeams(data);
          //console.log(data[0].naam + " testinggggg");
        });
    }
  };

  endRound = () => {
    let rounds = [...this.state.rounds];
    let roundFilter = { ...rounds.filter((x) => x.key === this.state.selectedRound) };

    this.props.endRound(roundFilter[0].text);

    this.setState({selectedRound: null})
  }

  render() {
    return (
      <div style={{color:"#61a3dc", background: "rgb(221 229 236)", borderRadius: "5px", fontStyle:"italic", padding:"5px", }}>   
        <div>
          {this.state.quizes.length === 0 ? (
            <Button
              size="massive"
              inverted
              color="blue"
              onClick={this.getAllQuizes}
            >
              Click here to start a quiz!
            </Button>
          ) : (
            <h4 style={{color:"#388cd4", fontStyle:"normal", padding:"0px", fontWeight:"bold"}}>Instructions</h4>
          )}
          <div>
            {this.state.selectedQuiz != null ? "1.  You have selected " : ""}
            {this.state.quizes.length > 0 ? (
              <Dropdown
                placeholder="1. Select a quiz to start here!"
                inline
                options={this.state.quizes}
                onChange={this.quizSelected}
              />
            ) : (
              <div />
            )}
          </div>

          <div>
          {this.state.selectedQuiz != null ? "2.  You have selected round " : ""}
            {this.state.selectedQuiz != null ? (
              <Dropdown
                placeholder="Select your round"
                inline
                options={this.state.rounds}
                onChange={this.roundSelected}
              />
            ) : (
              <div />
            )}
          </div>
          <div>
          {this.state.selectedRound != null ? "3.  " : ""}
            {this.state.selectedRound != null ? (
              <Button size="mini" onClick={this.props.getAllQuestions}>
                Get all questions for this round
              </Button>
            ) : (
              <div />
            )}
          </div>
          <div>
            {this.props.questions === true ? "4.  Send the questions for this round" : ""}
          </div>
          <div>
          {this.props.questions === true ? "5.  " : ""}
            {this.props.questions === true ? (
              <Button size="mini" onClick={this.endRound}>
                End this round and send scoreboard to the teams
              </Button>
            ) : (
              <div />
            )}
          </div>
          {this.props.questions === true ? "6.  Repeat steps 2,3,4,5 for each round you want to play" : ""}
        </div>
      </div>
    );
  }
}

export default GetData;
