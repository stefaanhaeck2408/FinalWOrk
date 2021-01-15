export interface IVraag {
    id: number;
    maxScoreVraag: number;
    typeVraagId: number;
    vraagStelling: string;    
    jsonCorrecteAntwoord: string;
    jsonMogelijkeAntwoorden: string;
    

}