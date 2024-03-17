import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import ProizvodService from "../../services/ProizvodService";
import { IoIosAdd } from "react-icons/io";
import { FaEdit, FaTrash } from "react-icons/fa";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { RoutesNames } from "../../constants";



export default function Proizvodi(){
    const [Proizvodi,setProizvodi] = useState();
    const navigate = useNavigate(); 

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
                    {Proizvodi && Proizvodi.map((Proizvod,index)=>(
                        <tr key={index}>
                            <td>{Proizvod.naziv}</td>
                            <td>{Proizvod.sifraproizvoda}</td>
                            <td>{Proizvod.mjernajedinica}</td>
                            <td className="sredina">
                                    <Button
                                        variant='primary'
                                        onClick={()=>{navigate(`/proizvodi/${Proizvod.sifra}`)}}
                                    >
                                        <FaEdit 
                                    size={25}
                                    />
                                    </Button>
                               
                                
                                    &nbsp;&nbsp;&nbsp;
                                    <Button
                                        variant='danger'
                                        onClick={() => obrisiProizvod(Proizvod.sifra)}
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