import React from 'react'
import { Container, Segment, Header, Image, Button } from 'semantic-ui-react'
import { Link } from 'react-router-dom'

const HomePage = () => {
    return (
        <Segment inverted textAlign='center' vertical className='masthead' >
        <Container text>
            <Header as='h1' inverted>
                <Image size='massive' src='/assets/images/logo.png' alt='logo' style={{marginBottom: 12}}/>
                Make a quiz!
            </Header>
            <Header as='h2' inverted content='Welcome to MakeAQuiz!' />
            <Button as={Link} to='/client' size='huge' inverted>
                Take me to create a quiz!
            </Button>
        </Container>
    </Segment>
    );
};

export default HomePage