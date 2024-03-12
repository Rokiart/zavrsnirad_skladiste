import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import ProizvodService from "../../services/ProizvodService";
import { IoIosAdd } from "react-icons/io";
import { FaEdit, FaTrash } from "react-icons/fa";
import { Link } from "react-router-dom";
import { RoutesNames } from "../../constants";
import { useNavigate } from "react-router-dom";


export default function Proizvodi(){
    const [Proizvodi,setProizvodi] = useState();
    let navigate = useNavigate(); 

    async function dohvatiProizvode(){
        await ProizvodService.get()
        .then((res)=>{
            setProizvodi(res.data);
        })
        .catch((e)=>{
            alert(e);
        });
    }

    useEffect(()=>{
       dohvatiProizvode();
    },[]);



    async function obrisiProizvod(sifra) {
        const odgovor = await ProizvodService.obrisi(sifra);
    
        if (odgovor.ok) {
            dohvatiProizvode();
        } else {
          alert(odgovor.poruka);
        }
      }

    return (

        <Container>
            <Link to={RoutesNames.PROIZVODI_NOVI} className="btn btn-success gumb">
                <IoIosAdd
                size={25}
                /> Dodaj
            </Link>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Naziv</th>
                        <th>SifraProizvoda</th>
                        <th>MjernaJedinica</th>
                        <th>Akcija</th>
                    </tr>
                </thead>
                <tbody>
                    {Proizvodi && Proizvodi.map((proizvod,index)=>(
                        <tr key={index}>
                            <td>{proizvod.naziv}</td>
                            <td>{proizvod.sifraProizvoda}</td>
                            <td>{proizvod.mjernaJedinica}</td>
                            <td className="sredina">
                                    <Button
                                        variant='primary'
                                        onClick={()=>{navigate(`/proizvodi/${proizvod.sifra}`)}}
                                    >
                                        <FaEdit 
                                    size={25}
                                    />
                                    </Button>
                               
                                
                                    &nbsp;&nbsp;&nbsp;
                                    <Button
                                        variant='danger'
                                        onClick={() => obrisiProizvod(proizvod.sifra)}
                                    >
                                        <FaTrash
                                        size={25}/>
                                    </Button>

                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>

    );

}