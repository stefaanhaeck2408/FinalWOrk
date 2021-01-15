import { useContext, useEffect } from "react";
import VraagStore from "../../../app/stores/vraagStore";
import React from "react";
import { observer } from "mobx-react-lite";
import { Grid } from "semantic-ui-react";
import VraagList from "./VraagList";
import LoadingComponent from "../../../app/layout/LoadingComponent";

const VraagDashboard: React.FC = () => {
  const vraagStore = useContext(VraagStore);

  useEffect(() => {
    vraagStore.loadVragen();
  }, [vraagStore]);

  if(vraagStore.loadingInitial)
    return <LoadingComponent content="Loading vragen..."/>


  return (
    <div>
      <h1>VraagDashboard</h1>

      <Grid>
        <Grid.Column width={10}>
          <VraagList />
        </Grid.Column>
        <Grid.Column>
          <h2>Vraag filters</h2>
        </Grid.Column>
      </Grid>
    </div>
  );
};

export default observer(VraagDashboard);
