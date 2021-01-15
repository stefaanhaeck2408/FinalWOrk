import { IVraag } from "../../../app/models/vraag";
import React, { useContext } from "react";
import { Segment, Button } from "semantic-ui-react";
import { Link } from "react-router-dom";
import VraagStore from "../../../app/stores/vraagStore";

const VraagListItem: React.FC<{ vraag: IVraag }> = ({ vraag }) => { 

  const vraagStore = useContext(VraagStore);
  const {deleteVraag, submitting, target} = vraagStore;
  return (
    <Segment.Group>
      <Segment clearing>{vraag.vraagStelling}
      
        <Button
          as={Link}
          to={`/vragen/${vraag.id}`}
          floated="right"
          content="View"
          color="blue"
        />
        <Button
          name={vraag.id}
          loading={target === vraag.id && submitting}
          onClick={e => deleteVraag(e, vraag.id)}
          floated="right"
          content="Delete"
          color="red"
        />
      </Segment>
    </Segment.Group>
  );
};

export default VraagListItem;
