import React, { Component } from "react";
import {  Icon } from "semantic-ui-react";

export class ScoreBoard extends Component {
  render() {
    return (
      <div style={{ background: "rgb(221 229 236)" }}>
        <h4 style={{ color: "rgb(56 140 212)", fontWeight: "bold" }}>
          Scoreboard
        </h4>
        <table
          style={{
            borderRadius: "5px",
            padding: "5px",
            width: "100%",
            color: "#868d9e",
          }}
        >
          <thead>
            <tr>
              <th>Team name</th>
              <th>Question answered</th>
              <th>Score</th>
              <th>Remove team</th>
            </tr>
          </thead>

          <tbody>
            {this.props.teams.sort((a,b) => a.score < b.score ? 1 : -1).map((team, index) => (
              <tr key={index}>
                <td>{team.naam}</td>
                <td>{team.questionsAnswered}</td>
                <td>
                  {team.score}
                  {"/"}
                  {team.maxScoreQuiz}
                </td>
                <td>
                  <Icon
                    name="trash alternate outline"
                    onClick={() => this.props.removeTeamFromQuiz(team.id)}
                  ></Icon>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
}

export default ScoreBoard;
