
import { useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import OsobaService from "../../services/OsobaService";
import { RoutesNames } from "../../constants";


export default function OsobePromjeni(){

    const navigate =useNavigate();
    const routeParams = useParams();
    const [osoba,setOsoba] = useState({});

    async function dohvatiOsobu(){
        await OsobaService.getBySifra(routeParams.sifra)
        .then((res)=>{
            setOsoba(res.data)
        })
        .catch((e)=>{
            alert(e.poruka);
        });
    }

    useEffect(()=>{
        
        dohvatiOsobu();
    },[]);

    async function promjeniOsobu(osoba){
        const odgovor = await OsobaService.promjeniOsobu(routeParams.sifra,osoba);
        if(odgovor.ok){
          navigate(RoutesNames.OSOBE_PREGLED);
        }else{
          console.log(odgovor);
          alert(odgovor.poruka);
        }
    }

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);

        const osoba = 
        {
            ime: podaci.get('ime'),
            prezime: parseInt(podaci.get('prezime')),
            brojTelefona: parseFloat(podaci.get('broj telefona')),
            email: parseFloat(podaci.get('email')),
            
          };

          
          promjeniOsobu(osoba);
    }


    return (

        <Container>
           
           <Form onSubmit={handleSubmit}>

                <Form.Group controlId="ime">
                    <Form.Label>Ime</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={osoba.ime}
                        name="ime"
                    />
                </Form.Group>

                <Form.Group controlId="prezime">
                    <Form.Label>Prezime</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={osoba.prezime}
                        name="prezime"
                    />
                </Form.Group>

                <Form.Group controlId="brojTelefona">
                    <Form.Label>Cijena</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={osoba.brojTelefona}
                        name="brojTelefona"
                    />
                </Form.Group>

                <Form.Group controlId="email">
                    <Form.Label>Upisnina</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={osoba.email}
                        name="email"
                    />
                </Form.Group>

                
                <Row className="akcije">
                    <Col>
                        <Link 
                        className="btn btn-danger"
                        to={RoutesNames.OSOBE_PREGLED}>Odustani</Link>
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