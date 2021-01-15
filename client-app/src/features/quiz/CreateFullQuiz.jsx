import React from "react";
import {
  List,
  Grid,
  Header,
  Icon,
  Button,
  Popup,
  Form,
  Input,
  Radio,
  Message,
  Loader,
} from "semantic-ui-react";
import axios from "axios";
import AnswerPopUp from "./AnswerPopUp";
import LoginNeeded from "./Components/LoginNeeded.jsx";
import Moment from "react-moment";
import "moment-timezone";
import LoadingComponent from "../../app/layout/LoadingComponent";

class CreateFullQuiz extends React.Component {
  state = {
    error: null,
    isLoaded: false,
    quizes: [],
    selectedQuiz: null,
    rondes: [],
    selectedRonde: null,
    questions: [],
    selectedQuestion: null,
    answers: [],
    quizName: "",
    roundName: "",
    questionName: "",
    questionMaxScore: 0,
    questionTypeOf: "1",
    answerCorrect: null,
    multipleChoiceWrong1: null,
    multipleChoiseWrong2: null,
    mulitpleChoiseWrong3: null,
    rankingAnswer1: null,
    rankingAnswer2: null,
    rankingAnswer3: null,
    rankingAnswer4: null,
    quizFreeChecked: false,
    stripeURL: null,
    stripeAccount: null,
    quizFeePrice: 0,
    loading: false,
    loadingAddQuiz: false,
    loadingContent: "",
    loadingAddQuestion: false,
    loadingAddRound: false,
    loadingAddAnswer: false,
    beforeClickButton: true
  };

  componentDidMount() {
    //this.getAllQuizen();
  }

