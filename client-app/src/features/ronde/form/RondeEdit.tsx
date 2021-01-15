import React, { useContext, useState, ChangeEvent } from "react";
import { Grid, Segment, Form, Label, Button } from "semantic-ui-react";
import RondeStore from "../../../app/stores/rondeStore";
import { IRonde } from "../../../app/models/ronde";

const RondeEdit = () => {
  const rondeStore = useContext(RondeStore);
  const { editRonde, ronde, submitting, switchEditMode } = rondeStore;

  const [rondeEdit, setRondeEdit] = useState<IRonde>({
    id: ronde!.id,
    naam: ronde!.naam
  });

  const handleSubmit = () => {
    console.log(rondeEdit);
    editRonde(rondeEdit!);
  };

  const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
    //console.log(event.target.value);
    setRondeEdit({ ...rondeEdit!, [event.target.name]: event.target.value });
  };

  return (
    <Grid>
      <Grid.Column width={16}>
        <Segment clearing>
          <Form onSubmit={handleSubmit}>
            <Label>Naam:</Label>
            <Form.Input
              onChange={handleInputChange}
              name="naam"
              placeholder="Naam"
              value={rondeEdit!.naam}
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
                onClick={switchEditMode}
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

export default RondeEdit;
