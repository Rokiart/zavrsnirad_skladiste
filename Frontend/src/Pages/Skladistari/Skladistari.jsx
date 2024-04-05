import { useEffect, useState } from "react";
import { Button,Container, Form, Modal, Table } from "react-bootstrap";
import SkladistarService from "../../services/SkladistarService";
import { ImManWoman } from "react-icons/im";
import { FaDownload, FaEdit, FaTrash, FaUpload } from "react-icons/fa";

import { Link, useNavigate } from "react-router-dom";
import { RoutesNames } from "../../constants";
import useError from "../../hooks/useError";


export default function Skladistari() {

    const [Skladistari,setSkladistari] = useState();
    const navigate = useNavigate();
    const { prikaziError } = useError();
    const [prikaziModal, setPrikaziModal] = useState(false);
    const [odabraniPredavac,setOdabraniPredavac] = useState({});

    async function dohvatiSkladistare(){
        const odgovor = await SkladistarService.get('Skladistar')
        if(!odgovor.ok){
            prikaziError(odgovor.podaci);
            return;
        }
        setSkladistari(odgovor.podaci);
    }

    async function ObrisiSkladistara(sifra){
        const odgovor = await SkladistarService.obrisi('Skladistar',sifra);
        prikaziError(odgovor.podaci);
        if (odgovor.ok){
            dohvatiSkladistare();
        }
        
    }

    useEffect(()=>{
        dohvatiSkladistare();
    },[]);

    function postaviDatotekuModal(skladistar){
        setOdabraniPredavac(skladistar);
        setPrikaziModal(true);
    }

    function zatvoriModal(){
        setPrikaziModal(false);
    }

    async function postaviDatoteku(e){
        if (e.currentTarget.files) {
            const formData = new FormData();
            formData.append('datoteka', e.currentTarget.files[0]);
            const config = {
            headers: {
                'content-type': 'multipart/form-data',
            },
            };
            const odgovor = await Service.postaviDatoteku(odabraniSkladistar.sifra,formData,config);
            alert(dohvatiPorukeAlert(odgovor.podaci));
            if (odgovor.ok){
                dohvatiSkladistare();
                setPrikaziModal(false);
            }
        }
    }


   

    return(
        <>
        <Container>
             <Link to={RoutesNames.SKLADISTARI_NOVI} className="btn btn-success gumb">
                <ImManWoman
                size={25}
                /> Dodaj
            </Link>
            <Table striped bordered hover responsive>
               <thead>
                  <tr>
                     <th>Ime</th>
                     <th>Prezime</th>
                     <th>Broj Telefona</th>
                     <th>Email</th>
                     <th>Akcija</th>
                  </tr>
               </thead>
               <tbody>
                    {Skladistari && Skladistari.map((skladistar,index)=>(
                        <tr key={index}>
                            <td>{skladistar.ime}</td>
                            <td>{skladistar.prezime}</td>
                            <td>{skladistar.brojtelefona}</td>
                            <td>{skladistar.email}</td>
                            <td className="sredina">
                            <Button
                                        variant='primary'
                                        onClick={()=>{navigate(`/skladistari/${skladistar.sifra}`)}}
                                    >
                                        <FaEdit 
                                    size={25}
                                    />
                                    </Button>
                               
                                
                                    &nbsp;&nbsp;&nbsp;
                                    <Button
                                        variant='danger'
                                        onClick={() => ObrisiSkladistara(skladistar.sifra)}
                                    >
                                        <FaTrash
                                        size={25}/>
                                    </Button>

                                   

                                        {skladistar.datoteka!=null ? 
                                        <>
                                        &nbsp;&nbsp;&nbsp;
                                        <a target="_blank" href={App.URL + skladistar.datoteka}>
                                            <FaDownload
                                            size={25}/>
                                        </a>
                                        </>
                                        
                                    : ''
                                    }
                                    &nbsp;&nbsp;&nbsp;
                                        <Button
                                            onClick={() => postaviDatotekuModal(skladistar)}
                                        >
                                            <FaUpload
                                            size={25}/>
                                        </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
            <Modal show={prikaziModal} onHide={zatvoriModal}>
                <Modal.Header closeButton>
                <Modal.Title>Postavljanje datoteke na <br /> {odabraniSkladistar.prezime}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Control type="file" size="lg" 
                            name='datoteka'
                            id='datoteka'
                            onChange={postaviDatoteku}
                            />
                        </Form.Group>
                        <hr />
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                <Button variant='secondary' onClick={zatvoriModal}>
                    Zatvori
                </Button>
                </Modal.Footer>
            </Modal>
        </>
    );

}