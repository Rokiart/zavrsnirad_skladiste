
import { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import SkladistarService from "../../services/SkladistarService";
import { RoutesNames } from "../../constants";



export default function SkladistaraPromjeni(){

    const [skladistar, setSkladistar] = useState({});

    const routeParams = useParams();
    const navigate = useNavigate();

    async function dohvatiSkladistara(){
        await SkladistarService
        .getBySifra(routeParams.sifra)
        .then((response) => {
          console.log(response);
          setSkladistar(response.data);
        })
        .catch((err) => alert(err.poruka));
    }

    useEffect(()=>{
        
        dohvatiSkladistara();
    },[]);

    async function promjeniSkladistar(skladistar){
        const odgovor = await SkladistarService.promjeni(routeParams.sifra,skladistar);
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
        
        const skladistar =
        {
            ime: podaci.get('ime'),
            prezime: skladistar.get('prezime'),
            brojTelefona: podaci.get('broj telefona'),
            email: podaci.get('email')
            
        };

        promjeniSkladistar(skladistar) ; 
         
    }


    return (

        <Container>
           
           <Form onSubmit={handleSubmit}>

                <Form.Group controlId="ime">
                    <Form.Label>Ime</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={skladistar.ime}
                        name="ime"
                    />
                </Form.Group>

                <Form.Group controlId="prezime">
                    <Form.Label>Prezime</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={skladistar.prezime}
                        name="prezime"
                    />
                </Form.Group>

                <Form.Group controlId="brojTelefona">
                    <Form.Label>Broj Telefona</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={skladistar.brojTelefona}
                        name="brojTelefona"
                    />
                </Form.Group>

                <Form.Group controlId="email">
                    <Form.Label>Email</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={skladistar.email}
                        name="email"
                    />
                </Form.Group>

                
                <Row className="akcije">
                    <Col>
                        <Link 
                        className="btn btn-danger"
                        to={RoutesNames.SKLADISTARI_PREGLED}>Odustani</Link>
                    </Col>
                    <Col>
                        <Button
                            variant="primary"
                          
                            type="submit"
                        >
                            Promjeni skladistara
                        </Button>
                    </Col>
                </Row>
                
           </Form>

        </Container>

    );

}