import React, { Component } from "react";
import {
  List,
  Grid,
  Header,
  Icon,
  Button,
  Popup,
  Form,
  Input,
  Label,
  Dropdown,
  TextArea,
  Message,
  Loader
} from "semantic-ui-react";
import axios from "axios";
import LoginNeeded from "./LoginNeeded.jsx";
import Moment from "react-moment";
import "moment-timezone";
import LoadingComponent from "../../../app/layout/LoadingComponent";


export class InviteTeams extends Component {
  state = {
    teams: null,
    inviteMessage: "",
    selectedQUiz: null,
    paypalEmailLink: "",
    PaypalLinkInvalid: false,
    invalidMessage: false,
    sendInvationsSuccess: false,
    teamName: "",
    teamEmail: "",
    addTeamLoading: false,
    teamAddedSucces: false,
    SendingInvitesLoader: false,        
    
  };

  componentDidMount() {}

  getAllTeams = () => {
    this.setState({ loadingTeams: true});
    fetch(
      "https://makeaquizapi.azurewebsites.net/api/Team/GetAllTeamsFromOneUser/" +
        this.props.user.email
    )
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            teams: result,
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
  addTeam = async () => {
    this.setState({addTeamLoading : true})

    console.log("the value is: " + this.state.teamName);

    await axios
      .post("https://makeaquizapi.azurewebsites.net/api/Team/Create", {
        naam: this.state.teamName,
        emailCreator: this.props.user.email,
        email: this.state.teamEmail,
      })
      .then((response) =>
        this.setState({
          teams: [
            ...this.state.teams,
            {
              email: response.data.email,
              emailCreator: response.data.emailCreator,
              id: response.data.id,
              naam: response.data.naam,
              pin: response.data.pin,
              quizId: response.data.quizId,
              updatedAt: response.data.updatedAt,
            },
          ],
          teamAddedSucces: true
        })
      );

      this.setState({addTeamLoading : false, teamName: "", teamEmail: ""})

  };

  invite = async () => {
    console.log("this message needs te be sent: " + this.state.inviteMessage);
    this.setState({SendingInvitesLoader: true})
    if (
      this.state.PaypalLinkInvalid === false &&
      this.state.invalidMessage === false
    ) {
      await axios
        .post("https://makeaquizapi.azurewebsites.net/api/Quiz/SendInvitations", {
          QuizId: this.state.selectedQuiz.id,
          Message: this.state.inviteMessage,
          PaypalLink: this.state.paypalEmailLink,
        })
        .then((response) => {
          if(response.status === 200){
            this.setState({inviteMessage: "", paypalEmailLink: "", sendInvationsSuccess: true})
          }
        });
    }
  };

  handleChangeTeamName = (event) => {
    this.setState({ teamName: event.target.value });
  };

  handleChangeTeamEmail = (event) => {
    this.setState({ teamEmail: event.target.value });
  };

  handleChangeQuiz = (event, data) => {
    fetch("https://makeaquizapi.azurewebsites.net/api/Quiz/GetById/" + data.value)
      .then((res) => res.json())
      .then((result) => {
        this.setState({ selectedQuiz: result });
      });
    //this.setState({ selectedQuiz: data.value });
    this.setState({sendInvationsSuccess : false})
    this.GetAllTeamsForQuiz(data.value);
  };

  handleChangeInviteMessage = (event) => {
    var message = event.target.value;
    var invalidMessage = message.includes(
      "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id="
    );

    if (invalidMessage) {
      this.setState({ invalidMessage: true });
    } else {
      this.setState({ invalidMessage: false });
    }
    this.setState({ inviteMessage: event.target.value });
  };

  handleChangePaypalEmailLink = (event) => {
    var paypalEmail = event.target.value;

    var validLink = paypalEmail.startsWith(
      "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id="
    );
    var validLength = paypalEmail.length === 82;
    console.log(validLength);
    if (validLink && validLength) {
      this.setState({ PaypalLinkInvalid: false });
    } else {
      this.setState({ PaypalLinkInvalid: true });
    }
    this.setState({ paypalEmailLink: event.target.value });
  };

  GetAllTeamsForQuiz = (quizId) => {
    fetch("https://makeaquizapi.azurewebsites.net/api/Quiz/GetAllTeamsFromQuiz/" + quizId)
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            TeamsInSelectedQuiz: result,
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

  AddTeamToQuiz = async (TeamId, TeamName) => {
    console.log("Adding team with id " + TeamId + " to a quiz!");
    this.setState({loading: true, loadingContent: "Adding team from quiz.."})

    if (this.state.TeamsInSelectedQuiz != null) {
      let teamAllreadyExists = this.state.TeamsInSelectedQuiz.filter(
        (x) => x.naam === TeamName
      );

      console.log(teamAllreadyExists);
      if (teamAllreadyExists.length === 0) {
        await axios
          .post("https://makeaquizapi.azurewebsites.net/api/Quiz/AddTeamToQuiz", {
            quizId: this.state.selectedQuiz.id,
            teamId: TeamId,
          })
          .then((response) =>
            this.setState({
              TeamsInSelectedQuiz: [
                ...this.state.TeamsInSelectedQuiz,
                { id: TeamId, naam: TeamName },
              ],
            })
          );
      }
    }
    this.setState({loading: false, loadingContent: ""})
  };

  DeleteTeamFromQuiz = async (TeamId) => {
    this.setState({loading: true, loadingContent: "Removing team from quiz.."})
    console.log(
      "Deleting team with id " +
        TeamId +
        " from quiz " +
        this.state.selectedQuiz.id
    );

    await axios
      .post("https://makeaquizapi.azurewebsites.net/api/Quiz/DeleteTeamFromQuiz", {
        quizId: this.state.selectedQuiz.id,
        teamId: TeamId,
      })
      .then((response) =>
        this.setState({
          TeamsInSelectedQuiz: this.state.TeamsInSelectedQuiz.filter(
            (team) => team.id !== TeamId
          ),
        })
      );

      this.setState({loading: false, loadingContent: ""})
  };

  getData = () => {    
    
    this.getAllTeams();
    this.getAllQuizes();
    this.setState({ loadingTeams: false});
  };

  userPaidForSelectedQuiz = (quiz) => {
    this.setState({ selectedQUiz: quiz });
  };

  render() {
    if (this.state.loading === true) {
      return <LoadingComponent content={this.state.loadingContent} />;
    }
    return (
      <div>
        {this.props.isAuthenticated ? (
          <div>
            <h1>Invite teams</h1>
            {this.state.teams === null ? (
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
                onClick={this.getData}
                style={{}}
              >
                Get all your teams
              </Button>
              {this.state.loadingTeams ? <Loader active inline size="large" /> : <div/>}
            </div>
            ) : (
              <div>
                <Grid>
                  <Grid.Column width={5}>
                    <h3 style={{ float: "left" }}>Teams</h3>

                    <Popup
                      trigger={
                        <Button icon>
                          <Icon name="plus" />
                        </Button>
                      }
                      on="click"
                    >
                      <Grid centered divided>
                        <Grid.Column textAlign="center">
                        {this.state.teamAddedSucces != false ? (
                                <Message color="green" size="mini">
                                  <Message.Header>
                                    Team created!
                                  </Message.Header>
                                </Message>
                              ) : (
                                <div></div>
                              )}
                          <Header as="h4">Make a new team</Header>
                          <Form onSubmit={this.addTeam}>
                                                         
                              <Input
                                placeholder="Enter teamname here..."
                                type="text"
                                onChange={this.handleChangeTeamName}
                                required
                                value={this.state.teamName}
                              ></Input>
                            
                            <p></p>
                            <div>                              
                              <Input
                                placeholder="Enter team email here..."
                                type="email"
                                onChange={this.handleChangeTeamEmail}
                                required
                                value={this.state.teamEmail}
                              ></Input>
                            </div>
                            <p></p>
                            <Input type="submit" value="Submit"></Input>
                            {this.state.addTeamLoading ? <Loader active inline size='mini'/> : <div/>}
                          </Form>
                        </Grid.Column>
                      </Grid>
                    </Popup>
                    {this.state.teams.length > 0 ? <List divided relaxed>
                      {this.state.teams.map((team) => (
                        <List.Item key={team.id}>
                          <List.Icon
                            name="group"
                            size="large"
                            verticalAlign="middle"
                          />
                          <List.Content>
                      <List.Header as="a">{team.naam}</List.Header>
                            <div>
                              <List.Description
                                as="a"
                                style={{ float: "left", paddingRight: "50px" }}
                              >
                                {"Updated "}
                                <Moment fromNow>{team.updatedAt}</Moment>
                              </List.Description>
                              <Button
                                icon
                                onClick={() =>
                                  this.AddTeamToQuiz(team.id, team.naam)
                                }
                              >
                                <Icon
                                  name="arrow right"
                                  style={{ color: "red" }}
                                />
                              </Button>
                            </div>
                          </List.Content>
                        </List.Item>
                      ))}
                    </List> : <div><Message info size="tiny" style={{top:"10px"}}>
                      <Message.Header>
                        No teams in list yet, start by adding one above.
                      </Message.Header>
                    </Message></div>}
                    
                  </Grid.Column>
                  <Grid.Column width={5}>
                    Teams in quiz with name
                    <Dropdown
                      placeholder="select a quiz"
                      inline
                      options={this.state.quizes}
                      onChange={this.handleChangeQuiz}
                    />
                    {this.state.TeamsInSelectedQuiz != null ? (
                      <List divided relaxed>
                        {this.state.TeamsInSelectedQuiz.length > 0 ? this.state.TeamsInSelectedQuiz.map((team) => (
                          <List.Item key={team.id}>
                            <List.Icon
                              name="group"
                              size="large"
                              verticalAlign="middle"
                            />
                            <List.Content>
                              <List.Header as="a">{team.naam}</List.Header>
                              <div>
                                <List.Description
                                  as="a"
                                  style={{
                                    float: "left",
                                    paddingRight: "50px",
                                  }}
                                >
                                  {"Updated "}
                                  {/*<Moment fromNow>{team.updatedAt}</Moment>*/}
                                </List.Description>
                                <Button
                                  icon
                                  onClick={() =>
                                    this.DeleteTeamFromQuiz(team.id)
                                  }
                                >
                                  <Icon name="trash alternate" />
                                </Button>
                              </div>
                            </List.Content>
                          </List.Item>
                        )): <div><Message info size="tiny" style={{top:"10px"}}>
                        <Message.Header>
                          No teams in this quiz, add one by clicking the red arrow next to the team
                        </Message.Header>
                      </Message></div>}
                      </List>
                    ) : (
                      <div><Message info size="tiny" style={{top:"10px"}}>
                      <Message.Header>
                        Select a quiz above to add teams to the quiz!
                      </Message.Header>
                    </Message></div>
                    )}
   
                  </Grid.Column>
                  <Grid.Column width={5}>
                    {this.state.TeamsInSelectedQuiz != null ? this.state.TeamsInSelectedQuiz.length > 0 ? this.state.sendInvationsSuccess === false ?
                    this.state.selectedQuiz != null ? (
                        <div>
                          <h3>Sending invites</h3>
                          <Form onSubmit={this.invite}>
                            <div>
                              <Label>Message</Label>
                              {this.state.invalidMessage ? (
                                <Message negative>
                                  <Message.Header>
                                    Sorry you text message cannot contain a
                                    paypal link.
                                  </Message.Header>
                                </Message>
                              ) : (
                                <div></div>
                              )}

                              <TextArea
                                type="text"
                                onChange={this.handleChangeInviteMessage}
                                required
                                value={this.state.inviteMessage}
                              ></TextArea>                              
                            </div>
                            <Input type="submit" value="Submit"></Input>
                            {this.state.SendingInvitesLoader ? <Loader active inline size='mini'/> : <div/>}
                          </Form>
                        </div>
                      ) 
                     : (
                      <div></div>
                    ): <Message positive><Message.Header>Invitations sent successfully!</Message.Header></Message> : <div></div> : <div></div>}
                  </Grid.Column>
                </Grid>
              </div>
            )}
          </div>
        ) : (
          <LoginNeeded redirect={this.props.redirect}></LoginNeeded>
        )}
      </div>
    );
  }
}

export default InviteTeams;
