import { useEffect, useState } from "react";
import { Button,Container, Table } from "react-bootstrap";
import { ImManWoman } from "react-icons/im";
import { FaRegEdit } from "react-icons/fa";
import { FaRegTrashAlt } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import IzdatnicaService from "../../services/IzdatnicaService";
import { RoutesNames } from "../../constants";




export default function Izdatnice() {

    const [Izdatnice,setIzdatnice] = useState();
    const navigate = useNavigate();

    async function dohvatiIzdatnice(){
        await IzdatnicaService.get()
        .then((res)=>{

          
        })
        .catch((e)=>{
            alert(e);
        });
    }

    useEffect(()=>{
        dohvatiIzdatnice();
    },[]);

    async function ObrisiIzdatnicu(sifra){
      
        const odgovor = await IzdatnicaService.obrisi(sifra);
        if (odgovor.ok){
           
            dohvatiIzdatnice();
        

    } else {
        alert(odgovor.poruka);
    }
        
    }

    return(
        <Container>
             <Link to={RoutesNames.IZDATNICE_NOVI} className="btn btn-success gumb">
                <ImManWoman
                size={25}
                /> Dodaj
            </Link>
            <Table striped bordered hover responsive>
               <thead>
                  <tr>
                     <th>BrojIzdatnice</th>
                     <th>Datum</th>
                     {/* <th>Osoba</th>
                     <th>Skladistar</th> */}
                     <th>Napomena</th>
                     <th>Akcija</th>
                  </tr>
               </thead>
               <tbody>
                    {Izdatnice && Izdatnice.map((izdatnica,index)=>(
                        <tr key={index}>
                            <td>{izdatnica.brojizdatnice}</td>
                            <td>{izdatnica.datum}</td>
                            {/* <td>{izdatnica.osoba}</td>
                            <td>{izdatnica.skladistar}</td> */}
                            <td>{izdatnica.napomena}</td>
                            <td className="sredina">
                                <Button 
                                variant="primary"
                                onClick={()=>{navigate(`/izdatnica/${izdatnica.sifra}`)}}>
                                    <FaRegEdit
                                    size={25}
                                    />
                                </Button>
                                
                                    &nbsp;&nbsp;&nbsp;
                                <Button
                                    variant="danger"
                                    onClick={()=>ObrisiIzdatnicu(izdatnica.sifra)}
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