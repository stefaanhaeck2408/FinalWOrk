import { IQuiz } from "../../../app/models/quiz";
import QuizStore from "../../../app/stores/quizStore";
import React, { useContext } from "react";
import { Segment, Button } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { observer } from "mobx-react-lite";

const QuizListItem: React.FC<{ quiz: IQuiz }> = ({ quiz }) => { 
  const quizStore = useContext(QuizStore);
  const {deleteQuiz, submitting, target} = quizStore;

  return (
    <Segment.Group>
      <Segment clearing>{quiz.naam}
      
        <Button
          as={Link}
          to={`/quizes/${quiz.id}`}
          floated="right"
          content="View"
          color="blue"
        />
        <Button
          name={quiz.id}
          loading={target === quiz.id && submitting}
          onClick={e => deleteQuiz(e, quiz.id!)}
          floated="right"
          content="Delete"
          color="red"
        />
      </Segment>
    </Segment.Group>
  );
};

export default observer(QuizListItem);
