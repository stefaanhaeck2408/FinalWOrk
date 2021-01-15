import { RouteComponentProps } from "react-router";
import React, { useContext, useEffect } from "react";
import { observer } from "mobx-react-lite";
import VraagStore from "../../../app/stores/vraagStore";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Grid, Card, Image, Button } from "semantic-ui-react";
import VraagEdit from "../form/VraagEdit";

interface DetailsParams {
    id: string
}

const VraagDetails: React.FC<RouteComponentProps<DetailsParams>> = ({
    match,
    history
}) => {

    const vraagStore = useContext(VraagStore);
    const {
      vraag,
      loadVraag,
      loadingInitial,
      editMode,
      switchEditMode
    } = vraagStore;
  
    useEffect(() => {
      loadVraag(Number(match.params.id));
    }, [loadVraag, match.params.id]);

    
  
    if (loadingInitial || !vraag)
      return <LoadingComponent content="Loading vraag..." />;
    return (
        <div>
        <Grid>
          <Grid.Column width={10}>
            <Card>
              <Image src="/assets/images/vraag.jpg" wrapped ui={false} />
              <Card.Content>
                <Card.Header>{vraag.vraagStelling}</Card.Header>
                <Card.Meta>
    <span className="date">Max score op deze vraag: {vraag.maxScoreVraag}</span>
                </Card.Meta>
                <Card.Description>Type vraag: {vraag.typeVraagId === 1 ? "Openvraag" : vraag.typeVraagId === 2 ? "Meerkeuzevraag" : "Reeksvraag"  }</Card.Description>
              </Card.Content>
              <Card.Content extra>
                
                Mogelijke antwoorden: {vraag.jsonMogelijkeAntwoorden} <br></br>
                Correcte antwoord: {vraag.jsonCorrecteAntwoord}
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
          <Grid.Column width={6}>{editMode && <VraagEdit/>}</Grid.Column>
        </Grid>
      </div>
    );
}

export default observer(VraagDetails);

