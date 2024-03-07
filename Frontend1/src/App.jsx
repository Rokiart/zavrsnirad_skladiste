import { Route, Routes } from "react-router-dom";
import Pocetna from "./pages/Pocetna";
import { RoutesNames } from "./Constants";
import NavBar from "./Components/NavBar";
import Osobe from "./pages/Osobe/Osobe";
import 'bootstrap/dist/css/bootstrap.min.css'
import '.App.css';
import OsobeDodaj from "./pages/Osobe/OsobeDodaj";




function App() {
  

  return (
    <>
      <NavBar />
       <Routes>  
        <>
          <Route path={RoutesNames.HOME} element={<Pocetna />} />
          <Route path={RoutesNames.OSOBE_PREGLED} element={<Osobe />} />
          <Route path={RoutesNames.OSOBE_NOVE} element={<OsobeDodaj />} />
          <Route path={RoutesNames.OSOBE_PROMJENI} element={<OsobePromjeni />} />
        </>   
      </Routes>
   </>
  )
}

export default App
