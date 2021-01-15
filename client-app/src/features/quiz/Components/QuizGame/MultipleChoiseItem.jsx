import React, { Component } from 'react'

export class MultipleChoiseItem extends Component {
    state=  {
        hover: false,
        clicked: false
    }

    toggleHover = () => {
        //if(!this.state.clicked){
            this.setState({hover : !this.state.hover});
        //}
        
    }

    answerSelected = (e, answer) => {
        console.log(answer);
        //this.setState({clicked: true})
        this.props.answerSelected(e,answer);
    }
    render() {
        var itemStyle;
        
        if(this.state.hover){
            itemStyle  = {            
                background: "rgb(213 229 252)",
                borderRadius: "5px",
                margin: "5px",
                marginLeft: "13%",
                marginRight: "13%",
                color: "#193670",
                paddingLeft: "25px",
                height: "25px",
                fontSize: "18px"
        }
        }else{
            itemStyle  = {            
                background: "rgb(124 173 242)",
                borderRadius: "5px",
                margin: "5px",
                marginLeft: "13%",
                marginRight: "13%",
                color: "#193670",
                paddingLeft: "25px",
                height: "25px",
                fontSize: "18px"
        }
    }

        return (
            <div style={itemStyle} onMouseEnter={this.toggleHover} onMouseLeave={this.toggleHover} onClick={(e) => this.answerSelected(e, this.props.answer)}>
                {this.props.answer}
            </div>
        )
    }
}

export default MultipleChoiseItem
