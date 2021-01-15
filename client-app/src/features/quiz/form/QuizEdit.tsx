import React, { useContext, useState, ChangeEvent } from "react";
import { observer } from "mobx-react-lite";
import { Grid, Segment, Form, Button, Label } from "semantic-ui-react";
import QuizStore from "../../../app/stores/quizStore";
import { IQuiz } from "../../../app/models/quiz";

const QuizEdit: React.FC = () => {
  const quizStore = useContext(QuizStore);
  const { editQuiz, quiz, submitting, switchEditMode } = quizStore;

  const [quizEdit, setQuizEdit] = useState<IQuiz>({
    id: quiz!.id,
    naam: quiz!.naam
  });

  const handleSubmit = () => {
    console.log(quizEdit);
    editQuiz(quizEdit!);
  };

  /*
  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    const {  value } = event.currentTarget;
    setQuiz({ id: quiz.quiz.id, naam: value });
  };*/

  const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
    //console.log(event.target.value);
    setQuizEdit({ ...quizEdit!, [event.target.name]: event.target.value });
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
              value={quizEdit!.naam}
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

export default observer(QuizEdit);
