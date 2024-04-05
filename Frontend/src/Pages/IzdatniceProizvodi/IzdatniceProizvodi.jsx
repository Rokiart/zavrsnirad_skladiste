import { useEffect, useState } from "react";
import {  Button, Col, Container, Form, Row, Table } from "react-bootstrap";
import Service from "../../services/IzdatniceProizvodi";
import { FaEdit, FaTrash } from "react-icons/fa";
import useError from "../../hooks/useError";
import useLoading from "../../hooks/useLoading";


export default function IzdatniceProizvodi(){
    const [lista,setLista] = useState();
    const [odabrano,setOdabrano] = useState();
    const { prikaziError } = useError();
    const { showLoading, hideLoading } = useLoading();

    async function dohvati(){
        showLoading();
        const odgovor = await Service.get('IzdatniceProizvodi');
        if(!odgovor.ok){
            hideLoading();
            prikaziError(odgovor.podaci);
            return;
        }
        setLista(odgovor.podaci);
        hideLoading();
    }

    async function obrisi(sifra){
        showLoading();
        const odgovor = await Service.obrisi('IzdatniceProizvodi',sifra);
        prikaziError(odgovor.podaci);
        if (odgovor.ok){
            hideLoading();
            dohvati();
        }
        hideLoading();
    }
    async function dodaj(IzdatniceProizvodi){
        showLoading();
        const odgovor = await Service.dodaj('IzdatniceProizvodi',IzdatniceProizvodi);
        if(!odgovor.ok){
            hideLoading();
            prikaziError(odgovor.podaci);
            return;
        }
        dohvati();
        hideLoading();
    }

    async function promjeni(IzdatniceProizvodi){
        showLoading();
        const odgovor = await Service.promjeni('IzdatniceProizvodi',odabrano.sifra,oznaka);
        if(!odgovor.ok){
            hideLoading();
            prikaziError(odgovor.podaci);
            return;
        }
        dohvati();
        hideLoading();
    }
    useEffect(()=>{
        dohvati();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    },[]);

    function handleSubmit(e){   
        e.preventDefault();
        if (e.nativeEvent.submitter.id==='dodaj'){
            dodaj({naziv: e.target.naziv.value});
        }else{
            promjeni({naziv: e.target.naziv.value});
        }
        setOdabrano(null);
        e.target.naziv.value='';
    }

    function postaviOdabrano(e){
        setOdabrano(e);
    }
    


    return (

        <Container>
            <Row>
                <Col>
                    <Table striped bordered hover responsive>
                        <thead>
                            <tr>
                                <th>Kolicina</th>
                                <th>Akcija</th>
                            </tr>
                        </thead>
                        <tbody>
                            {lista && lista.map((e,index)=>(
                                <tr key={index}>
                                    <td className={odabrano!=null && odabrano.sifra==e.sifra ? 'podebljano' : ''}>
                                        {e.naziv}
                                    </td>
                                    
                                    <td className="sredina">
                                        <Button variant="primary" onClick={()=>postaviOdabrano(e)}>
                                            <FaEdit size={25} />
                                        </Button>
                                        <Button variant="danger" onClick={()=>obrisi(e.sifra)} >
                                            <FaTrash size={25} />
                                        </Button>

                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
                <Col>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group>
                            <Form.Label>Naziv</Form.Label>
                            <Form.Control
                                name='naziv'
                                defaultValue={odabrano!=null ? odabrano.naziv : ''}
                            />
                        </Form.Group>
                        <Button variant="success" type="submit" id="dodaj">
                            Dodaj
                        </Button>
                        {odabrano!=null ?
                        <Button variant="primary" type="submit" id="promjeni">
                            Promjeni
                        </Button>
                        :''}
                    </Form>
                </Col>
            </Row>
            
        </Container>

    );

}