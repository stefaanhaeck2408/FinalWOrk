import { ITeam } from "../../../app/models/team";
import React, { useContext } from "react";
import { Segment, Button } from "semantic-ui-react";
import { Link } from "react-router-dom";
import TeamStore from "../../../app/stores/teamStore";

const TeamListItem: React.FC<{ team: ITeam }> = ({ team }) => {
  
  const teamStore = useContext(TeamStore);
  const {deleteTeam, submitting, target} = teamStore;

  return (
    <Segment.Group>
      <Segment clearing>{team.naam}
      
        <Button
          as={Link}
          to={`/teams/${team.id}`}
          floated="right"
          content="View"
          color="blue"
        />
        <Button
          name={team.id}
          loading={target === team.id && submitting}
          onClick={e => deleteTeam(e, team.id)}
          floated="right"
          content="Delete"
          color="red"
        />
      </Segment>
    </Segment.Group>
  );
};

export default TeamListItem;
