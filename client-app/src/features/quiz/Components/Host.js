import React, { Component } from "react";
import * as signalR from "@aspnet/signalr";
import axios from "axios";
import { withAuth0 } from "@auth0/auth0-react";
import GetData from "../Components/Host/GetData.jsx";
import Questions from "../Components/Host/Questions.jsx";
import ScoreBoard from "./Host/ScoreBoard.jsx";
import ReviewAnswers from "../Components/Host/ReviewAnswers.jsx";
import History from "../Components/Host/History.jsx";
import Message from "../Components/Host/Message.jsx";
import LoginNeeded from "./LoginNeeded.jsx";
import {
  Grid,
  Segment,
} from "semantic-ui-react";

export class Host extends Component {
  constructor(props) {
    super(props);

    // state for running a quiz, teams, selectedQuiz, quizRounds, questions, answers and scoreboard
    this.state = {
      teams: [],
      totalPointsQuiz: 0,
      quizes: [],
      selectedQuiz: null,
      rounds: [],
      selectedRound: null,
      questions: [],
      message: "",
      selectedQuestion: null,
      messagesUnderReview: [],
      creator: null,
      history: [],
      countHistoryMessages: 0,
    };
  }

  componentWillRece;

  //When component is made
  componentDidMount = () => {
    const hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://makeaquizsignalr.azurewebsites.net/chatHub")
      .configureLogging(signalR.LogLevel.Trace)
      .build();

    this.setState({ hubConnection }, () => {
      this.state.hubConnection
        .start()
        .then(() => console.log("Connection started!"))
        .catch((err) =>
          console.log("error while establishing connection: " + err)
        );

      this.state.hubConnection.on("ReceiveUserLeftGame", (teamName, teamId) => {
        //Updating the history and deleting the team from the scoreboard
        let newCount = this.state.countHistoryMessages;
        this.setState({
          history: [
            ...this.state.history,
            {
              index: ++newCount,
              message: "Team with name " + teamName + " has left the quiz ",
              time: new Date().toLocaleTimeString(),
            },
          ],

          teams: this.state.teams.filter((team) => team.id !== teamId),
        });
      });

      //add a quiz filter to the answer so you just receive answers from this quiz
      this.state.hubConnection.on(
        "ReceiveAnswer",
        (team, receivedAnswer, teamId) => {
          console.log(
            team + " " + receivedAnswer + " from team with id " + teamId
          );

          if (this.state.teams.filter((team) => team.id === teamId).length > 0) {
            if (this.state.selectedQuestion[0].typeVraagId === 1) {
              if (
                this.state.selectedQuestion[0].jsonCorrecteAntwoord ===
                receivedAnswer
              ) {
                console.log(
                  "deze shit komt overeen " +
                    this.state.selectedQuestion[0].jsonCorrecteAntwoord +
                    " = " +
                    receivedAnswer
                );
                //post het antwoord naar de database

                this.updateScoreTeam(true, teamId);

                axios.post(
                  "https://makeaquizapi.azurewebsites.net/api/IngevoerdAntwoord/Create",
                  {
                    teamId,
                    vraagId: this.state.selectedQuestion[0].id,
                    gescoordeScore: this.state.selectedQuestion[0]
                      .maxScoreVraag,
                    antwoord: receivedAnswer,
                  }
                );
              } else {
                console.log(
                  "deze shit komt niet overeen " +
                    this.state.selectedQuestion[0].jsonCorrecteAntwoord +
                    " = " +
                    receivedAnswer
                );
                let messageUnderReview = {
                  answer: receivedAnswer,
                  answerTeam: teamId,
                };
                this.setState({
                  messagesUnderReview: [
                    ...this.state.messagesUnderReview,
                    messageUnderReview,
                  ],
                });

                //Update the score of the team
                //this.updateScoreTeam(false, teamId);

                /*axios.post(
                  "https://localhost:44302/api/IngevoerdAntwoord/Create",
                  {
                    teamId,
                    vraagId: this.state.selectedQuestion[0].id,
                    gescoordeScore: 0,
                    antwoord: receivedAnswer,
                  }
                );*/
              }
            }

            if (this.state.selectedQuestion[0].typeVraagId === 2) {
              if (
                this.state.selectedQuestion[0].jsonCorrecteAntwoord ===
                receivedAnswer
              ) {
                console.log(
                  "deze shit komt overeen " +
                    this.state.selectedQuestion[0].jsonCorrecteAntwoord +
                    " = " +
                    receivedAnswer
                );
                //post het antwoord naar de database

                //update the score of the team
                this.updateScoreTeam(true, teamId);

                axios.post(
                  "https://makeaquizapi.azurewebsites.net/api/IngevoerdAntwoord/Create",
                  {
                    teamId,
                    vraagId: this.state.selectedQuestion[0].id,
                    gescoordeScore: this.state.selectedQuestion[0]
                      .maxScoreVraag,
                    antwoord: receivedAnswer,
                  }
                );
              } else {
                //update the score of the team
                this.updateScoreTeam(false, teamId);

                axios.post(
                  "https://makeaquizapi.azurewebsites.net/api/IngevoerdAntwoord/Create",
                  {
                    teamId,
                    vraagId: this.state.selectedQuestion[0].id,
                    gescoordeScore: 0,
                    antwoord: receivedAnswer,
                  }
                );
              }
            }

            if (this.state.selectedQuestion[0].typeVraagId === 3) {
              if (
                this.state.selectedQuestion[0].jsonCorrecteAntwoord ===
                receivedAnswer
              ) {
                console.log(
                  "deze shit komt overeen " +
                    this.state.selectedQuestion[0].jsonCorrecteAntwoord +
                    " = " +
                    receivedAnswer
                );
                //post het antwoord naar de database

                //update the score of the team
                this.updateScoreTeam(true, teamId);

                axios.post(
                  "https://makeaquizapi.azurewebsites.net/api/IngevoerdAntwoord/Create",
                  {
                    teamId,
                    vraagId: this.state.selectedQuestion[0].id,
                    gescoordeScore: this.state.selectedQuestion[0]
                      .maxScoreVraag,
                    antwoord: receivedAnswer,
                  }
                );
              } else {
                //update the score of the team
                this.updateScoreTeam(false, teamId);

                axios.post(
                  "https://makeaquizapi.azurewebsites.net/api/IngevoerdAntwoord/Create",
                  {
                    teamId,
                    vraagId: this.state.selectedQuestion[0].id,
                    gescoordeScore: 0,
                    antwoord: receivedAnswer,
                  }
                );
              }
            }
          }
        }
      );
    });
  };

