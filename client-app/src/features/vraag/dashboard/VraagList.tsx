import React, { useContext, Fragment } from "react";
import { observer } from 'mobx-react-lite';
import VraagStore from "../../../app/stores/vraagStore";
import VraagListItem from "./VraagListItem";

const VraagList: React.FC = () => {
    const vraagStore = useContext(VraagStore);
    const vragen = vraagStore.vragenRegistry;
    return (
        <Fragment>
            {vragen.map(vraag => (
                <VraagListItem key={vraag.id} vraag={vraag}/>
            ))}
        </Fragment>
    );
}

export default observer(VraagList)
