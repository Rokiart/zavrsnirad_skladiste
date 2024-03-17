import { useEffect, useState } from "react";
import { Button,Container, Table } from "react-bootstrap";
import SkladistarService from "../../services/SkladistarService";
import { ImManWoman } from "react-icons/im";
import { FaRegEdit } from "react-icons/fa";
import { FaRegTrashAlt } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import { RoutesNames } from "../../constants";



export default function Skladistari() {

    const [Skladistari,setSkladistari] = useState();
    const navigate = useNavigate();

    async function dohvatiSkladistare(){
        await SkladistarService.get()
        .then((res)=>{
            setSkladistari(res.data);
        })
        .catch((e)=>{
            alert(e);
        });
    }

    useEffect(()=>{
        dohvatiSkladistare();
    },[]);

    async function ObrisiSkladistara(sifra){
        const odgovor = await SkladistarService.obrisi(sifra);
        if (odgovor.ok){
            
            dohvatiSkladistare();
        } else {
            alert(odgovor.poruka);
        }
        
    }

    return(
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
                                variant="primary"
                                onClick={()=>{navigate(`/skladistari/${skladistar.sifra}`)}}>
                                    <FaRegEdit
                                    size={25}
                                    />
                                </Button>
                                
                                    &nbsp;&nbsp;&nbsp;
                                <Button
                                    variant="danger"
                                    onClick={()=>ObrisiSkladistara(skladistar.sifra)}
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