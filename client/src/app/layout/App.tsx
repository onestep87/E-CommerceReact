import { useState } from "react";
import Catalog from "../../features/Catalog/Catalog";
import Header from "./Header";
import { Container, CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import { Route } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import ProductDetails from "../../features/Catalog/ProductDetails";
import AboutPage from "../../features/about/AboutPage";
import ContactPage from "../../features/contact/ContactPage";


export default function App() {
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
      <CssBaseline/>
      <Header darkMode={darkMode}  setDarkMode={handleDarkMode}/>
      <Container>
      <Route exact path="/" component={HomePage} />
      <Route exact path="/catalog" component={Catalog} />
      <Route exact path="/catalog/:id" component={ProductDetails} />
      <Route exact path="/about" component={AboutPage} />
      <Route exact path="/contact" component={ContactPage} />

      </Container>
    </ThemeProvider>
  );
}

