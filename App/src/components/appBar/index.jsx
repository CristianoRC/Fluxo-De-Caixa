import { useNavigate } from 'react-router-dom';

import {
  Container,
  Button,
  Typography,
  Box,
  Toolbar,
  AppBar,
} from '@mui/material';

const pages = ['wallet', 'report', 'transaction'];

function AppBarComponent() {
  const navigate = useNavigate();
  const walletName = localStorage.getItem('walletName');

  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Typography
            variant="h6"
            sx={{
              mr: 8,
              letterSpacing: '.3rem',
              color: 'inherit',
              textDecoration: 'none',
            }}
          >
            {walletName}
          </Typography>

          <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
            {pages.map((page) => (
              <Button
                key={page}
                sx={{ my: 2, color: 'white', display: 'block' }}
                onClick={() => navigate(`/${page}`)}
              >
                {page}
              </Button>
            ))}
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default AppBarComponent;
