import { RouteComponentProps } from "react-router";
import React, { useContext, useEffect } from "react";
import { observer } from "mobx-react-lite";
import { Grid, Card, Image, Icon, Button } from "semantic-ui-react";
import RondeStore from "../../../app/stores/rondeStore";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import RondeEdit from "../form/RondeEdit";

interface DetailsParams {
  id: string;
}

const RondeDetails: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history

}) => {
    const rondeStore = useContext(RondeStore);
  const {
    ronde,
    loadRonde,
    loadingInitial,
    editMode,
    switchEditMode
  } = rondeStore;

  useEffect(() => {
    loadRonde(Number(match.params.id));
  }, [loadRonde, match.params.id]);

  if (loadingInitial || !ronde)
    return <LoadingComponent content="Loading ronde..." />;
  return (
    <div>
      <Grid>
        <Grid.Column width={10}>
          <Card>
            <Image src="/assets/images/ronde.png" wrapped ui={false} />
            <Card.Content>
              <Card.Header>{ronde.naam}</Card.Header>
              <Card.Meta>
                <span className="date">Plaats voor extra info</span>
              </Card.Meta>
              <Card.Description>Beschrijving</Card.Description>
            </Card.Content>
            <Card.Content extra>
              <Icon name="user" />
              Misschien handig om het aantal quizen waarin deze ronde werd gebruikt in te plaatsen of een samenvatting van vragen
            </Card.Content>
            <Card.Content extra>
              <Button.Group widths={2}>
                <Button
                  onClick={() => switchEditMode()}
                  basic
                  color="blue"
                  content="Edit"
                />
                <Button
                  basic
                  color="grey"
                  content="Cancel"
                  onClick={() => history.push("/rondes")}
                />
              </Button.Group>
            </Card.Content>
          </Card>
        </Grid.Column>
        <Grid.Column width={6}>{editMode && <RondeEdit />}</Grid.Column>
      </Grid>
    </div>
  );
};

export default observer(RondeDetails);
