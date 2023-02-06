import { AppBar, Box, Switch, Toolbar } from "@mui/material";

interface HeaderProps {
    darkMode: boolean;
    setDarkMode: () => void;
}

export default function Header({darkMode, setDarkMode}: HeaderProps) {
    return (
        <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static" sx={{mb: 4}}>
        <Toolbar>
          <h1>E-Commerce</h1>
          <Switch checked={darkMode} onChange={setDarkMode}/>
        </Toolbar>
      </AppBar>
    </Box>
    )
}