import { createContext, SyntheticEvent } from "react";
import {observable, configure, runInAction, action} from 'mobx';
import { IRonde } from "../models/ronde";
import agent from "../api/agent";
import { IRondeRequest } from "../models/RondeRequest";

configure({enforceActions: 'always'});

class RondeStore {
    @observable rondesRegistry: IRonde[] = [];
    @observable ronde: IRonde | null = null;
    @observable loadingInitial = false;
    @observable submitting = false;
    @observable target: number | null = null; 
    @observable editMode: boolean = false ;

    @action switchEditMode = () => {
        this.editMode = !this.editMode;      
      }

    @action rondesFromQuiz = async (id:number) => {
        this.loadingInitial = true;
        this.rondesRegistry = [];
        try{
          const rondes = await agent.Rondes.rondesInQuiz(id);
          runInAction('loading rondes', () => {
            rondes.forEach((ronde: IRonde) => {                
                this.rondesRegistry.push(ronde);
            });
            this.loadingInitial = false;
        })
        return rondes; 
        }
        catch(error)
        {
          runInAction('load rondes error', () => {
            console.log(error);
            this.loadingInitial = false;
        })
        }

    }

    @action loadRondes = async () => {        
        try{
            const rondes = await agent.Rondes.list(); 
            runInAction('loading rondes', () => {
                rondes.forEach(ronde => {
                    this.rondesRegistry.push(ronde);
                });
                this.loadingInitial = false;
            })
            return rondes;        
        }
        catch(error)
        {
            runInAction('load rondes error', () => {
                console.log(error);
                this.loadingInitial = false;
            })
        }
    };

    @action clearRonde = () => {
        this.ronde = null;
    }

    @action loadRonde = async (id:number) => {
        let ronde = this.getRonde(id);
        if(ronde){
          this.ronde = ronde;
        } else{
          this.loadingInitial = true;
          try{
             ronde = await agent.Rondes.details(id); 
             runInAction('getting ronde', () => {
              this.ronde = ronde!;
              this.loadingInitial = false;
             })
          }
          catch(error)
          {
            runInAction('get ronde error', () => {
              this.loadingInitial = false;
            })
            console.log(error);
          }
        }
      }

      getRonde = (id:number) => {
        return this.rondesRegistry.find(x => x.id === id);
      }

      @action cancelSelectedRonde = () => {
        this.ronde = null;
      }
    
      @action createRonde = async (ronde: IRondeRequest) => {
        this.submitting = true;
        try {
          let rondeReturn = await agent.Rondes.create(ronde);
          runInAction('Creating ronde',()=> {
            this.rondesRegistry.push(rondeReturn);
            this.submitting = false;
          })
          
        } catch (error) {
          runInAction('create ronde error',() => {
            this.submitting = false;
            console.log(error);
          })
          
        }
      };
    
      @action editRonde = async (ronde: IRonde) => {
        this.submitting = true;
        try {
          await agent.Rondes.update(ronde);
          runInAction('editing ronde',() => {
            this.rondesRegistry.push(ronde);
            this.ronde = ronde;
            this.submitting = false;
          })
          
        }
        catch(error)
        {
          runInAction('editing ronde error',() => {
            this.submitting = false;
            console.log(error);
          })      
        }
      }
    
      @action deleteRonde = async (event: SyntheticEvent<HTMLButtonElement>, id: number) => {
        this.submitting = true;
        this.target = Number(event.currentTarget.name);
        try{
          await agent.Rondes.delete(id);
          runInAction('Deleting ronde',() => {
            this.rondesRegistry.filter(x => x.id !== id);
            this.submitting = false;
            this.target = null;
            window.location.reload();
          })
          
        }
        catch(error){
          runInAction('Deleting ronde error', () => {
            this.submitting = false;
            this.target = null;
            console.log(error);
          })      
        }
        
      }
}

export default createContext(new RondeStore() );