  quizSelected(quizid) {
    this.setState({
      selectedQuiz: quizid,
      selectedRonde: null,
      selectedQuestion: null,
    });
    fetch(
      "https://makeaquizapi.azurewebsites.net/api/Ronde/GetAllRondesInAQuiz/" +
        quizid
    )
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            questions: [],
            answers: [],
            rondes: result,
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

  rondeSelected(rondeid) {
    this.setState({ selectedRonde: rondeid, selectedQuestion: null });
    fetch(
      "https://makeaquizapi.azurewebsites.net/api/Vragen/GetAllQuestionsFromARonde/" +
        rondeid
    )
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            answers: [],
            questions: result,
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

  questionSelected(questionid) {
    this.setState({
      answers: [],
    });
    fetch(
      "https://makeaquizapi.azurewebsites.net/api/Vragen/GetById/" + questionid
    )
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

  getAllQuizen = () => {
    this.setState({ loading: true, loadingContent: "Loading quizzes...", beforeClickButton: false });
    fetch(
      "https://makeaquizapi.azurewebsites.net/api/Quiz/GetAllQuizesForOneUser/" +
        this.props.user.email
    )
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            quizes: result,
          });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error,
          });
        }
      );

    this.getStripeAccount();
  };

  getStripeAccount = () => {
    fetch(
      "https://makeaquizapi.azurewebsites.net/api/Account/GetByEmail/" +
        this.props.user.email
    )
      .then((res) => res.json())
      .then((result) => {
        if (result.stripeAccountId != null) {
          this.setState({
            loading: false,
            isLoaded: false,
            stripeAccount: result,
            loadingContent: "",
          });
        } else {
          this.setState({
            loading: false,
            isLoaded: false,
            loadingContent: "",
          });
        }
      });
  };

  getStripeLink = () => {
    console.log("Button works");

    axios
      .post("https://makeaquizapi.azurewebsites.net/api/Account/Create/", {
        userEmail: this.props.user.email,
      })
      .then((result) => {
        console.log("ok we geraken hier al");
        this.setState({ stripeURL: result.data });
      });
  };

  addQuiz = async () => {
    this.setState({ loadingAddQuiz: true, loadingContent: "Adding Quiz!" });
    console.log("the value is: " + this.state.quizName);

    if (this.state.quizName !== "") {
      await axios
        .post("https://makeaquizapi.azurewebsites.net/api/Quiz/Create", {
          naam: this.state.quizName,
          emailCreator: this.props.user.email,
          FreeQuiz: !this.state.quizFreeChecked,
          EntryFee: this.state.quizFeePrice.toString(),
        })
        .then((response) =>
          this.setState({
            quizes: [
              ...this.state.quizes,
              {
                didCreatorPayAllready: response.data.didCreatorPayAllready,
                emailCreator: response.data.emailCreator,
                entryFee: response.data.entryFee,
                freeQuiz: response.data.freeQuiz,
                id: response.data.id,
                naam: response.data.naam,
                updatedAt: response.data.updatedAt
              },
            ],
            selectedQuiz: response.data.id
          })
        );
      //this.getAllQuizen();
      this.setState({
        createQuizMessage: "Quiz created!",
        loadingAddQuiz: false,
        loadingContent: "",
        quizName: "",
      });
    } else {
      this.setState({
        createQuizError: "Quiz name cannot be empty",
        loadingAddQuiz: false,
      });
    }
  };

  deleteQuiz = async (quizId) => {
    this.setState({ loading: true, loadingContent: "Deleting quiz..." });
    await axios.delete(
      "https://makeaquizapi.azurewebsites.net/api/Quiz/Delete/" + quizId
    );
    this.getAllQuizen();
  };

  addRound = async () => {
    this.setState({ loadingAddRound: true, loadingContent: "Adding round!" });
    let rondeId = null;
    if (this.state.roundName !== "") {
      await axios
        .post("https://makeaquizapi.azurewebsites.net/api/Ronde/Create", {
          naam: this.state.roundName,
        })
        .then((response) => (rondeId = response.data.id));

      await axios
        .post(
          "https://makeaquizapi.azurewebsites.net/api/Quiz/AddRoundToQuiz",
          {
            quizId: this.state.selectedQuiz,
            rondeId: rondeId,
          }
        )
        .then((response) => console.log(response));

      this.quizSelected(this.state.selectedQuiz);
      this.setState({
        createRoundMessage: "Round created!",
        loadingAddRound: false,
        loadingContent: "",
        roundName: "",
      });
    } else {
      this.setState({
        createRoundError: "De round name cannot be empty!",
        loadingAddRound: false,
        loadingContent: "",
      });
    }
  };

  deleteRound = async (roundId) => {
    this.setState({ loading: true, loadingContent: "Deleting round!" });
    await axios.delete(
      "https://makeaquizapi.azurewebsites.net/api/Ronde/Delete/" + roundId
    );

    this.quizSelected(this.state.selectedQuiz);
    this.setState({ loading: false, loadingContent: "Deleting round!" });
  };

  deleteAnswer = async (answerId) => {
    this.setState({ loading: true, loadingContent: "Deleting answer!" });
    var question = this.state.selectedQuestion;
    question.jsonCorrecteAntwoord = "";
    var response = await axios.put(
      "https://makeaquizapi.azurewebsites.net/api/Vragen/Update/",
      question
    );
    var array = this.state.answers;

    this.setState({
      selectedQuestion: question,
      answers: array.splice(answerId, 1),
      loading: false,
      loadingContent: "",
    });
    console.log(response);
  };

  addQuestion = async () => {
    this.setState({
      loadingAddQuestion: true,
      loadingContent: "Adding question!",
    });
    let questionId = null;
    let maxScoreInt = parseInt(this.state.questionMaxScore);
    let typeQuestion = parseInt(this.state.questionTypeOf);
    if (this.state.questionName !== "") {
      await axios
        .post("https://makeaquizapi.azurewebsites.net/api/Vragen/Create", {
          maxScoreVraag: maxScoreInt,
          typeVraagId: typeQuestion,
          vraagStelling: this.state.questionName,
          jsonCorrecteAntwoord: "",
          jsonMogelijkeAntwoorden: "",
        })
        .then((response) => (questionId = response.data.id));

      await axios.post(
        "https://makeaquizapi.azurewebsites.net/api/Vragen/AddVraagToRonde",
        {
          vraagId: questionId,
          rondeId: this.state.selectedRonde,
        }
      );

      this.rondeSelected(this.state.selectedRonde);
      this.setState({
        createQuestionMessage: "Question created!",
        loadingAddQuestion: false,
        loadingContent: "",
      });
    } else {
      this.setState({
        createQuestionError: "The question cannot be empty!",
        loadingAddQuestion: false,
        loadingContent: "",
      });
    }
  };

  deleteQuestion = async (questionId) => {
    this.setState({ loading: true, loadingContent: "Deleting question..." });
    console.log("delete question with id " + questionId);
    await axios.delete(
      "https://makeaquizapi.azurewebsites.net/api/vragen/Delete/" + questionId
    );

    this.rondeSelected(this.state.selectedRonde);
    this.setState({ loading: false, loadingContent: "" });
  };

  handleChangeQuiz = (event) => {
    this.setState({
      quizName: event.target.value,
      createQuizMessage: null,
      createQuizError: null,
    });
  };

  handleChangeRound = (event) => {
    this.setState({
      roundName: event.target.value,
      createRoundMessage: null,
      createRoundError: null,
    });
  };

  handleChangeQuestion = (event) => {
    this.setState({
      questionName: event.target.value,
      createQuestionMessage: null,
      createQuestionError: null,
    });
  };

  handleChangeQuestionMaxScore = (event) => {
    this.setState({
      questionMaxScore: event.target.value,
      createQuestionMaxScoreMessage: null,
      createQuestionMaxScoreError: null,
    });
  };

  handleChangeTypeQuestion = (e, { value }) => {
    this.setState({ questionTypeOf: value });
  };

  handleQuizFeePrice = (event) => {
    console.log(event.target.value);
    this.setState({ quizFeePrice: event.target.value });
  };

  handleQuizPrice = () => {
    this.setState({ quizFreeChecked: !this.state.quizFreeChecked });
  };

  updateCorrectAnswer = (answer) => {
    console.log(answer);
    var question = this.state.selectedQuestion;
    question.jsonCorrecteAntwoord = answer;
    this.setState({ selectedQuestion: question, answers: [answer] });
  };

  updateCorrectAndWrongAnswers = (correct, wrong) => {
    var question = this.state.selectedQuestion;
    question.jsonCorrecteAntwoord = correct;
    question.jsonMogelijkeAntwoorden = wrong;
    this.setState({ selectedQuestion: question, answers: [correct, wrong] });
  };

  render() {
    if (this.state.loading === true) {
      return <LoadingComponent content={this.state.loadingContent} />;
    }

    return (
      <div>
        {this.props.isAuthenticated ? (
          <div>
            <div>
              {this.state.beforeClickButton == true ? (
                <div
                  style={{
                    padding: 9,
                    background: "#e0dcdc",
                    marginBottom: "5px",
                    color: "black",
                    borderRadius: "5px",
                    boxShadow:
                      "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)",
                    width: "50%",
                    textAlign: "center",
                  }}
                >
                  <Button
                    size="massive"
                    inverted
                    color="green"
                    onClick={this.getAllQuizen}
                    style={{}}
                  >
                    Get all your quizzes
                  </Button>
                </div>
              ) : (
                <div></div>
              )}
            </div>

            {this.state.beforeClickButton === false ? (
              <div>
                <Header as="h2" icon textAlign="center">
                  <Icon name="users" circular />
                  <Header.Content>Create your quiz</Header.Content>
                </Header>
                {this.state.stripeAccount != null ? (
                  <div>Congrats you can make a paid quiz.</div>
                ) : (
                  <div>
                    If you want to host a paid quiz.{" "}
                    <button onClick={() => this.getStripeLink()}>
                      Click here!
                    </button>
                    {this.state.stripeURL != null ? (
                      <a href={this.state.stripeURL}>
                        Make your stripe account here
                      </a>
                    ) : (
                      <div />
                    )}
                  </div>
                )}
                <p></p>
                <Grid>
                  <Grid.Column width={4}>
                    <h1 style={{ float: "left" }}>Quizzes</h1>

                    <Popup
                      trigger={
                        <Button icon style={{ marginLeft: "5%" }}>
                          <Icon name="plus" />
                        </Button>
                      }
                      on="click"
                    >
                      <Grid centered divided>
                        <Grid.Column textAlign="center">
                          <div>
                            {this.state.createQuizMessage != null ? (
                              <Message color="green" size="mini">
                                <Message.Header>
                                  {this.state.createQuizMessage}
                                </Message.Header>
                              </Message>
                            ) : (
                              <div></div>
                            )}
                            <Header as="h4">Make a new quiz</Header>
                            <Form onSubmit={this.addQuiz}>
                              <Input
                                placeholder="Name of quiz?"
                                type="text"
                                onChange={this.handleChangeQuiz}
                                value={this.state.quizName}
                              ></Input>
                              <p></p>
                              {this.state.createQuizError != null ? (
                                <Message color="red" size="mini">
                                  <Message.Header>
                                    {this.state.createQuizError}
                                  </Message.Header>
                                </Message>
                              ) : (
                                <div></div>
                              )}

                              {this.state.stripeAccount != null ? (
                                <Message color="green" size="mini">
                                  <Message.Header>
                                    Attention! We take 5% commission
                                  </Message.Header>
                                </Message>
                              ) : (
                                <div></div>
                              )}
                              <Radio
                                label="Users need to pay to play this quiz"
                                onClick={this.handleQuizPrice}
                                checked={this.state.quizFreeChecked}
                              ></Radio>
                              <br></br>
                              {this.state.stripeAccount != null ? (
                                this.state.quizFreeChecked ? (
                                  <div
                                    style={{ width: "40px", marginLeft: "0px" }}
                                  >
                                    <Input
                                      type="number"
                                      label="Entry fee for your quiz"
                                      onChange={this.handleQuizFeePrice}
                                      value={this.state.quizFreePrice}
                                    ></Input>
                                    <br></br>
                                  </div>
                                ) : (
                                  <div></div>
                                )
                              ) : (
                                <div></div>
                              )}
                              <p></p>
                              <Input type="submit" value="Submit"></Input>
                              {this.state.loadingAddQuiz ? (
                                <Loader active inline size="mini" />
                              ) : (
                                <div />
                              )}
                            </Form>
                          </div>
                        </Grid.Column>
                      </Grid>
                    </Popup>
                    <List divided relaxed>
                      {this.state.quizes.length > 0 ? (
                        this.state.quizes.map((quiz) => (
                          <List.Item
                            key={quiz.id}
                            onClick={() => this.quizSelected(quiz.id)}
                            style={
                              this.state.selectedQuiz === quiz.id
                                ? {
                                    background: "#a9d5de",
                                    boxShadow:
                                      "0 0 0 1px #a9d5de inset, 0 0 0 0 transparent",
                                  }
                                : {}
                            }
                          >
                            <List.Icon
                              name="group"
                              size="large"
                              verticalAlign="middle"
                            />
                            <List.Content>
                              <List.Header as="a">
                                {quiz.naam}{" "}
                                {quiz.freeQuiz ? " - Free" : " - Paying"}
                              </List.Header>
                              <div>
                                <List.Description
                                  as="a"
                                  style={{
                                    float: "left",
                                  }}
                                >
                                  {"Updated "}
                                  <Moment fromNow add={{ hours: 1 }}>
                                    {quiz.updatedAt}
                                  </Moment>
                                </List.Description>
                                <Button
                                  icon
                                  onClick={() => this.deleteQuiz(quiz.id)}
                                  style={{ marginLeft: "40px" }}
                                >
                                  <Icon name="trash" color="red" size="small" />
                                </Button>
                              </div>
                            </List.Content>
                          </List.Item>
                        ))
                      ) : (
                        <p>
                          No quizzes available for this user. Add one by
                          clicking on the plus button above!
                        </p>
                      )}
                    </List>
                  </Grid.Column>
                  <Grid.Column width={4}>
                    {this.state.selectedQuiz != null ? (
                      <div>
                        <h1 style={{ float: "left" }}>Rounds</h1>
                        <Popup
                          trigger={
                            <Button icon style={{ marginLeft: "5%" }}>
                              <Icon name="plus" />
                            </Button>
                          }
                          on="click"
                        >
                          <Grid centered divided>
                            <Grid.Column textAlign="center">
                              {this.state.createRoundMessage != null ? (
                                <Message color="green" size="mini">
                                  <Message.Header>
                                    {this.state.createRoundMessage}
                                  </Message.Header>
                                </Message>
                              ) : (
                                <div></div>
                              )}
                              <Header as="h4">Make a new round</Header>
                              <Form onSubmit={this.addRound}>
                                <Input
                                  placeholder="Name of round?"
                                  type="text"
                                  onChange={this.handleChangeRound}
                                  value={this.state.roundName}
                                ></Input>
                                {this.state.createRoundError != null ? (
                                  <Message color="red" size="mini">
                                    <Message.Header>
                                      {this.state.createRoundError}
                                    </Message.Header>
                                  </Message>
                                ) : (
                                  <div></div>
                                )}
                                <p></p>
                                <Input type="submit" value="Submit"></Input>
                                {this.state.loadingAddRound ? (
                                  <Loader active inline size="mini" />
                                ) : (
                                  <div />
                                )}
                              </Form>
                            </Grid.Column>
                          </Grid>
                        </Popup>
                        <List divided relaxed>
                          {this.state.rondes.length > 0 ? (
                            this.state.rondes.map((ronde) => (
                              <List.Item
                                key={ronde.id}
                                onClick={() => this.rondeSelected(ronde.id)}
                                style={
                                  this.state.selectedRonde === ronde.id
                                    ? {
                                        background: "#a9d5de",
                                        boxShadow:
                                          "0 0 0 1px #a9d5de inset, 0 0 0 0 transparent",
                                      }
                                    : {}
                                }
                              >
                                <List.Icon
                                  name="r circle"
                                  size="large"
                                  verticalAlign="middle"
                                />
                                <List.Content>
                                  <List.Header as="a">{ronde.naam}</List.Header>
                                  <div>
                                    <List.Description
                                      as="a"
                                      style={{
                                        float: "left",
                                      }}
                                    >
                                      {"Updated "}
                                      <Moment add={{ hours: 1 }} fromNow>
                                        {ronde.updatedAt}
                                      </Moment>
                                    </List.Description>
                                    <Button
                                      icon
                                      onClick={() => this.deleteRound(ronde.id)}
                                      style={{ marginLeft: "40px" }}
                                    >
                                      <Icon
                                        name="trash"
                                        color="red"
                                        size="small"
                                      />
                                    </Button>
                                  </div>
                                </List.Content>
                              </List.Item>
                            ))
                          ) : (
                            <p>
                              No rounds in quiz. Add one by clicking on the plus
                              button above!
                            </p>
                          )}
                        </List>
                      </div>
                    ) : (
                      <div style={{ marginTop: "15%" }}>
                        <Message info size="tiny">
                          <Message.Header>
                            Click on a quiz to see your rounds!
                          </Message.Header>
                        </Message>
                      </div>
                    )}
                  </Grid.Column>
                  <Grid.Column width={4}>
                    {this.state.selectedRonde != null ? (
                      <div>
                        <h1 style={{ float: "left" }}>Questions</h1>
                        <Popup
                          trigger={
                            <Button icon style={{ marginLeft: "5%" }}>
                              <Icon name="plus" />
                            </Button>
                          }
                          on="click"
                          //style={{ width: "500px" }}
                        >
                          <Grid centered divided>
                            <Grid.Column textAlign="center">
                              {this.state.createQuestionMessage != null ? (
                                <Message color="green" size="mini">
                                  <Message.Header>
                                    {this.state.createQuestionMessage}
                                  </Message.Header>
                                </Message>
                              ) : (
                                <div></div>
                              )}
                              <Header as="h4">Make a new question</Header>
                              <Form onSubmit={this.addQuestion}>
                                <Input
                                  placeholder="Enter your question here"
                                  type="text"
                                  onChange={this.handleChangeQuestion}
                                  value={this.state.questionName}
                                ></Input>
                                {this.state.createQuestionError != null ? (
                                  <Message color="red" size="mini">
                                    <Message.Header>
                                      {this.state.createQuestionError}
                                    </Message.Header>
                                  </Message>
                                ) : (
                                  <div></div>
                                )}
                                <p></p>
                                <p>Max score</p>
                                <Input
                                  type="number"
                                  onChange={this.handleChangeQuestionMaxScore}
                                  value={this.state.questionMaxScore}
                                  required
                                  style={{ width: "100px" }}
                                ></Input>
                                <p></p>
                                <Form.Field>
                                  Selected type of question:
                                </Form.Field>
                                <Form.Field>
                                  <Radio
                                    label="Open question: "
                                    name="radioGroup"
                                    value="1"
                                    checked={this.state.questionTypeOf === "1"}
                                    onChange={this.handleChangeTypeQuestion}
                                  />
                                </Form.Field>
                                <Form.Field>
                                  <Radio
                                    label="Multiple choice question"
                                    name="radioGroup"
                                    value="2"
                                    checked={this.state.questionTypeOf === "2"}
                                    onChange={this.handleChangeTypeQuestion}
                                  />
                                </Form.Field>
                                <Form.Field>
                                  <Radio
                                    label="Ranking question"
                                    name="radioGroup"
                                    value="3"
                                    checked={this.state.questionTypeOf === "3"}
                                    onChange={this.handleChangeTypeQuestion}
                                  />
                                </Form.Field>
                                <Input type="submit" value="Submit"></Input>
                                {this.state.loadingAddQuestion ? (
                                  <Loader active inline size="mini" />
                                ) : (
                                  <div />
                                )}
                              </Form>
                            </Grid.Column>
                          </Grid>
                        </Popup>
                        <List divided relaxed>
                          {this.state.questions.length > 0 ? (
                            this.state.questions.map((question) => (
                              <List.Item
                                key={question.id}
                                onClick={() =>
                                  this.questionSelected(question.id)
                                }
                                style={
                                  this.state.selectedQuestion != null
                                    ? this.state.selectedQuestion.id ===
                                      question.id
                                      ? {
                                          background: "#a9d5de",
                                          boxShadow:
                                            "0 0 0 1px #a9d5de inset, 0 0 0 0 transparent",
                                        }
                                      : {}
                                    : {}
                                }
                              >
                                <List.Icon
                                  name="question"
                                  size="large"
                                  verticalAlign="middle"
                                />
                                <List.Content>
                                  <List.Header as="a">
                                    {question.vraagStelling}
                                  </List.Header>

                                  <List.Description
                                    as="a"
                                    style={{
                                      float: "left",
                                    }}
                                  >
                                    {"Updated "}
                                    <Moment add={{ hours: 1 }} fromNow>
                                      {question.updatedAt}
                                    </Moment>
                                  </List.Description>
                                  <Button
                                    icon
                                    onClick={() =>
                                      this.deleteQuestion(question.id)
                                    }
                                    style={{ marginLeft: "40px" }}
                                  >
                                    <Icon
                                      name="trash"
                                      color="red"
                                      size="small"
                                    />
                                  </Button>
                                </List.Content>
                              </List.Item>
                            ))
                          ) : (
                            <p>
                              No questions for this rounds. Add one by clicking
                              on the plus button above!
                            </p>
                          )}
                        </List>
                      </div>
                    ) : this.state.selectedQuiz != null ? (
                      <div style={{ marginTop: "15%" }}>
                        <Message info size="tiny">
                          <Message.Header>
                            Click on a round to see your questions!
                          </Message.Header>
                        </Message>
                      </div>
                    ) : (
                      <div></div>
                    )}
                  </Grid.Column>
                  <Grid.Column width={4}>
                    {this.state.selectedQuestion != null ? (
                      <div>
                        <h1 style={{ float: "left" }}>Answers</h1>
                        {this.state.selectedQuestion != null ? (
                          <AnswerPopUp
                            selectedQuestion={this.state.selectedQuestion}
                            updateCorrectAndWrongAnswers={
                              this.updateCorrectAndWrongAnswers
                            }
                            loadingAddAnswer={this.state.loadingAddAnswer}
                          />
                        ) : null}
                        <List divided relaxed>
                          {this.state.selectedQuestion.jsonCorrecteAntwoord !==
                          "" ? (
                            this.state.answers.map((answer, index) =>
                              answer !== "" ? (
                                <List.Item key={index}>
                                  <List.Icon
                                    name="pencil"
                                    size="large"
                                    verticalAlign="middle"
                                  />
                                  <List.Content>
                                    <List.Header as="a">{answer}</List.Header>
                                    <List.Description as="a">
                                      {"Updated "}
                                      <Moment fromNow>
                                        {answer.updatedAt}
                                      </Moment>
                                    </List.Description>
                                    <Button
                                      icon
                                      onClick={() => this.deleteAnswer(index)}
                                      style={{ marginLeft: "40px" }}
                                    >
                                      <Icon
                                        name="trash"
                                        color="red"
                                        size="small"
                                      />
                                    </Button>
                                  </List.Content>
                                </List.Item>
                              ) : null
                            )
                          ) : (
                            <p>
                              No answers for this question. Add one by clicking
                              on the plus button above!
                            </p>
                          )}
                        </List>
                      </div>
                    ) : this.state.selectedQuiz != null &&
                      this.state.selectedRonde != null ? (
                      <div style={{ marginTop: "15%" }}>
                        <Message info size="tiny">
                          <Message.Header>
                            Click on a questions to see your answers!
                          </Message.Header>
                        </Message>
                      </div>
                    ) : (
                      <div></div>
                    )}
                  </Grid.Column>
                </Grid>
              </div>
            ) : (
              <div></div>
            )}
          </div>
        ) : (
          <LoginNeeded redirect={this.props.redirect}></LoginNeeded>
        )}
      </div>
    );
  }
}
export default CreateFullQuiz;
