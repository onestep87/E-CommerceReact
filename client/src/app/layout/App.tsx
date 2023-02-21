import { useEffect, useState } from "react";
import Catalog from "../../features/Catalog/Catalog";
import Header from "./Header";
import { Container, CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import { Route, Switch } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ProductDetails from "../../features/Catalog/ProductDetails";
import AboutPage from "../../features/about/AboutPage";
import ContactPage from "../../features/contact/ContactPage";
import { ToastContainer } from "react-toastify";
import NotFound from "../api/errors/NotFound";
import ServerError from "../api/errors/ServerError";
import 'react-toastify/dist/ReactToastify.css'
import agent from "../api/agent";

import { getCookie } from "../util/util";
import { useStoreContext } from "../context/StoreContext";
import BasketPage from "../../features/basket/BasketPage";


export default function App() {
  const {setBasket} = useStoreContext();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const buyerId = getCookie('buyerId');
    if (buyerId) {
      agent.Basket.get()
        .then(basket => setBasket(basket))
        .catch(error => console.log(error))
        .finally(() => setLoading(false));
    } else {
      setLoading(false);
    }
  }, [setBasket])


  const [darkMode, setDarkMode] = useState(false);
  const palleteType = darkMode ? "dark" : "light";
const theme = createTheme({
  palette: {
    mode: palleteType,
    background: {
     default: palleteType === 'light' ?'#eaeaea' : '#121212' }
  }
});

function handleDarkMode() {
  setDarkMode(!darkMode);
}

  return (
    <ThemeProvider theme={theme}>
      <ToastContainer position='bottom-right' hideProgressBar theme='colored' />
      <CssBaseline />
      <Header darkMode={darkMode}  setDarkMode={handleDarkMode}/>
      <Container>
        <Switch>
          <Route exact path='/' component={HomePage} />
          <Route exact path='/catalog' component={Catalog} />
          <Route path='/catalog/:id' component={ProductDetails} />
          <Route path='/about' component={AboutPage} />
          <Route path='/basket' component={BasketPage} />
          <Route path='/contact' component={ContactPage} />
          <Route path='/server-error' component={ServerError} />
          <Route component={NotFound} />
        </Switch>
      </Container>
    </ThemeProvider>
  );
}

