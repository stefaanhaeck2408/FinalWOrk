import React, { useContext, useState, ChangeEvent } from "react";
import { Grid, Segment, Form, Label, Button } from "semantic-ui-react";
import TeamStore from "../../../app/stores/teamStore";
import { ITeam } from "../../../app/models/team";

const TeamEdit = () => {

    const teamStore = useContext(TeamStore);
    const { editTeam, team, submitting, switchEditMode } = teamStore;
  
    const [teamEdit, setTeamEdit] = useState<ITeam>({
      id: team!.id,
      naam: team!.naam
    });
  
    const handleSubmit = () => {
      console.log(editTeam);
      editTeam(teamEdit!);
    };

    const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        //console.log(event.target.value);
        setTeamEdit({ ...teamEdit!, [event.target.name]: event.target.value });
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
              value={teamEdit!.naam}
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

export default TeamEdit;
