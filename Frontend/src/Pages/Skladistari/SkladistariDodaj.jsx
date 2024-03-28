import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate,  } from "react-router-dom";
import { RoutesNames } from "../../constants";
import SkladistarService from "../../services/SkladistarService";





export default function SkladistareDodaj() {
    const navigate = useNavigate();

    async function dodajSkladistara(skladistar){
        const odgovor = await SkladistarService.dodaj(skladistar);
        if(odgovor.ok){
          navigate(RoutesNames.SKLADISTARI_PREGLED);
        }else{
          console.log(odgovor);
          alert(odgovor.poruka);
        }
    }

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
       

        dodajSkladistara({
            ime: podaci.get('ime'),
            prezime: podaci.get('prezime'),
            brojTelefona: podaci.get('Broj Telefona'),
            email: podaci.get('email')
            
          });         
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
            placeholder='Prezime'
            maxLength={50}
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='Broj telefona'>
          <Form.Label>brojTelefona</Form.Label>
          <Form.Control
            type='text'
            name='Broj Telefona'
            placeholder='Broj Telefona'
            maxLength={20}
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='email'>
          <Form.Label>Email</Form.Label>
          <Form.Control
            type='text'
            name='email'
            placeholder='Email'
            maxLength={50}
          />
        </Form.Group>
    
        <Row className="akcije">
          <Col>
            <Link className='btn btn-danger gumb' to={RoutesNames.SKLADISTARI_PREGLED}>
              Odustani
            </Link>
          </Col>
          <Col>
            <Button variant='primary' className='gumb' type='submit'>
              Dodaj SKladištara
            </Button>
          </Col>
        </Row>
      </Form>
        </Container>
      
    );


}