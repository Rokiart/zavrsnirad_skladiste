import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { useNavigate } from 'react-router-dom';
import { RoutesNames, App } from '../constants';
import useAuth from '../hooks/useAuth';
import './NavBar.css';


function NavBar() {

  const navigate  = useNavigate ();
  const { logout, isLoggedIn } = useAuth();

  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand
          className='linkPocetna' 
           onClick={()=>navigate(RoutesNames.HOME)} 
        >
          Skladište APP
          </Navbar.Brand>

          {isLoggedIn ? (
          <>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">

          <NavDropdown title="Izbornik" id="basic-nav-dropdown">
              
              <NavDropdown.Item 
                onClick={()=>navigate(RoutesNames.OSOBE_PREGLED)}
              >
                Osobe
                </NavDropdown.Item>
              <NavDropdown.Item 
              onClick={()=>navigate(RoutesNames.PROIZVODI_PREGLED)}
              >
                Proizvodi
              </NavDropdown.Item>
              <NavDropdown.Item 
              onClick={()=>navigate(RoutesNames.SKLADISTARI_PREGLED)}
              >
                Skladistari
                </NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item
               onClick={()=>navigate(RoutesNames.IZDATNICE_PREGLED)}
               >
               Izdatnice
              </NavDropdown.Item>
            </NavDropdown>


          </Nav>
          </Navbar.Collapse>
            <Navbar.Collapse className="justify-content-end">
                <Nav>
                  <Nav.Link onClick={logout}>Odjava</Nav.Link>
                  <Nav.Link target="_blank" href={App.URL + '/swagger/index.html'}>API dokumentacija</Nav.Link>
                </Nav>
              </Navbar.Collapse>
        </>
         ) : (
          <>
          <Navbar.Collapse className="justify-content-end">
            <Nav.Link onClick={() => navigate(RoutesNames.LOGIN)}>
              Prijava
            </Nav.Link>
          </Navbar.Collapse>
          </>
        )}
      </Container>
    </Navbar>
  );
}

export default  NavBar;