  updateScoreTeam = (answerCorrect, teamId) => {
    if (answerCorrect) {
      let teams = [...this.state.teams];
      let teamFilter = { ...teams.filter((x) => x.id === teamId) };
      teamFilter[0].score += this.state.selectedQuestion[0].maxScoreVraag;
      teamFilter[0].questionsAnswered += 1;
      teams.filter((x) => x.id === teamId)[0] = teamFilter;

      this.setState({ teams });
      //updating the history
      console.log("updating history");
      let newCount = this.state.countHistoryMessages;
      this.setState({
        history: [
          ...this.state.history,
          {
            index: ++newCount,
            message:
              "Received a correct answer from " +
              teamFilter[0].naam +
              " for question " +
              this.state.selectedQuestion.vraagStelling,
            time: new Date().toLocaleTimeString(),
          },
        ],
      });
    } else {
      let teams = [...this.state.teams];
      let teamFilter = { ...teams.filter((x) => x.id === teamId) };
      teamFilter[0].questionsAnswered += 1;
      teams.filter((x) => x.id === teamId)[0] = teamFilter;
      
      this.setState({ teams });
      //updating the history
      console.log("updating history");
      let newCount = this.state.countHistoryMessages;
      this.setState({
        history: [
          ...this.state.history,
          {
            index: ++newCount,
            message:
              "Received a wrong answer from " +
              teamFilter[0].naam +
              " for question " +
              this.state.selectedQuestion.vraagStelling,
            time: new Date().toLocaleTimeString(),
          },
        ],
      });
    }
  };

