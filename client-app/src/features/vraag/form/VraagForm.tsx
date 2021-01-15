import { RouteComponentProps } from "react-router";
import { observer } from "mobx-react-lite";
import React, { useContext, useState, FormEvent } from "react";
import VraagStore from "../../../app/stores/vraagStore";
import { IVraagRequest } from "../../../app/models/vraagRequest";
import {
  Grid,
  Segment,
  Form,
  Button,
  Dropdown,
  DropdownProps
} from "semantic-ui-react";

interface DetailParams {
  id: string;
}

const VraagForm: React.FC<RouteComponentProps<DetailParams>> = ({  
  history
}) => {
  const vraagStore = useContext(VraagStore);
  const {
    createVraag,
    submitting,    
  } = vraagStore;

  const [vraag, setVraag] = useState<IVraagRequest>({
    TypeVraagId: 1,
    jsonCorrecteAntwoord: "",
    jsonMogelijkeAntwoorden: "",
    maxScoreVraag: 0,
    vraagStelling: ""
  });

  /*useEffect(() => {
      if (match.params.id === null) {
        let number = Number(match.params.id);
        loadVraag(number).then(
          () => initialFormState && setVraag(initialFormState)
        );
      }
      return () => {
        clearTeam();
      };
    }, [loadTeam, clearTeam, match.params.id, initialFormState]);*/

  const handleSubmit = () => {
    console.log(vraag);
    createVraag(vraag).then(() => history.push(`/vragen`));
  };

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    
    setVraag({
      ...vraag!,
      [event.currentTarget.name]: event.currentTarget.value
    });
  };

  const handelDropDown = (
    event: React.SyntheticEvent<HTMLElement, Event>,
    data: DropdownProps
  ) => {
    //console.log(data.value);
    setVraag(vraag => {
      return { ...vraag, typeVraagId: Number(data.value) };
    });
  };

  const handleNumberValue = (event: FormEvent<HTMLInputElement>) => {
      setVraag({
          ...vraag!,
          maxScoreVraag: Number(event.currentTarget.value)
      })
  }

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
      <Grid.Column width={10}>
        <Segment clearing>
          <Form onSubmit={handleSubmit}>
            <Form.Input
              onChange={handleInputChange}
              name="vraagStelling"
              placeholder="vraagStelling"
            />
            <Form.Input
              onChange={handleInputChange}
              name="jsonCorrecteAntwoord"
              placeholder="jsonCorrecteAntwoord"
            />
            <Form.Input
              onChange={handleInputChange}
              name="jsonMogelijkeAntwoorden"
              placeholder="jsonMogelijkeAntwoorden"
            />
            <Form.Input
              onChange={handleNumberValue}
              name="maxScoreVraag"
              placeholder="maxScore"
            />
            <Dropdown
              placeholder="Type vraag"
              fluid
              selection
              options={typeVraagOptions}
              onChange={(e, data) => handelDropDown(e, data)}
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
                onClick={() => history.push("/vragen")}
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

export default observer(VraagForm);
