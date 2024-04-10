import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import App from './App.jsx'
import { ErrorProvider } from './Components/ErrorContext.jsx'

import { AuthProvider } from './Components/AuthContext.jsx'
import { LoadingProvider } from './Components/LoadingContext.jsx'


ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
  <BrowserRouter>
    <ErrorProvider>
      <LoadingProvider>
        <AuthProvider>
            <App />
        </AuthProvider>
      </LoadingProvider>
    </ErrorProvider>
  </BrowserRouter>
</React.StrictMode>,
)
