import React, { useContext, Fragment } from "react";
import { observer } from 'mobx-react-lite';
import TeamStore from "../../../app/stores/teamStore";
import TeamListItem from "./TeamListItem";

const TeamList: React.FC = () => {
    const teamStore = useContext(TeamStore);
    const teams = teamStore.TeamRegistry;
    return (
        <Fragment>
            {teams.map(team => (
                <TeamListItem key={team.id} team={team}/>
            ))}
        </Fragment>
    );
}

export default observer(TeamList)
