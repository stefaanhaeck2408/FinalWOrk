import React from "react";
import { Menu, Container, Button } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { NavLink } from "react-router-dom";
import { useAuth0 } from "../../react-auth0-spa";

export const NavBar: React.FC = () => {

  const { isAuthenticated, loginWithRedirect, logout } = useAuth0();
  
  return (
    <Menu fixed="top" inverted>
      <Container>
        <Menu.Item header as={NavLink} exact to="/">
          <img
            src="/assets/images/logo.png"
            alt="logo"
            style={{ marginRight: "10px" }}
          />
        </Menu.Item>  
        {isAuthenticated && (<Menu.Item name="Create your own quiz" as={NavLink} to="/createFullQuiz"/>)}      
        {isAuthenticated && (<Menu.Item name="Invite teams to your quiz" as={NavLink} to="/inviteTeams"/> )}
        {isAuthenticated && (               
        <Menu.Item name="Quiz Master" as={NavLink} to="/host"/>  
        )}
        <Menu.Item name="Play a quiz" as={NavLink} to="/client"/>          
          {!isAuthenticated && (
            <Menu.Item>
            <Button style={{background: "#41c4de" }} onClick={() => loginWithRedirect({})} positive content="Login/Register" />
          </Menu.Item>
          )} 
        <div>
          {isAuthenticated && (
            <Menu.Item>
            <Button style={{background: "rgb(89 216 241)" }} onClick={() => logout()} positive content="Logout" />
          </Menu.Item>
          )}
          
        </div>
      </Container>
    </Menu>
  );
};

export default observer(NavBar);
