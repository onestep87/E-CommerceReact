import { useState } from "react";
import Catalog from "../../features/Catalog/Catalog";
import Header from "./Header";
import { Container, CssBaseline, ThemeProvider, createTheme } from "@mui/material";


function App() {
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
      <Catalog />
      </Container>
    </ThemeProvider>
  );
}

export default App;
