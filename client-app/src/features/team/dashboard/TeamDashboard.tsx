import { useContext, useEffect } from "react";
import TeamStore from "../../../app/stores/teamStore";
import React from "react";
import { observer } from "mobx-react-lite";
import { Grid } from "semantic-ui-react";
import TeamList from "./TeamList";
import LoadingComponent from "../../../app/layout/LoadingComponent";

const TeamDashboard: React.FC = () => {
  const teamStore = useContext(TeamStore);

  useEffect(() => {
    teamStore.loadTeams();
  }, [teamStore]);

  if(teamStore.loadingInitial)
    return <LoadingComponent content="Loading teams..."/>


  return (
    <div>
      <h1>TeamDashboard</h1>
      <Grid>
        <Grid.Column width={10}>
          <TeamList />
        </Grid.Column>
        <Grid.Column>
          <h2>Team filters</h2>
        </Grid.Column>
      </Grid>
    </div>
  );
};

export default observer(TeamDashboard);
