import { createContext, SyntheticEvent } from "react";
import { ITeam } from "../models/team";
import { observable, action, runInAction } from "mobx";
import agent from "../api/agent";
import { ITeamRequest } from "../models/teamRequest";

class TeamStore {
    @observable TeamRegistry: ITeam[] = [];
    @observable team: ITeam | null = null;
    @observable loadingInitial = false;
    @observable submitting = false;
    @observable target: number | null = null;
    @observable editMode: boolean = false ;

    @action switchEditMode = () => {
        this.editMode = !this.editMode;      
      }

    @action loadTeams = async () => {
        this.loadingInitial = true;
        this.TeamRegistry = [];
        try{
            const teams = await agent.Teams.list();  
            runInAction('loading quizes', () => {
                teams.forEach(team => {
                    this.TeamRegistry.push(team);
                });
                this.loadingInitial = false;
            })
            return teams;        
        }
        catch(error)
        {
            runInAction('load team error', () => {
                console.log(error);
                this.loadingInitial = false;
            })
        }
    };

    @action clearTeam = () => {
        this.team = null;
    }

    @action loadTeam = async (id:number) => {
        let team = this.getTeam(id);
        if(team){
          this.team = team;
        } else{
          this.loadingInitial = true;
          try{
             team = await agent.Teams.details(id); 
             runInAction('getting team', () => {
              this.team = team!;
              this.loadingInitial = false;
             })
          }
          catch(error)
          {
            runInAction('get team error', () => {
              this.loadingInitial = false;
            })
            console.log(error);
          }
        }
      }

      getTeam = (id:number) => {
        return this.TeamRegistry.find(x => x.id === id);
      }

      @action cancelSelectedTeam = () => {
        this.team = null;
      }
    
      @action createTeam = async (quiz: ITeamRequest) => {
        this.submitting = true;
        try {
          let teamReturn = await agent.Teams.create(quiz);
          runInAction('Creating team',()=> {
            this.TeamRegistry.push(teamReturn);
            this.submitting = false;
          })
          
        } catch (error) {
          runInAction('create team error',() => {
            this.submitting = false;
            console.log(error);
          })
          
        }
      };
    
      @action editTeam = async (team: ITeam) => {
        this.submitting = true;
        try {
          await agent.Teams.update(team);
          runInAction('editing team',() => {
            this.TeamRegistry.push(team);
            this.team = team;
            this.submitting = false;
          })
          
        }
        catch(error)
        {
          runInAction('editing team error',() => {
            this.submitting = false;
            console.log(error);
          })      
        }
      }
    
      @action deleteTeam = async (event: SyntheticEvent<HTMLButtonElement>, id: number) => {
        this.submitting = true;
        this.target = Number(event.currentTarget.name);
        try{
          await agent.Teams.delete(id);
          runInAction('Deleting team',() => {
            this.TeamRegistry.filter(x => x.id !== id);
            this.submitting = false;
            this.target = null;
            window.location.reload();
          })
          
        }
        catch(error){
          runInAction('Deleting team error', () => {
            this.submitting = false;
            this.target = null;
            console.log(error);
          })      
        }
        
      }
}

export default createContext(new TeamStore() );