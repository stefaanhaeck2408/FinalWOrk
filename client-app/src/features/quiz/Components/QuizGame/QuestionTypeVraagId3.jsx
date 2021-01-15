import React, { Component } from 'react'

export class QuestionTypeVraagId3 extends Component {
    state = {
        positions: []
    }

    resetPositions = () => {
        this.setState({positions: []});
    }

    submitPositions = (e) => {
        console.log("submit positions")
        var string = this.props.positions.join(',');
        this.props.submitAnswer(e,string);
    }

    render() {
        const segmentStyle = {
            //padding: 10,
            color: "rgb(124 173 242)",
            borderRadius: "5px",
            height: "100",
            //padding: "25px",
            //paddingBottem: "25px"
          };

          const resetStyle = {
            border: "3px solid rgb(25 24 112)",
            color: "rgb(25 24 112)",    
            backgroundColor:  "rgb(234 243 250 1)" ,
            borderRadius: "30px 12px 12px 30px",              
          }
          const submitStyle = {
            border: "3px solid rgb(25 24 112)",
            color: "rgb(25 24 112)",    
            backgroundColor:  "rgb(234 243 250 1)" ,
            borderRadius: "12px 30px 30px 12px"      
          }
        if(this.props.positions.length > 0){
            return (
                <div style={segmentStyle}>
                    <h4>Your answer:</h4>
                    {this.props.positions.map((answer, index) => (<p key={index}>{index+1}{". "}{answer}</p>))} 
                    <button style={resetStyle} value="Reset" onClick={this.props.resetPosition}>Reset</button>
                    <button style={submitStyle} value="Submit" onClick={(e) => this.submitPositions(e)}>Submit</button> 
                </div>
            )
        }else{
            return (
                <div>
                    
                </div>
            )
        }
        
    }
}

export default QuestionTypeVraagId3
