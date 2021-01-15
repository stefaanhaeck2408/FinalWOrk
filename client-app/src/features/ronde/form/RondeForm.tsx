import { RouteComponentProps } from "react-router";
import { observer } from "mobx-react-lite";
import React, { useContext, useState, useEffect, FormEvent } from "react";
import { Grid, Segment, Form, Button } from "semantic-ui-react";
import RondeStore from "../../../app/stores/rondeStore";
import { IRondeRequest } from "../../../app/models/RondeRequest";

interface DetailParams {
    id: string;
}

const RondeForm: React.FC<RouteComponentProps<DetailParams>> = ({
    match, history
}) => {
    const rondeStore = useContext(RondeStore);
    const {
      createRonde,
      submitting,
      ronde: initialFormState,
      loadRonde,
      clearRonde
    } = rondeStore;
  
    const [ronde, setRonde] = useState<IRondeRequest>({
      naam: ""
    });
  
    useEffect(() => {
      if (match.params.id === null) {
        let number = Number(match.params.id);
        loadRonde(number).then(
          () => initialFormState && setRonde(initialFormState)
        );
      }
      return () => {
        clearRonde();
      };
    }, [loadRonde, clearRonde, match.params.id, initialFormState]);
  
    const handleSubmit = () => {
      //console.log("test");
      createRonde(ronde).then(() => history.push(`/rondes`));
    };
  
    const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
      const { value } = event.currentTarget;
      setRonde({ naam: value });
    };

    return(
        <Grid>
        <Grid.Column width={10}>
          <Segment clearing>
            <Form onSubmit={handleSubmit}>
              <Form.Input
                onChange={handleInputChange}
                name="naam"
                placeholder="Naam"
              />
              <Button.Group widths={2}>
                <Button
                  loading={submitting}
                  floated="right"
                  positive
                  type="submit"
                  content="Submit"
                />
                <Button
                  onClick={() => history.push("/teams")}
                  floated="right"
                  type="button"
                  content="Cancel"
                />
              </Button.Group>
            </Form>
          </Segment>
        </Grid.Column>
      </Grid>
    );
};

export default observer(RondeForm);