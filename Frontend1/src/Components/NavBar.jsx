import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { useNavigate } from 'react-router-dom';
import { RoutesNames } from '../Constants';

import './NavBar.css';

function NavBar() {
    const navigate = useNavigate();
  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand 
            className='linkPocetna'
            onClick={()=>navigate(RoutesNames.HOME)}
        >
            Skladište Appl
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            
            <NavDropdown title="Izbornik" id="basic-nav-dropdown">
              <NavDropdown.Item 
                 onClick={()=>navigate(RoutesNames.OSOBE_PREGLED)}
              >
                Osobe
                </NavDropdown.Item>
              <NavDropdown.Item href="#action/3.2">
                Proizvodi
              </NavDropdown.Item>
              <NavDropdown.Item href="#action/3.3">
                Skladistari
                </NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item href="#action/3.4">
                Izdatnice
              </NavDropdown.Item>
            </NavDropdown>
           
          </Nav>
        </Navbar.Collapse>
        <Navbar.Collapse className="justify-content-end">
        <Nav.Link target="" href='http://http://romanzaric-001-site2.
        itempurl.com/swagger/index.html'>API dokumentacija</Nav.Link>
        </Navbar.Collapse>    
      </Container>
    </Navbar>
  );
}

export default NavBar;