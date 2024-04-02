import { useEffect, useState } from "react";
import { Button,Container, Table } from "react-bootstrap";
import OsobaService from "../../services/OsobaService";
import { ImManWoman } from "react-icons/im";
import { FaRegEdit } from "react-icons/fa";
import { FaRegTrashAlt } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import { RoutesNames } from "../../constants";
import useError from "../../hooks/useError";



export default function Osobe() {

    const [osobe,setOsobe] = useState();
    let navigate = useNavigate();
    const { prikaziError } = useError();
    

    async function dohvatiOsobe(){
        const odgovor = await OsobaService.get('Osoba');
        if(!odgovor.ok){
            prikaziError(odgovor.podaci);
            return;
        }
        setOsobe(odgovor.podaci);
    }

    async function ObrisiOsobu(sifra){
        const odgovor = await OsobaService.obrisi('Osoba',sifra);
        prikaziError(odgovor.podaci);
        if (odgovor.ok){
            
            dohvatiOsobe();
        }    
    }


    useEffect(()=>{
        dohvatiOsobe();
    },[]);

   
    return(
        <Container>
             <Link to={RoutesNames.OSOBE_NOVI} className="btn btn-success gumb">
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
                    {osobe && osobe.map((osoba,index)=>(
                        <tr key={index}>
                            <td>{osoba.ime}</td>
                            <td>{osoba.prezime}</td>
                            <td>{osoba.brojtelefona}</td>
                            <td>{osoba.email}</td>
                            <td className="sredina">
                                <Button 
                                variant="primary"
                                onClick={()=>{navigate(`/osobe/${osoba.sifra}`)}}>
                                    <FaRegEdit
                                    size={25}
                                    />
                                </Button>
                                
                                    &nbsp;&nbsp;&nbsp;
                                <Button
                                    variant="danger"
                                    onClick={()=>ObrisiOsobu(osoba.sifra)}
                                >
                                    <FaRegTrashAlt 
                                    size={25}
                                    />
                                </Button>

                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
      
    );


}