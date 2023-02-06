import { ShoppingCart } from "@mui/icons-material";
import { AppBar, Badge, Box, List, ListItem, Switch, Toolbar, Typography } from "@mui/material";
import IconButton from "@mui/material/IconButton";
import { link } from "fs";
import { NavLink } from "react-router-dom";

interface HeaderProps {
  darkMode: boolean;
  setDarkMode: () => void;
}

const midLinks = [
  { title: 'Catalog', path: '/catalog' },
  { title: 'About', path: '/about' },
  { title: 'Contact', path: '/contact' },
]

const navStyle = {
  color: 'inherit',
  textDecoration: 'none',
  typography: 'h6',
  '&:hover': {
    color: 'grey.600'
  },
  '&.active': {
    color: 'text.secondary'
  }
}

const rightLinks = [
  { title: 'Login', path: '/login' },
  { title: 'Register', path: '/register' },
]

export default function Header({ darkMode, setDarkMode }: HeaderProps) {
  return (
      <AppBar position="static" sx={{ mb: 4 }}>
        <Toolbar sx={{display:'flex', justifyContent:'space-between', alignItems:'center'}}>

          <Box display='flex' alignItems={'center'}><Typography variant="h6" component={NavLink} to='/' exact sx={navStyle}>
            E-Commerce
          </Typography>
          <Switch checked={darkMode} onChange={setDarkMode} />
          </Box>
          
          <Box>
          <List sx={{ display: 'flex' }}>
            {midLinks.map(({ title, path }) => (
              <ListItem
                component={NavLink}
                to={path}
                key={path}
                sx={navStyle}
              >
                {title.toUpperCase()}
              </ListItem>
            ))}
          </List>
          </Box>
         
         <Box display={'flex'} alignItems={'center'}>
         <IconButton size="large" sx={{ color: "white" }}>
            <Badge badgeContent={0} color="secondary" >
              <ShoppingCart />
            </Badge>
          </IconButton>
          <List sx={{ display: 'flex' }}>
            {rightLinks.map(({ title, path }) => (
              <ListItem
                component={NavLink}
                to={path}
                key={path}
                sx={navStyle}
              >
                {title.toUpperCase()}
              </ListItem>
            ))}
          </List>
         </Box>
          
        </Toolbar>
      </AppBar>
  )
}