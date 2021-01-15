import { useContext, useEffect } from "react";
import QuizStore from "../../../app/stores/quizStore";
import React from "react";
import { observer } from "mobx-react-lite";
import { Grid } from "semantic-ui-react";
import QuizList from "./QuizList";
import LoadingComponent from "../../../app/layout/LoadingComponent";

const QuizDashboard: React.FC = () => {
  const quizStore = useContext(QuizStore);

  useEffect(() => {
    quizStore.loadQuizes();
  }, [quizStore]);
  
  

  if(quizStore.loadingInitial)
    return <LoadingComponent content="Loading quizes..."/>

  return (
    <div>
      <h1>QuizDashboard</h1>
      <Grid>
        <Grid.Column width={10}>
          <QuizList />
        </Grid.Column>
        <Grid.Column>
          <h2>Quiz filters</h2>
        </Grid.Column>
      </Grid>
    </div>
  );
};

export default observer(QuizDashboard);
