import { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";

import { RoutesNames } from "../../constants";
import IzdatnicaService from "../../services/IzdatnicaService";


export default function IzdatnicePromjeni(){

    const navigate =useNavigate();
    const routeParams = useParams();
    const [izdatnica,setIzdatnica] = useState({});

    async function dohvatiIzdatnicu(){
        await IzdatnicaService.getBySifra(routeParams.sifra)
        .then((res)=>{
            setIzdatnica(res.data)
        })
        .catch((e)=>{
            alert(e.poruka);
        });
    }

    useEffect(()=>{
        
        dohvatiIzdatnicu();
    },[]);

    async function promjeniIzdatnicu(Izdatnica){
        const odgovor = await IzdatnicaService.promjeniIzdatnicu(routeParams.sifra,Izdatnica);
        if(odgovor.ok){
          navigate(RoutesNames.IZDATNICE_PREGLED);
        }else{
          console.log(odgovor);
          alert(odgovor.poruka);
        }
    }

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);

        const izdatnica = 
        {
            brojIzdatnice: podaci.get('brojIzdatnice'),
            datum: parseInt(podaci.get('datum')),
            osoba: parseInt(podaci.get('osoba')),
            skladistar: parseInt(podaci.get('skladistar')),
            napomena: parseFloat(podaci.get('napomena'))
            
            
          };

          
          promjeniIzdatnicu(izdatnica);
    }


    return (

        <Container>
           
           <Form onSubmit={handleSubmit}>

                <Form.Group controlId="brojIzdatnice">
                    <Form.Label>Broj Izdatnice</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.brojIzdatnice}
                        name="brojIzdatnice"
                    />
                </Form.Group>

                <Form.Group controlId="datum">
                    <Form.Label>Datum</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.datum}
                        name="datum"
                    />
                </Form.Group>

                <Form.Group controlId="osoba">
                    <Form.Label>Osoba</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.osoba}
                        name="osoba"
                    />
                </Form.Group>

                <Form.Group controlId="skladistar">
                    <Form.Label>Skladistar</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.skladistar}
                        name="skladistar"
                    />
                </Form.Group>

                <Form.Group controlId="napomena">
                    <Form.Label>Napomena</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.napomena}
                        name="napomena"
                    />

                </Form.Group>                     

                
                <Row className="akcije">
                    <Col>
                        <Link 
                        className="btn btn-danger"
                        to={RoutesNames.IZDATNICE_PREGLED}>Odustani</Link>
                    </Col>
                    <Col>
                        <Button
                            variant="primary"
                            type="submit"
                        >
                            Promjeni osobu
                        </Button>
                    </Col>
                </Row>
                
           </Form>

        </Container>

    );

}