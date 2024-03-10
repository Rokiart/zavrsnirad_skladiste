
import { Route, Routes,  } from 'react-router-dom'
import Pocetna from './Pages/Pocetna'
import { RoutesNames } from './constants'
import NavBar from './Components/NavBar'
import Osobe from './Pages/osobe/Osobe'


import 'bootstrap/dist/css/bootstrap.min.css'

import './App.css';
import OsobeDodaj from './Pages/osobe/OsobeDodaj'
import OsobePromjeni from './Pages/osobe/OsobePromjeni'

function App() {

  
  return (
    <>
     <NavBar />
     <Routes>
      <>
        <Route path={RoutesNames.HOME} element={<Pocetna/>} />
        <Route path={RoutesNames.OSOBE_PREGLED} element={<Osobe/>} />
        <Route path={RoutesNames.OSOBE_NOVI} element={<OsobeDodaj/>} />
        <Route path={RoutesNames.OSOBE_PROMJENI} element={<OsobePromjeni/>} />
      </>
     </Routes>
    </>
  )
}

export default App
