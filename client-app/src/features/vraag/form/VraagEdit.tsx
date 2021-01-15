import React, { useContext, useState, FormEvent } from "react";
import VraagStore from "../../../app/stores/vraagStore";
import { IVraag } from "../../../app/models/vraag";
import {
  Grid,
  Segment,
  Form,
  Label,
  Button,  
  Dropdown,
  DropdownProps
} from "semantic-ui-react";

const VraagEdit = () => {
  const vraagStore = useContext(VraagStore);
  const { editVraag, vraag, submitting, switchEditMode } = vraagStore;

  const [old, setVraagEdit] = useState<IVraag>({
    id: vraag!.id,
    vraagStelling: vraag!.vraagStelling,
    typeVraagId: vraag!.typeVraagId,
    jsonCorrecteAntwoord: vraag!.jsonCorrecteAntwoord,
    jsonMogelijkeAntwoorden: vraag!.jsonMogelijkeAntwoorden,
    maxScoreVraag: vraag!.maxScoreVraag
  });

  const handleSubmit = () => {
    console.log(editVraag);
    editVraag(old!);
  };

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    //console.log(event.currentTarget.value);
    setVraagEdit({
      ...old!,
      [event.currentTarget.name]: event.currentTarget.value
      
    });
    console.log(event.currentTarget.name)
  };

  const handelDropDown = (event: React.SyntheticEvent<HTMLElement, Event>, data: DropdownProps) => {
    console.log(data.value);
    setVraagEdit(old => {return {...old, typeVraagId: Number(data.value)}});
  };

  const typeVraagOptions = [
    {
      key: "1",
      text: "Meerkeuze",
      value: 2
    },
    {
      key: "2",
      text: "Openvraag",
      value: 1
    },
    {
      key: "3",
      text: "Reeksvraag",
      value: 3
    }
  ];

  return (
    <Grid>
      <Grid.Column width={16}>
        <Segment clearing>
          <Form onSubmit={handleSubmit}>
            <Label>Vraagstelling:</Label>
            <Form.Input
              onChange={handleInputChange}
              name="vraagStelling"
              placeholder="Naam"
              value={old!.vraagStelling}
            />
            <Label>Correcte antwoord:</Label>
            <Form.Input
              onChange={handleInputChange}
              name="jsonCorrecteAntwoord"
              placeholder="Correcte antwoord"
              value={old!.jsonCorrecteAntwoord}
            />
            <Label>Mogelijke antwoorden:</Label>
            <Form.Input
              onChange={handleInputChange}
              name="jsonMogelijkeAntwoorden"
              placeholder="Mogelijke antwoorden"
              value={old!.jsonMogelijkeAntwoorden}
            />
            <Label>Max score:</Label>
            <Form.Input
              onChange={handleInputChange}
              name="maxScoreVraag"
              placeholder="Max score"
              value={old!.maxScoreVraag}
            />
            <Label>Type vraag:</Label>

            <Dropdown
              placeholder="Type vraag"
              fluid
              selection
              options={typeVraagOptions}
              onChange={(e,data) => handelDropDown(e,data)}
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

export default VraagEdit;
