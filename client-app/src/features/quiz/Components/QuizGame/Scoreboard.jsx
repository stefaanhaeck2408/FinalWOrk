import React, { Component } from 'react'

export class Scoreboard extends Component {
    render() {
        return (
            <div style={{position:"absolute"}}>
                <h3>The round has ended!</h3> 
                <table style={{width:"800px", borderCollapse:"collapse", overflow:"hidden"}}>
                    <thead>
                    <tr>
                        <th style={{padding:"15px", backgroundColor:"#55608f", color:"#fff"}}>Rank</th>
                        <th style={{padding:"15px", backgroundColor:"#55608f", color:"#fff"}}>Score</th>
                        <th style={{padding:"15px", backgroundColor:"#55608f", color:"#fff"}}>Team name</th>
                    </tr>
                    </thead>
                    <tbody>
                    {this.props.score.sort((a,b) => a.score < b.score ? 1 : -1).map((team, index) => (
                        <tr key={index}>
                            <td style={{padding:"15px", backgroundColor:"rgb(89 216 241)", color:"#fff", textAlign:"left"}}>{++index}</td>
                            <td style={{padding:"15px", backgroundColor:"rgb(89 216 241)", color:"#fff", textAlign:"left"}}>{team.score + "/" + team.maxScoreQuiz}</td>
                            <td style={{padding:"15px", backgroundColor:"rgb(89 216 241)", color:"#fff", textAlign:"left"}}>{team.naam}</td>
                        </tr>
                    ))}
                    </tbody>                    
                </table>
            </div>
        )
    }
}

export default Scoreboard
