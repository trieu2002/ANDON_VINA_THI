import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App'
import {ToastContainer} from 'react-toastify'
import "react-toastify/dist/ReactToastify.css";
import { Provider } from "react-redux";
import { store } from "./redux/store/store.ts";
createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
        <App />
        <ToastContainer position="top-right" autoClose={3000} />
    </Provider>
  </StrictMode>,
)
