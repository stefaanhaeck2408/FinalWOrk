import { RouteComponentProps } from "react-router";
import { observer } from "mobx-react-lite";
import React, { useContext, useState, useEffect, FormEvent } from "react";
import { Grid, Segment, Form, Button } from "semantic-ui-react";
import TeamStore from "../../../app/stores/teamStore";
import { ITeamRequest } from "../../../app/models/teamRequest";

interface DetailParams {
  id: string;
}

const TeamForm: React.FC<RouteComponentProps<DetailParams>> = ({
  match,
  history
}) => {
  const teamStore = useContext(TeamStore);
  const {
    createTeam,
    submitting,
    team: initialFormState,
    loadTeam,
    clearTeam
  } = teamStore;

  const [team, setTeam] = useState<ITeamRequest>({
    naam: ""
  });

  useEffect(() => {
    if (match.params.id === null) {
      let number = Number(match.params.id);
      loadTeam(number).then(
        () => initialFormState && setTeam(initialFormState)
      );
    }
    return () => {
      clearTeam();
    };
  }, [loadTeam, clearTeam, match.params.id, initialFormState]);

  const handleSubmit = () => {
    //console.log("test");
    createTeam(team).then(() => history.push(`/teams`));
  };

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    const { value } = event.currentTarget;
    setTeam({ naam: value });
  };

  return (
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

export default observer(TeamForm);
