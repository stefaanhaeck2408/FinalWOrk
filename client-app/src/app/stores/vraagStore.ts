import { createContext, SyntheticEvent } from "react";
import {observable, configure, runInAction, action} from 'mobx';
import { IVraag } from "../models/vraag";
import agent from "../api/agent";
import { IVraagRequest } from "../models/vraagRequest";

configure({enforceActions: 'always'});

class VraagStore {
    @observable vragenRegistry: IVraag[] = [];
    @observable vraag: IVraag | null = null;
    @observable loadingInitial = false;
    @observable submitting = false;
    @observable target: number | null = null; 
    @observable editMode: boolean = false ;

    @action switchEditMode = () => {
        this.editMode = !this.editMode;      
      }

    @action loadVragen = async () => {
        this.loadingInitial = true;
        this.vragenRegistry = [];
        try{
            const vragen = await agent.Vragen.list();  
            runInAction('loading vragen', () => {
                vragen.forEach(vraag => {
                    this.vragenRegistry.push(vraag);
                });
                this.loadingInitial = false;
            })
            return vragen;        
        }
        catch(error)
        {
            runInAction('load vragen error', () => {
                console.log(error);
                this.loadingInitial = false;
            })
        }
    };

    @action clearVraag = () => {
        this.vraag = null;
    }

    @action loadVraag = async (id:number) => {
        let vraag = this.getVraag(id);
        
        if(vraag){
          this.vraag = vraag;
        } else{
          this.loadingInitial = true;
          try{
            vraag = await agent.Vragen.details(id); 
            console.log(vraag);
             runInAction('getting vraag', () => {
              this.vraag = vraag!;
              this.loadingInitial = false;
             })
          }
          catch(error)
          {
            runInAction('get vraag error', () => {
              this.loadingInitial = false;
            })
            console.log(error);
          }
        }
      }

      getVraag = (id:number) => {
        return this.vragenRegistry.find(x => x.id === id);
      }

      @action cancelSelectedVraag = () => {
        this.vraag = null;
      }
    
      @action createVraag = async (vraag: IVraagRequest) => {
        this.submitting = true;
        try {
          let vraagReturn = await agent.Vragen.create(vraag);
          runInAction('Creating vraag',()=> {
            this.vragenRegistry.push(vraagReturn);
            this.submitting = false;
          })
          
        } catch (error) {
          runInAction('create vraag error',() => {
            this.submitting = false;
            console.log(error);
          })
          
        }
      };
    
      @action editVraag = async (vraag: IVraag) => {
        this.submitting = true;
        try {
          console.log(vraag);
          await agent.Vragen.update(vraag);
          runInAction('editing vraag',() => {
            this.vragenRegistry.push(vraag);
            this.vraag = vraag;
            this.submitting = false;
          })
          
        }
        catch(error)
        {
          runInAction('editing vraag error',() => {
            this.submitting = false;
            console.log(error);
          })      
        }
      }
    
      @action deleteVraag = async (event: SyntheticEvent<HTMLButtonElement>, id: number) => {
        this.submitting = true;
        this.target = Number(event.currentTarget.name);
        try{
          await agent.Vragen.delete(id);
          runInAction('Deleting vraag',() => {
            this.vragenRegistry.filter(x => x.id !== id);
            this.submitting = false;
            this.target = null;
            window.location.reload();
          })
          
        }
        catch(error){
          runInAction('Deleting vraag error', () => {
            this.submitting = false;
            this.target = null;
            console.log(error);
          })      
        }
        
      }
}

export default createContext(new VraagStore() );