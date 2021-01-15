import { createContext, SyntheticEvent } from "react";
import {observable, configure, runInAction, action} from 'mobx';
import { IQuiz } from "../models/quiz";
import agent from "../api/agent";
import { IQuizRequest } from "../models/quizRequest";

configure({enforceActions: 'always'});

class QuizStore {
    @observable quizesRegistry: IQuiz[] = [];
    @observable quiz: IQuiz | null = null;
    @observable loadingInitial = false;
    @observable submitting = false;
    @observable target: number | null = null;    
    @observable editMode: boolean = false ;

    @action switchEditMode = () => {
      this.editMode = !this.editMode;      
    }

    @action loadQuizes = async () => {
        this.loadingInitial = true;
        this.quizesRegistry = [];
        try{
            const quizes = await agent.Quizes.list();  
            runInAction('loading quizes', () => {
                quizes.forEach(quiz => {
                    this.quizesRegistry.push(quiz);
                });
                this.loadingInitial = false;
            })
            return quizes;        
        }
        catch(error)
        {
            runInAction('load quizes error', () => {
                console.log(error);
                this.loadingInitial = false;
            })
        }
    };

    @action clearQuiz = () => {
        this.quiz = null;
    }

    @action loadQuiz = async (id:number) => {
        let quiz = this.getQuiz(id);
        if(quiz){
          this.quiz = quiz;
        } else{
          this.loadingInitial = true;
          try{
             quiz = await agent.Quizes.details(id); 
             runInAction('getting quiz', () => {
              this.quiz = quiz!;
              this.loadingInitial = false;
             })
          }
          catch(error)
          {
            runInAction('get quiz error', () => {
              this.loadingInitial = false;
            })
            console.log(error);
          }
        }
      }

      getQuiz = (id:number) => {
        return this.quizesRegistry.find(x => x.id === id);
      }

      @action cancelSelectedQuiz = () => {
        this.quiz = null;
      }
    
      @action createQuiz = async (quiz: IQuizRequest) => {
        this.submitting = true;
        try {
          let quizReturn = await agent.Quizes.create(quiz);
          runInAction('Creating quiz',()=> {
            this.quizesRegistry.push(quizReturn);
            this.submitting = false;
          })
          
        } catch (error) {
          runInAction('create quiz error',() => {
            this.submitting = false;
            console.log(error);
          })
          
        }
      };
    
      @action editQuiz = async (quiz: IQuiz) => {
        this.submitting = true;
        try {
          await agent.Quizes.update(quiz);
          runInAction('editing quiz',() => {
            this.quizesRegistry.push(quiz);
            this.quiz = quiz;
            this.submitting = false;
          })
          
        }
        catch(error)
        {
          runInAction('editing quiz error',() => {
            this.submitting = false;
            console.log(error);
          })      
        }
      }
    
      @action deleteQuiz = async (event: SyntheticEvent<HTMLButtonElement>, id: number) => {
        this.submitting = true;
        this.target = Number(event.currentTarget.name);
        try{
          await agent.Quizes.delete(id);
          runInAction('Deleting quiz',() => {
            this.quizesRegistry.filter(x => x.id !== id);
            this.submitting = false;
            this.target = null;
            window.location.reload();
          })
          
        }
        catch(error){
          runInAction('Deleting quiz error', () => {
            this.submitting = false;
            this.target = null;
            console.log(error);
          })      
        }
        
      }
}

export default createContext(new QuizStore() );