import React, { useContext, Fragment } from "react";
import { observer } from 'mobx-react-lite';
import RondeStore from "../../../app/stores/rondeStore";
import RondeListItem from "./RondeListItem";

const RondeList: React.FC = () => {
    const rondeStore = useContext(RondeStore);
    const rondes = rondeStore.rondesRegistry;
    return (
        <Fragment>
            {rondes.map(ronde => (
                <RondeListItem key={ronde.id} ronde={ronde}/>
            ))}
        </Fragment>
    );
}

export default observer(RondeList)