  setTeams = (teamsForQuiz) => {
    this.setState({ teams: teamsForQuiz });
  };

  selectRound = (roundId) => {
    console.log("the selected round id is " + roundId);
    this.setState({ selectedRound: roundId });
  };

  //Send scoreboard to the teams playing
  sendScoreboard = () => {
    this.state.hubConnection
      .invoke("SendScoreboard", this.state.teams, this.state.selectedQuiz)
      .catch((err) => console.error(err));
  };

  shuffleArray = (array) => {
    for (let i = array.length - 1; i > 0; i--) {
      let j = Math.floor(Math.random() * (i + 1));
      [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
  };

  answerReview = (answer, verdict) => {
    console.log(answer + " is " + verdict);
    //Update the scoreboard

    if (verdict === true) {
      //Update the score of the team
      let teams = [...this.state.teams];
      let teamFilter = { ...teams.filter((x) => x.id === answer.answerTeam) };
      teamFilter[0].score += this.state.selectedQuestion[0].maxScoreVraag;
      teamFilter[0].questionsAnswered += 1;
      teams.filter((x) => x.Name === answer.answerTeam)[0] = teamFilter;
      this.setState({ teams });

      //updating the history
      console.log("updating history");
      let newCount = this.state.countHistoryMessages;
      this.setState({
        history: [
          ...this.state.history,
          {
            index: ++newCount,
            message:
              "After review the answer from team " +
              teamFilter[0].naam +
              " is a valid answer",
            time: new Date().toLocaleTimeString(),
          },
        ],
      });
    } else {
      let teams = [...this.state.teams];
      let teamFilter = { ...teams.filter((x) => x.id === answer.answerTeam) };
      //updating the history
      console.log("updating history");
      let newCount = this.state.countHistoryMessages;
      this.setState({
        history: [
          ...this.state.history,
          {
            index: ++newCount,
            message:
              "After review the answer from team " +
              teamFilter[0].naam +
              " is not a valid answer",
            time: new Date().toLocaleTimeString(),
          },
        ],
      });
    }
    //Deleting the item from the state
    this.setState({
      messagesUnderReview: [
        ...this.state.messagesUnderReview.filter(
          (message) => message !== answer
        ),
      ],
    });
  };

  updateScore = (question) => {
    console.log(question);

    //Update the max score of the quiz by the total points of the send question
    let totalScore = this.state.totalPointsQuiz;
    totalScore += question[0].maxScoreVraag;
    this.setState({ totalPointsQuiz: totalScore });

    //Update the totalQuestionsOfQuiz and maxScore of the team
    let teams = this.state.teams.map((team) => {
      team.totalQuestionsOfQuiz += 1;
      team.maxScoreQuiz += question[0].maxScoreVraag;
      return team;
    });
    this.setState({ teams, selectedQuestion: question });

    //updating the history
    console.log("updating history");
    let newCount = this.state.countHistoryMessages;

    this.setState({
      history: [
        ...this.state.history,
        {
          index: ++newCount,
          message:
            "The question " + question[0].vraagStelling + " has been send",
          time: new Date().toLocaleTimeString(),
        },
      ],
    });
  };

  setQuiz = (quizId) => {
    this.setState({ selectedQuiz: quizId });
  };

  endRound = (roundName) => {
    console.log(roundName);

    //updating the history
    console.log("updating history");
    let newCount = this.state.countHistoryMessages;
    this.setState({
      history: [
        ...this.state.history,
        {
          index: ++newCount,
          message: "The round " + roundName + " has been stopped!",
          time: new Date().toLocaleTimeString(),
        },
      ],
      questions: [],
    });

    this.sendScoreboard();
  };

  getAllQuestions = () => {
    console.log("getting al questions");
    if (this.state.selectedRound != null) {
      console.log(
        "Getting all questions for rounds " + this.state.selectedRound
      );

      axios
        .get(
          "https://makeaquizapi.azurewebsites.net/api/Vragen/GetAllQuestionsFromARonde/" +
            this.state.selectedRound
        )
        .then((res) => this.setState({ questions: res.data }));
    }
  };

  sendMessage = (message) => {
    console.log(message);
    this.state.hubConnection
      .invoke("SendMessage", this.state.selectedQuiz, message)
      .catch((err) => console.error(err));
  };

  removeTeamFromQuiz = (teamId) => {
    console.log("removeing team with id " + teamId + " from quiz");
    //var teamThatHasBeenKickedOut = this.state.teams.filter(x => x.id == teamId);
    var teamsFiltered = this.state.teams.filter(x => x.id !== teamId);

    this.setState({teams : teamsFiltered});

    //teamThatHasBeenKickedOut.PIN = "";
    //axios.put("https://localhost:44302/api/Team/Update/", teamThatHasBeenKickedOut );
  }

  render() {
    const segmentStyle = {
      background: "rgb(221 229 236)",
      padding: 10,
      color: "white",
      borderRadius: "5px",
      boxShadow:
        "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)",
    };
    return (
      <div>
        {this.props.isAuthenticated ? (
          <div>
            <Grid columns={3} divided>
              <Grid.Row centered>
                {this.state.selectedQuiz != null ? (
                  <Segment
                    style={{
                      background: "rgb(221 229 236)",
                      padding: 10,
                      color: "white",
                      borderRadius: "5px",
                      width: "100%",
                    }}
                  >
                    <Message sendMessage={this.sendMessage}></Message>
                  </Segment>
                ) : (
                  <div />
                )}
              </Grid.Row>
              <Grid.Row stretched>
                <Grid.Column>
                  <Segment style={segmentStyle}>
                    <GetData
                      setQuiz={this.setQuiz}
                      selectRound={this.selectRound}
                      user={this.props.user}
                      setTeams={this.setTeams}
                      getAllQuestions={this.getAllQuestions}
                      questions={this.state.questions.length > 0 ? true : false}
                      endRound={this.endRound}
                    ></GetData>
                  </Segment>
                  {this.state.teams.length > 0 ? (
                    <Segment style={segmentStyle}>
                      <ScoreBoard
                        sendScoreboard={this.sendScoreboard}
                        teams={this.state.teams}
                        removeTeamFromQuiz={this.removeTeamFromQuiz}
                      ></ScoreBoard>
                    </Segment>
                  ) : (
                    <div />
                  )}
                </Grid.Column>
                {this.state.selectedQuiz != null ? (
                  <Grid.Column>
                    <Segment style={segmentStyle}>
                      <Questions
                        selectedQuiz={this.state.selectedQuiz}
                        selectedRound={this.state.selectedRound}
                        hubConnection={this.state.hubConnection}
                        updateScore={this.updateScore}
                        questions={this.state.questions}
                        endRound={this.endRound}
                      ></Questions>
                    </Segment>
                  </Grid.Column>
                ) : (
                  <div />
                )}
                {this.state.selectedQuiz !== null ? (
                  <Grid.Column>
                    <Segment style={segmentStyle}>
                      <ReviewAnswers
                        answerReview={this.answerReview}
                        selectedQuestion={this.state.selectedQuestion}
                        answersUnderReview={this.state.messagesUnderReview}
                      ></ReviewAnswers>
                    </Segment>
                  </Grid.Column>
                ) : (
                  <div />
                )}
              </Grid.Row>
              <Grid.Row centered>
                {this.state.selectedQuiz != null ? (
                  <Segment
                    style={{
                      background: "rgb(221 229 236)",
                      padding: 10,
                      color: "white",
                      borderRadius: "5px",
                      width: "100%",
                    }}
                  >
                    <History history={this.state.history}></History>
                  </Segment>
                ) : (
                  <div />
                )}
              </Grid.Row>
            </Grid>
          </div>
        ) : (
          <LoginNeeded redirect={this.props.redirect}></LoginNeeded>
        )}
      </div>
    );
  }
}

export default withAuth0(Host);
