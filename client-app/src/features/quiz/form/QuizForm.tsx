import { RouteComponentProps } from "react-router";
import { observer } from "mobx-react-lite";
import React, { useContext, useState, useEffect, FormEvent } from "react";
import QuizStore from "../../../app/stores/quizStore";
import { Grid, Segment, Form, Button } from "semantic-ui-react";
import { IQuizRequest } from "../../../app/models/quizRequest";

interface DetailParams {
  id: string;
}

const QuizForm: React.FC<RouteComponentProps<DetailParams>> = ({
  match,
  history
}) => {
  const quizStore = useContext(QuizStore);
  const {
    createQuiz,
    submitting,
    quiz: initialFormState,
    loadQuiz,
    clearQuiz
  } = quizStore;

  const [quiz, setQuiz] = useState<IQuizRequest>({
    naam: ""
  });

  useEffect(() => {
    if (match.params.id === null) {
      let number = Number(match.params.id);
      loadQuiz(number).then(
        () => initialFormState && setQuiz(initialFormState)
      );
    }
    return () => {
      clearQuiz();
    };
  }, [loadQuiz, clearQuiz, match.params.id, initialFormState]);

  const handleSubmit = () => {
    console.log("test");
    createQuiz(quiz).then(() => history.push(`/quizes`));
  };

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    const { value } = event.currentTarget;
    setQuiz({ naam: value });
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
                onClick={() => history.push("/quizes")}
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

export default observer(QuizForm);
