import React, { useContext, Fragment } from "react";
import { observer } from 'mobx-react-lite';
import QuizStore from "../../../app/stores/quizStore";
import QuizListItem from "./QuizListItem";

const QuizList: React.FC = () => {
    const quizStore = useContext(QuizStore);
    const { quizesRegistry } = quizStore;
    return (
        <Fragment>
            {quizesRegistry.map(quiz => (
                <QuizListItem key={quiz.id!} quiz={quiz}/>
            ))}
        </Fragment>   
    );
}

export default observer(QuizList)
