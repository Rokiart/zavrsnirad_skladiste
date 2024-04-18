import { useEffect, useState } from "react";
import { Button, Container, Form, Row, Table } from "react-bootstrap";
import useError from "../../hooks/useError";
import useLoading from "../../hooks/useLoading";
import Service from "../../services/IzdatnicaProizvodService";
import { FaEdit, FaTrash } from "react-icons/fa";



export default function IzdatniceProizvodi(){
    const [izdatniceProizvodi,setIzdatniceProizvodi] = useState();
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
        const odgovor = await Service.promjeni('IzdatniceProizvodi',odabrano.sifra,IzdatniceProizvodi);
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
            dodaj({kolicina: e.target.kolicina.value});
        }else{
            promjeni({kolicina: e.target.kolicina.value});
        }
        setOdabrano(null);
        e.target.kolicina.value='';
    }

    function Odabrano(e){
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
                                        {e.kolicina}
                                    </td>
                                    
                                    <td className="sredina">
                                        <Button variant="primary" onClick={()=>postaviIzdatniceProizvodi(e)}>
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
                            <Form.Label>Kolicina</Form.Label>
                            <Form.Control
                                name='kolicina'
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