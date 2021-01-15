import React, { Component } from "react";
import TableScrollbar from "react-table-scrollbar";
import {
  Table,
} from "semantic-ui-react";

export class History extends Component {

    
  render() {

    
    return (
      <div style={{background: "rgb(221 229 236)"}}>
        <h3 style={{color:"rgb(56 140 212)"}}>History of quiz</h3>

        <TableScrollbar rows={3} >
          <Table basic="very" celled collapsing style={{background: "rgb(221 229 236)", width: "100%"}}>
            <Table.Body>
              {[].concat(this.props.history).sort((a,b) => a.time < b.time ? 1 : -1).map((item, index) => (
                <Table.Row key={index} style={{marginBottom: "5px"}}>
                  <Table.Cell>{item.index}{". "}</Table.Cell>
                  <Table.Cell>{item.message}</Table.Cell>
                  <Table.Cell>{item.time}</Table.Cell>
                </Table.Row>
              ))}
            </Table.Body>
          </Table>
        </TableScrollbar>
      </div>
    );
  }
}

export default History;
