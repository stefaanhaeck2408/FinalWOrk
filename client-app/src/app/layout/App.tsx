import React, { Fragment } from "react";
import "../layout/App.css";
import { RouteComponentProps, Route, withRouter } from "react-router";
import { NavBar } from "../../features/nav/navbar";
import { Container } from "semantic-ui-react";
import HomePage from "../../features/HomePage/HomePage";
import { observer } from "mobx-react-lite";
import CreateFullQuiz from "../../features/quiz/CreateFullQuiz.jsx";
import Client from "../../features/quiz/Components/Client.jsx";
import Host from "../../features/quiz/Components/Host.js";
import InviteTeams from "../../features/quiz/Components/InviteTeams.jsx";
import "../../../node_modules/react-grid-layout/css/styles.css";
import "../../../node_modules/react-resizable/css/styles.css";
import { useAuth0 } from "../../react-auth0-spa";
import Gdpr from "../../features/quiz/Components/Account/gdpr";

const App: React.FC<RouteComponentProps> = ({ location }) => {

  const { user,isAuthenticated, loginWithRedirect } = useAuth0();

  return (
    <Fragment>
      <Route exact path="/" component={HomePage} />
      <Route
        path={"/(.+)"}
        render={() => (
          <Fragment>
            <NavBar />
            <Container style={{ marginTop: "7em" }}>
              <Route exact path="/createFullQuiz" render={(props) => (
                  <CreateFullQuiz {...props} user={user} isAuthenticated={isAuthenticated} redirect={loginWithRedirect}/>
                )} />
            </Container>
            <Container style={{ marginTop: "7em" }}>
              <Route exact path="/inviteTeams" render={(props) => (
                  <InviteTeams {...props} user={user} isAuthenticated={isAuthenticated} redirect={loginWithRedirect}/>
                )} />
            </Container>            
            <Container style={{ marginTop: "7em" }}>
              <Route
                path='/host'
                render={(props) => (
                  <Host {...props} user={user} isAuthenticated={isAuthenticated} redirect={loginWithRedirect}/>
                )}
              />
            </Container>
            <Container style={{ marginTop: "7em" }}>              
              <Route
                path='/client'
                render={(props) => (
                  <Client {...props} user={user} isAuthenticated={isAuthenticated}/>
                )}
              />
            </Container>               
            <Container style={{ marginTop: "7em" }}>              
              <Route
                path='/gdpr'
                render={(props) => (
                  <Gdpr {...props} user={user}/>
                )}
              />
            </Container>          
          </Fragment>
        )}
      />
    </Fragment>
  );
};

export default withRouter(observer(App));
