import { RouteComponentProps } from "react-router";
import React, { useContext, useEffect } from "react";
import { observer } from "mobx-react-lite";
import { Card, Image, Icon, Button, Grid } from "semantic-ui-react";
import QuizStore from "../../../app/stores/quizStore";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import QuizEdit from "../form/QuizEdit";

interface DetailsParams {
  id: string;
}

const QuizDetails: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history
}) => {
  const quizStore = useContext(QuizStore);
  const {
    quiz,
    loadQuiz,
    loadingInitial,
    editMode,
    switchEditMode
  } = quizStore;

  useEffect(() => {
    loadQuiz(Number(match.params.id));
  }, [loadQuiz, match.params.id]);

  if (loadingInitial || !quiz)
    return <LoadingComponent content="Loading quiz..." />;

  return (
    <div>
      <Grid>
        <Grid.Column width={10}>
          <Card>
            <Image src="/assets/images/quiz.jpg" wrapped ui={false} />
            <Card.Content>
              <Card.Header>{quiz.naam}</Card.Header>
              <Card.Meta>
                <span className="date">Plaats voor extra info</span>
              </Card.Meta>
              <Card.Description>Beschrijving</Card.Description>
            </Card.Content>
            <Card.Content extra>
              <Icon name="user" />
              Misschien handig om het aantal teams in te plaatsen
            </Card.Content>
            <Card.Content extra>
              <Button.Group widths={2}>
                <Button
                  onClick={() => switchEditMode()}
                  basic
                  color="blue"
                  content="Edit"
                />
                <Button basic color="grey" content="Cancel" onClick={() => history.push("/quizes")} />
              </Button.Group>
            </Card.Content>
          </Card>
        </Grid.Column>
        <Grid.Column width={6}>{editMode && <QuizEdit/>}</Grid.Column>
      </Grid>
    </div>
  );
};

export default observer(QuizDetails);
