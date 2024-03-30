
import { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import OsobaService from "../../services/OsobaService";
import { RoutesNames } from "../../constants";
import { dohvatiPorukeAlert } from '../../services/httpService';




export default function OsobePromjeni(){

    const [osoba,setOsoba] = useState({});
    const routeParams = useParams();
    const navigate =useNavigate();
    
    

    async function dohvatiOsobu(){
        const odgovor = await OsobaService
        .getBySifra(routeParams.sifra)
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
          }
          setOsoba(odgovor.podaci);
    }

    async function promjeniOsobu(osoba){
        const odgovor = await OsobaService.promjeni(routeParams.sifra,osoba);
        if(odgovor.ok){
          navigate(RoutesNames.OSOBE_PREGLED);
          return;
        }
        alert(dohvatiPorukeAlert(odgovor.podaci));
    }

    useEffect(()=>{
        
        dohvatiOsobu();
    },[]);

   

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
            
            promjeniOsobu({
                ime: podaci.get('ime'),
                prezime: podaci.get('prezime'),
                brojtelefona: podaci.get('brojtelefona'),
                email: podaci.get('email')
            });
              
            
    }


    return (

        <Container >
           
           <Form onSubmit={handleSubmit}>

                <Form.Group  controlId="ime">
                    <Form.Label>Ime</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={osoba.ime}
                        name="ime"
                        maxLength={50}
                        required
                    />
                </Form.Group>

                <Form.Group  controlId="prezime">
                    <Form.Label>Prezime</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={osoba.prezime}
                        name="prezime"
                        maxLength={50}
                        required
                    />
                </Form.Group>

                <Form.Group controlId="brojtelefona">
                    <Form.Label>Broj Telefona</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={osoba.brojtelefona}
                        name="brojtelefona"
                        maxLength={50}
                    />
                </Form.Group>

                <Form.Group controlId="email">
                    <Form.Label>Email</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={osoba.email}
                        name="email"
                        maxLength={20}
                    />
                </Form.Group>

                
                <Row >
                    <Col>
                        <Link 
                        className="btn btn-danger gumb"
                        to={RoutesNames.OSOBE_PREGLED}>Odustani</Link>
                    </Col>
                    <Col>
                        <Button
                            variant="primary"
                            className='gumb'
                            type="submit"
                        >
                            Promjeni podatke od osobe
                        </Button>
                    </Col>
                </Row>
                
           </Form>

        </Container>

    );

}