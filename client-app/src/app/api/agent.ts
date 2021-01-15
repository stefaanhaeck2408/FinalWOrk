import axios, { AxiosResponse } from "axios";
import { IQuiz } from "../models/quiz";
import { ITeam } from "../models/team";
import { IRonde } from "../models/ronde";
import { IVraag } from "../models/vraag";
import { IQuizRequest } from "../models/quizRequest";
import { ITeamRequest } from "../models/teamRequest";
import { IRondeRequest } from "../models/RondeRequest";
import { IVraagRequest } from "../models/vraagRequest";

axios.defaults.baseURL = 'https://localhost:44302/api/';

const responseBody = (response: AxiosResponse) => response.data;

const sleep = (ms: number) => (response: AxiosResponse) => 
    new Promise<AxiosResponse>(resolve => setTimeout(() => resolve(response), ms));

const requests = {
    get: (url: string) => axios.get(url).then(sleep(1000)).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(sleep(1000)).then(responseBody),
    put: (url:string, body: {}) => axios.put(url, body).then(sleep(1000)).then(responseBody),
    del: (url: string) => axios.delete(url).then(sleep(1000)).then(responseBody)
}

const Quizes = {
    list: (): Promise<IQuiz[]> => requests.get('Quiz/GetAll'),
    details: (id:number) => requests.get(`Quiz/GetById/${id}`),
    create: (quiz: IQuizRequest) => requests.post('/quiz/create', quiz),
    update: (quiz: IQuiz) => requests.put(`/quiz/update`, quiz),
    delete: (id:number) => requests.del(`/quiz/Delete/${id}`)
   
}

const Teams = {
    list: (): Promise<ITeam[]> => requests.get('Team/GetAll'),
    details: (id:number) => requests.get(`Team/GetById/${id}`),
    create: (team: ITeamRequest) => requests.post('/Team/create', team),
    update: (team: ITeam) => requests.put(`/Team/update`, team),
    delete: (id:number) => requests.del(`/Team/Delete/${id}`)
}

const Rondes = {
    list: (): Promise<IRonde[]> => requests.get('Ronde/GetAll'),
    details: (id:number) => requests.get(`Ronde/GetById/${id}`),
    create: (ronde: IRondeRequest) => requests.post('/Ronde/create', ronde),
    update: (ronde: IRonde) => requests.put(`/Ronde/update`, ronde),
    delete: (id:number) => requests.del(`/Ronde/Delete/${id}`),
    rondesInQuiz: (id:number) => requests.get(`Ronde/GetAllRondesInAQuiz/${id}`)
}

const Vragen = {
    list: (): Promise<IVraag[]> => requests.get('Vragen/GetAll'),
    details: (id:number) => requests.get(`Vragen/GetById/${id}`),
    create: (vraag: IVraagRequest) => requests.post('/Vragen/create', vraag),
    update: (vraag: IVraag) => requests.put(`/Vragen/update`, vraag),
    delete: (id:number) => requests.del(`/Vragen/Delete/${id}`)
}

export default {
    Quizes, Teams, Rondes, Vragen
}