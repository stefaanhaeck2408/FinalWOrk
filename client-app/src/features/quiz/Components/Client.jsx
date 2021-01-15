import React, { Component } from "react";
import * as signalR from "@aspnet/signalr";
import TypeVraagId1 from "./QuizGame/TypeVraagId1";
import TypeVraagId2 from "./QuizGame/TypeVraagId2";
import TypeVraagId3 from "./QuizGame/TypeVraagId3";
import Question from "./QuizGame/Question";
import Login from "./QuizGame/Login";
import axios from "axios";
import { Message } from "semantic-ui-react";
import Scoreboard from "./QuizGame/Scoreboard.jsx";
import CheckOutForm from "./Stripe/CheckoutForm";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import LoadingComponent from "../../../app/layout/LoadingComponent";

class Client extends Component {
  constructor(props) {
    super(props);

    this.state = {
      nick: "",
      hubConnection: null,
      currentQuestion: null,
      typeAnswer2Selected: "",
      answerTypeVraag1: "",
      positions: [],
      shuffleArray: [],
      teamPaidAllready: false,
      loading: false
    };
  }

  componentDidMount = () => {
    const nick = "test";

    const hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://makeaquizsignalr.azurewebsites.net/chatHub")
      .configureLogging(signalR.LogLevel.Trace)
      .build();

    this.setState({ hubConnection, nick }, () => {
      this.state.hubConnection
        .start()
        .then(() => console.log("Connection started!"))
        .catch((err) =>
          console.log("error while establishing connection: " + err)
        );

      this.state.hubConnection.on(
        "ReceiveMessage",
        (quizId, messageFromHost) => {
          if (this.state.selectedTeam.quizId === quizId) {
            this.setState({ message: messageFromHost });
          }
        }
      );

      this.state.hubConnection.on("ReceiveScoreboard", (teams, quizId) => {
        if (
          this.state.selectedTeam !== undefined &&
          this.state.selectedTeam.quizId === quizId
        ) {
          this.setState({
            scoreboard: teams,
            currentQuestion: null,
            mouseLeftDiv: false,
          });
        }
      });

      this.state.hubConnection.on(
        "ReceiveQuestion",
        (receivedQuestion, quizId) => {
          console.log("question received 1");
          if (this.state.selectedTeam !== undefined) {
            console.log(quizId + " from question received");
            if (quizId === this.state.selectedTeam.quizId) {
              this.setState({
                currentQuestion: receivedQuestion,
                scoreboard: null,
              });
            } else {
              console.log(
                quizId + " niet gelijk aan " + this.state.selectedTeam.quizId
              );
            }
          }
        }
      );
    });
  };

  sendMessage = () => {
    this.state.hubConnection
      .invoke("SendMessage", this.state.nick, this.state.message)
      .catch((err) => console.error(err));

    this.setState({ message: "" });
  };

  shuffleArray = (array) => {
    for (let i = array.length - 1; i > 0; i--) {
      let j = Math.floor(Math.random() * (i + 1));
      [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
  };

  //Find a way to return all the answer correctly
  submitAnswer = (e, answer) => {
    e.preventDefault();
    console.log(answer);
    this.state.hubConnection
      .invoke(
        "SendAnswer",
        this.state.selectedTeam.naam,
        answer,
        this.state.selectedTeam.id
      )
      .catch((err) => console.error(err));

    this.setState({ currentQuestion: null, positions: [] });
  };

  userLeftGame = () => {
    if (this.state.scoreboard != null) {
      this.state.hubConnection
        .invoke(
          "UserLeftGame",
          this.state.selectedTeam.naam,
          this.state.selectedTeam.id
        )
        .catch((err) => console.error(err));
    }
  };

  setPaidAllready = (paid) => {
    this.setState({teamPaidAllready: paid});
  }

  login = async (e, password) => {
    e.preventDefault();
    console.log(password + " entry at login");
    this.setState({error : null, loading: true})
    await axios
      .get("https://makeaquizapi.azurewebsites.net/api/Team/GetByPin/" + password)
      .then(
        async (result) => {
          await axios
            .get(
              "https://makeaquizapi.azurewebsites.net/api/Quiz/GetById/" + result.data.quizId
            )
            .then((resultQuiz) => {
              this.setState({
                isLoaded: true,
                selectedTeam: result.data,
                selectQuiz: resultQuiz.data,
                teamPaidAllready: resultQuiz.data.freeQuiz ? true : result.data.teamPaidAllready,
                loading: false
              });
            });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error,
            loading: false
          });
        }
      );
  };

