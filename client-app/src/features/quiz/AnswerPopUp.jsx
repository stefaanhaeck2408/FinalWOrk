import React, { Component } from "react";
import {
  Grid,
  Header,
  Icon,
  Button,
  Popup,
  Form,
  Input,
  Message,
  Loader,
  Label
} from "semantic-ui-react";
import axios from "axios";

class AnswerPopUp extends Component {
  state = {
    answerCorrect: "",
    multipleChoiceWrong1: "",
    multipleChoiceWrong2: "",
    multipleChoiceWrong3: "",
    rankingAnswer1: "",
    rankingAnswer2: "",
    rankingAnswer3: "",
    rankingAnswer4: "",
    loadingAddAnswer: false
  };

  questionSelected(questionid) {
    this.setState({
      answers: [],
    });
    fetch("https://makeaquizapi.azurewebsites.net/api/Vragen/GetById/" + questionid)
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            selectedQuestion: result,
            answers: [
              result.jsonCorrecteAntwoord,
              result.jsonMogelijkeAntwoorden,
            ],
          });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error,
          });
        }
      );
  }

  clearAllMessages = () => {
    this.setState({
      createAnswerCorrectError: null,
      createAnswerMessage: null,
      createanswerMultipleError: null,
      createanswerRankingError: null,
    });
  };

  addAnswer = async () => {
    this.setState({loadingAddAnswer: true})
    let finalAnswerCorrect = null;
    let mogelijkeAntwoorden = "";
    this.clearAllMessages();
    if (this.props.selectedQuestion.typeVraagId === 1) {
      if (this.state.answerCorrect !== "") {
        finalAnswerCorrect = this.state.answerCorrect;

        await axios
          .put("https://makeaquizapi.azurewebsites.net/api/Vragen/update", {
            id: this.props.selectedQuestion.id,
            maxScoreVraag: this.props.selectedQuestion.maxScoreVraag,
            typeVraagId: this.props.selectedQuestion.typeVraagId,
            vraagStelling: this.props.selectedQuestion.vraagStelling,
            jsonCorrecteAntwoord: finalAnswerCorrect,
            jsonMogelijkeAntwoorden: mogelijkeAntwoorden,
          })
          .then((response) => console.log(response));

        this.props.updateCorrectAndWrongAnswers(
          finalAnswerCorrect,
          mogelijkeAntwoorden
        );
        this.questionSelected(this.props.selectedQuestion.id);
        this.setState({ createAnswerMessage: "Answer created!" });
      } else {
        this.setState({ createAnswerCorrectError: "Answer cannot be empty!" });
      }
    }
    if (this.props.selectedQuestion.typeVraagId === 2) {
      if (this.state.answerCorrect !== "") {
        finalAnswerCorrect = this.state.answerCorrect;
      } else {
        this.setState({ createAnswerCorrectError: "Answer cannot be empty!" });
      }
      if (
        this.state.multipleChoiceWrong1 === "" ||
        this.state.multipleChoiceWrong2 === "" ||
        this.state.multipleChoiceWrong3 === ""
      ) {
        this.setState({
          createanswerMultipleError: "3 wrong answers required!",
        });
      } else {
        mogelijkeAntwoorden =
          this.state.multipleChoiceWrong1 +
          "," +
          this.state.multipleChoiceWrong2 +
          "," +
          this.state.multipleChoiceWrong3;

        await axios
          .put("https://makeaquizapi.azurewebsites.net/api/Vragen/update", {
            id: this.props.selectedQuestion.id,
            maxScoreVraag: this.props.selectedQuestion.maxScoreVraag,
            typeVraagId: this.props.selectedQuestion.typeVraagId,
            vraagStelling: this.props.selectedQuestion.vraagStelling,
            jsonCorrecteAntwoord: finalAnswerCorrect,
            jsonMogelijkeAntwoorden: mogelijkeAntwoorden,
          })
          .then((response) => console.log(response));

        this.props.updateCorrectAndWrongAnswers(
          finalAnswerCorrect,
          mogelijkeAntwoorden
        );
        this.questionSelected(this.props.selectedQuestion.id);
        this.setState({ createAnswerMessage: "Answer created!" });
      }
    }
    if (this.props.selectedQuestion.typeVraagId === 3) {
      if (
        this.state.rankingAnswer1 === "" ||
        this.state.rankingAnswer2 === "" ||
        this.state.rankingAnswer3 === "" ||
        this.state.rankingAnswer4 === ""
      ) {
        this.setState({
          createanswerRankingError: "4 ranking answers required!",
        });
      } else {
        finalAnswerCorrect =
          this.state.rankingAnswer1 +
          "," +
          this.state.rankingAnswer2 +
          "," +
          this.state.rankingAnswer3 +
          "," +
          this.state.rankingAnswer4;

        await axios
          .put("https://makeaquizapi.azurewebsites.net/api/Vragen/update", {
            id: this.props.selectedQuestion.id,
            maxScoreVraag: this.props.selectedQuestion.maxScoreVraag,
            typeVraagId: this.props.selectedQuestion.typeVraagId,
            vraagStelling: this.props.selectedQuestion.vraagStelling,
            jsonCorrecteAntwoord: finalAnswerCorrect,
            jsonMogelijkeAntwoorden: mogelijkeAntwoorden,
          })
          .then((response) => console.log(response));

        this.props.updateCorrectAndWrongAnswers(
          finalAnswerCorrect,
          mogelijkeAntwoorden
        );
        this.questionSelected(this.props.selectedQuestion.id);
        this.setState({ createAnswerMessage: "Answer created!"});
      }
    }
    this.setState({loadingAddAnswer: false})
  };

  handleChangeCorrectAnswer = (event) => {
    this.setState({
      answerCorrect: event.target.value,
      createAnswerCorrectError: null,
    });
  };

  handleChangeWrongAnswer1 = (event) => {
    this.setState({
      multipleChoiceWrong1: event.target.value,
      createanswerMultipleError: null,
    });
  };

  handleChangeWrongAnswer2 = (event) => {
    this.setState({
      multipleChoiceWrong2: event.target.value,
      createanswerMultipleError: null,
    });
  };

  handleChangeWrongAnswer3 = (event) => {
    this.setState({
      multipleChoiceWrong3: event.target.value,
      createanswerMultipleError: null,
    });
  };

  handleChangeRankingAnswer1 = (event) => {
    this.setState({
      rankingAnswer1: event.target.value,
      createanswerRankingError: null,
      createAnswerCorrectError: null,
    });
  };

  handleChangeRankingAnswer2 = (event) => {
    this.setState({
      rankingAnswer2: event.target.value,
      createanswerRankingError: null,
      createAnswerCorrectError: null,
    });
  };

  handleChangeRankingAnswer3 = (event) => {
    this.setState({
      rankingAnswer3: event.target.value,
      createanswerRankingError: null,
      createAnswerCorrectError: null,
    });
  };

  handleChangeRankingAnswer4 = (event) => {
    this.setState({
      rankingAnswer4: event.target.value,
      createanswerRankingError: null,
      createAnswerCorrectError: null,
    });
  };

  render() {
    let componentPopUp = null;

    if (this.props.selectedQuestion != null) {
      if (this.props.selectedQuestion.typeVraagId === 1) {
        componentPopUp = (
          <div>
            {this.state.createAnswerMessage != null ? (
              <Message color="green" size="mini">
                <Message.Header>
                  {this.state.createAnswerMessage}
                </Message.Header>
              </Message>
            ) : (
              <div></div>
            )}            
            <Form onSubmit={this.addAnswer}>
              <Label color="green">{"Question : '" + this.props.selectedQuestion.vraagStelling + "?'"}</Label>
              <p></p>
              <Input
                placeholder="Your answer.."
                type="text"
                onChange={this.handleChangeCorrectAnswer}
                value={this.state.answerCorrect}   
                style={{width:'80%'}}             
              ></Input>
              {this.state.createAnswerCorrectError != null ? (
                <Message color="red" size="mini">
                  <Message.Header>
                    {this.state.createAnswerCorrectError}
                  </Message.Header>
                </Message>
              ) : (
                <div></div>
              )}
              <p></p>
              <Input type="submit" value="Submit"></Input>
              {this.state.loadingAddAnswer ? <Loader active inline size='mini'/> : <div/>}
            </Form>
          </div>
        );
      }
      if (this.props.selectedQuestion.typeVraagId === 2) {
        componentPopUp = (
          <div>
            {this.state.createAnswerMessage != null ? (
              <Message color="green" size="mini">
                <Message.Header>
                  {this.state.createAnswerMessage}
                </Message.Header>
              </Message>
            ) : (
              <div></div>
            )}
            <Header as="h4">Multiple choice</Header>
            <Form onSubmit={this.addAnswer}>
              <div>
              <Label size="small" color="green">Multiple choice correct answer </Label>
              <Input
                color="green"
                type="text"
                onChange={this.handleChangeCorrectAnswer}
              ></Input>
              </div>
              
              {this.state.createAnswerCorrectError != null ? (
                <Message color="red" size="mini">
                  <Message.Header>
                    {this.state.createAnswerCorrectError}
                  </Message.Header>
                </Message>
              ) : (
                <div></div>
              )}
              <p></p>
              <Label size="small" color="red">Multiple choice first wrong answer </Label>
              <Input
                type="text"
                onChange={this.handleChangeWrongAnswer1}
              ></Input>
              <p></p>
              <Label size="small" color="red">Multiple choice second wrong answer </Label>

              <Input
                type="text"
                onChange={this.handleChangeWrongAnswer2}
              ></Input>
              <p></p>
              <Label size="small" color="red">Multiple choice third wrong answer </Label>

              <Input                
                type="text"
                onChange={this.handleChangeWrongAnswer3}
              ></Input>
              {this.state.createanswerMultipleError != null ? (
                <Message color="red" size="mini">
                  <Message.Header>
                    {this.state.createanswerMultipleError}
                  </Message.Header>
                </Message>
              ) : (
                <div></div>
              )}
              <br></br>
              <Input type="submit" value="Submit"></Input>
              {this.state.loadingAddAnswer ? <Loader active inline size='mini'/> : <div/>}
            </Form>
          </div>
        );
      }
      if (this.props.selectedQuestion.typeVraagId === 3) {
        componentPopUp = (
          <div>
            {this.state.createAnswerMessage != null ? (
              <Message color="green" size="mini">
                <Message.Header>
                  {this.state.createAnswerMessage}
                </Message.Header>
              </Message>
            ) : (
              <div></div>
            )}
            <Header as="h4">Ranking question</Header>
            <Form onSubmit={this.addAnswer}>
              <Input
                placeholder="Your first answer... "
                type="text"
                onChange={this.handleChangeRankingAnswer1}
              ></Input>
              <p></p>
              <Input
                placeholder="Your second answer..."
                type="text"
                onChange={this.handleChangeRankingAnswer2}
              ></Input>
              <p></p>
              <Input
                placeholder="Your third answer..."
                type="text"
                onChange={this.handleChangeRankingAnswer3}
              ></Input>
              <p></p>
              <Input
                placeholder="Your fourth answer..."
                type="text"
                onChange={this.handleChangeRankingAnswer4}
              ></Input>
              {this.state.createanswerRankingError != null ? (
                <Message color="red" size="mini">
                  <Message.Header>
                    {this.state.createanswerRankingError}
                  </Message.Header>
                </Message>
              ) : (
                <div></div>
              )}
              {this.state.createAnswerCorrectError != null ? (
                <Message color="red" size="mini">
                  <Message.Header>
                    {this.state.createAnswerCorrectError}
                  </Message.Header>
                </Message>
              ) : (
                <div></div>
              )}
              <br></br>
              <Input type="submit" value="Submit"></Input>
              {this.state.loadingAddAnswer ? <Loader active inline size='mini'/> : <div/>}
            </Form>
          </div>
        );
      }
    }

    return (
      <div>
        <Popup
          trigger={
            <Button icon style={{ marginLeft: "5%" }}>
              <Icon name="plus" />
            </Button>
          }
          on='click'
          style={{ width: "500px" }}
        >
          <Grid centered divided>
            <Grid.Column textAlign="center">
              <Header as="h4">Make a new Answer</Header>
              {componentPopUp}
            </Grid.Column>
          </Grid>
        </Popup>
      </div>
    );
  }
}

export default AnswerPopUp;
