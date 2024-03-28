import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate,  } from "react-router-dom";
import { RoutesNames } from "../../constants";
import OsobaService from "../../services/OsobaService";


export default function OsobeDodaj() {
    const navigate = useNavigate();
    

    async function dodajOsobu(osoba){
        const odgovor = await OsobaService.dodaj(osoba);
        if(odgovor.ok){
          navigate(RoutesNames.OSOBE_PREGLED);
        }else{
          console.log(odgovor);
          alert(odgovor.poruka.errors);
        }
    }

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
       

        const osoba ={
          ime: podaci.get('ime'),
          prezime: podaci.get('prezime'),
          brojtelefona: podaci.get('brojtelefona'),
          email: podaci.get('email')
        }
            
         dodajOsobu(osoba); 
    }

    return(
        <Container className="nt-4">
            <Form onSubmit={handleSubmit}>
        <Form.Group className='mb-3' controlId='ime'>
          <Form.Label>Ime</Form.Label>
          <Form.Control
            type='text'
            name='ime'
            placeholder='Ime'
            maxLength={50}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='prezime'>
          <Form.Label>Prezime</Form.Label>
          <Form.Control
            type='text'
            name='prezime'
            placeholder='prezime'
            maxLength={50}
            required
          />

           
        </Form.Group>

        <Form.Group className='mb-3' controlId='brojtelefona'>
          <Form.Label>brojTelefona</Form.Label>
          <Form.Control
            type='text'
            name='brojtelefona'
            placeholder='brojtelefona'
            maxLength={20}
            
          />
          </Form.Group>

           <Form.Group className='mb-3' controlId='email'>
           <Form.Label>Email</Form.Label>
           <Form.Control
           type='text'
           name='email'
           placeholder='email'
           maxLength={50}
   
        />
        </Form.Group>
    
        <Row className="akcije">
          <Col>
            <Link className='btn btn-danger gumb' to={RoutesNames.OSOBE_PREGLED}>
              Odustani
            </Link>
          </Col>
          <Col>
            <Button variant='primary' className='gumb' type='submit'>
              Dodaj Osobu
            </Button>
          </Col>
        </Row>
      </Form>
        </Container>
      
    );


}