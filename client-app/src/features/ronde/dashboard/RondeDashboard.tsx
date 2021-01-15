import { useContext, useEffect } from "react";
import RondeStore from "../../../app/stores/rondeStore";
import React from "react";
import { observer } from "mobx-react-lite";
import { Grid } from "semantic-ui-react";
import RondeList from "./RondeList";
import LoadingComponent from "../../../app/layout/LoadingComponent";

const RondeDashboard: React.FC = () => {
  const rondeStore = useContext(RondeStore);

  useEffect(() => {
    rondeStore.loadRondes();
  }, [rondeStore]);

  if(rondeStore.loadingInitial)
    return <LoadingComponent content="Loading rondes..."/>


  return (
    <div>
      <h1>RondeDashboard</h1>
      <Grid>
        <Grid.Column width={10}>
          <RondeList />
        </Grid.Column>
        <Grid.Column>
          <h2>Ronde filters</h2>
        </Grid.Column>
      </Grid>
    </div>
  );
};

export default observer(RondeDashboard);
