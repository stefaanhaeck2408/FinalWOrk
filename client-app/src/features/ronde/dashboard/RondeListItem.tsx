import { IRonde } from "../../../app/models/ronde";
import React, { useContext } from "react";
import { Segment, Button } from "semantic-ui-react";
import { Link } from "react-router-dom";
import RondeStore from "../../../app/stores/rondeStore";

const RondeListItem: React.FC<{ ronde: IRonde }> = ({ ronde }) => { 

  const rondeStore = useContext(RondeStore);
  const {deleteRonde, submitting, target} = rondeStore;

  return (
    <Segment.Group>
      <Segment clearing>{ronde.naam}
      
        <Button
          as={Link}
          to={`/rondes/${ronde.id}`}
          floated="right"
          content="View"
          color="blue"
        />
        <Button
          name={ronde.id}
          loading={target === ronde.id && submitting}
          onClick={e => deleteRonde(e, ronde.id)}
          floated="right"
          content="Delete"
          color="red"
        />
      </Segment>
    </Segment.Group>
  );
};

export default RondeListItem;