  typeAnswer = (question) => {
    if (question != null) {
      if (question.typeVraagId === 1) {
        return <TypeVraagId1 submitAnswer={this.submitAnswer} />;
      }
      if (question.typeVraagId === 2) {
        var answersArray = question.jsonMogelijkeAntwoorden.split(",");
        var allAnswersArray = [...answersArray, question.jsonCorrecteAntwoord];
        var arrayShuffle = this.shuffleArray(allAnswersArray);

        return (
          <TypeVraagId2 array={arrayShuffle} submitAnswer={this.submitAnswer} />
        );
      }
      if (question.typeVraagId === 3) {
        var array = question.jsonCorrecteAntwoord.split(",");
        arrayShuffle = this.shuffleArray(array);
        return (
          <TypeVraagId3
            array={arrayShuffle}
            setPosition={this.setPosition}
            positions={this.state.positions}
            resetPositions={this.resetPosition}
            submitAnswer={this.submitAnswer}
          />
        );
      }
    }
    return <p>Geen vraag</p>;
  };

  setPosition = (answer) => {
    if (!this.state.positions.includes(answer)) {
      this.setState({ positions: [...this.state.positions, answer] });
    }
  };

  resetPosition = () => {
    this.setState({ positions: [] });
  };

  mouseLeftDiv = (bool) => {
    this.setState({ mouseLeftDiv: bool });
  };

  stripePromise = loadStripe(
    "pk_test_51HvkPWGZgacNuBfZfpsww90AdX0dbLWitN2Wv1gFUrmJOR5pwpRX83CWmwdlz56b1n713Yz2oFTUpTZW1nfKltpZ00bDA2jcAR"
  );

  render() {
    const messageStyle = {
      padding: "20px",
      backgroundColor: "rgb(85, 96, 143)",
      color: "white",
      marginBottom: "15px",
    };

    if(this.state.loading === true){
      return <LoadingComponent content="Loading quiz..."/>
    }
    return (
      <div>
        {this.props.isAuthenticated ? <div></div> : <Message>To create a quiz or start your Quiz Master role, login first. </Message>}
        {this.state.selectedTeam == null ? (
          <div>
            <Login login={this.login} />
          </div>
        ) : (
          <div/>
        )}

        {this.state.teamPaidAllready === false ? this.state.selectedTeam != null ? (
          <Elements stripe={this.stripePromise} >
            <CheckOutForm selectQuiz={this.state.selectQuiz} selectedTeam={this.state.selectedTeam} setPaidAllready={this.setPaidAllready}/>
          </Elements>
        ) : (
          <div></div>
        ): (<div>
          <h3
            style={{
              padding: "10px",
              fontFamily: "Dancing Script",
              color: "#193670",
              borderRadius: "5px",
            }}
          >
            Welcome your playing as {this.state.selectedTeam.naam} the quiz
            with name {this.state.selectQuiz.naam}
          </h3>

          <div>
            {this.state.message != null ? (
              <div style={messageStyle}>
                Message from your quiz master!: {this.state.message}
              </div>
            ) : (
              <p />
            )}
            {this.state.mouseLeftDiv === true ? (
              <div
                style={{
                  padding: "20px",
                  backgroundColor: "#f44336",
                  color: "white",
                  marginBottom: "15px",
                }}
              >
                Your mouse must remain in the quiz game! You will get kicked
                out of the quiz in a couple of seconds!!
              </div>
            ) : (
              <div />
            )}
          </div>
          {this.state.scoreboard == null ? (
            <Question
              question={
                this.state.currentQuestion != null
                  ? this.state.currentQuestion
                  : null
              }
              setPosition={this.setPosition}
              positions={this.state.positions}
              resetPosition={this.resetPosition}
              submitAnswer={this.submitAnswer}
              userLeftGame={this.userLeftGame}
              mouseLeftDiv={this.mouseLeftDiv}
              selectedTeam={this.state.selectedTeam}
              receiveScoreboard={this.state.scoreboard == null ? false : true}
            />
          ) : (
            <Scoreboard score={this.state.scoreboard} />
          )}
        </div>)}

        {this.state.error != null ? (
          <Message color="red" size="small" style={{ width: "200px" }}>
            <Message.Header>No team with that pin</Message.Header>
          </Message>
        ) : (
          <div />
        )}
      </div>
    );
  }
}

export default Client;
