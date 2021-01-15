import { RouteComponentProps } from "react-router";
import React, { useContext, useEffect } from "react";
import { observer } from "mobx-react-lite";
import TeamStore from "../../../app/stores/teamStore";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Grid, Card, Image, Icon, Button } from "semantic-ui-react";
import TeamEdit from "./TeamEdit";

interface DetailsParams {
    id: string
}

const TeamDetails: React.FC<RouteComponentProps<DetailsParams>> = ({
    match,
    history
}) => {
    const teamStore = useContext(TeamStore);
    const {
      team,
      loadTeam,
      loadingInitial,
      editMode,
      switchEditMode
    } = teamStore;
  
    useEffect(() => {
      loadTeam(Number(match.params.id));
    }, [loadTeam, match.params.id]);
  
    if (loadingInitial || !team)
      return <LoadingComponent content="Loading team..." />;

    return (
        <div>
        <Grid>
          <Grid.Column width={10}>
            <Card>
              <Image src="/assets/images/Teams.jpg" wrapped ui={false} />
              <Card.Content>
                <Card.Header>{team.naam}</Card.Header>
                <Card.Meta>
                  <span className="date">Plaats voor extra info</span>
                </Card.Meta>
                <Card.Description>Beschrijving</Card.Description>
              </Card.Content>
              <Card.Content extra>
                <Icon name="user" />
                Misschien handig om het aantal quizen waaraan team heeft deel genomen te plaatsen
              </Card.Content>
              <Card.Content extra>
                <Button.Group widths={2}>
                  <Button
                    onClick={() => switchEditMode()}
                    basic
                    color="blue"
                    content="Edit"
                  />
                  <Button basic color="grey" content="Cancel" onClick={() => history.push("/teams")} />
                </Button.Group>
              </Card.Content>
            </Card>
          </Grid.Column>
          <Grid.Column width={6}>{editMode && <TeamEdit/>}</Grid.Column>
        </Grid>
      </div>
    );
}

export default observer(TeamDetails);

