import { useState } from 'react';
import { Snackbar, Alert } from '@mui/material';

import {
  Typography,
  Button,
  CardContent,
  CardActions,
  Card,
  Grid,
  Box,
  TextField,
} from '@mui/material';

import AppBarComponent from '../../components/appBar';
import axios from 'axios';

function CreateWallet() {
  const [walletName, setWalletName] = useState("");
  const [showSuccess, setShowSuccess] = useState(false);
  const [showError, setShowError] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  const createWalletAction = async () => {
    //em um ambiente real teria que pegar as urls base de um env*
    try {
      const body = { name: walletName };
      await axios.post("http://localhost:8081/api/balance", body);
      setShowSuccess(true);
    } catch (error) {
      if (error.response.status == 400)
        setErrorMessage("Preencha as informações do da carteira!")
      else
        setErrorMessage("Ocorreu um erro, tente mais tarde")
      setShowError(true);
    }
  };

  return (
    <>
      <AppBarComponent />
      <Box>
        <Grid
          style={{ height: '90vh' }}
          container
          justifyContent={'center'}
          alignItems={'center'}
        >
          <Grid item xs={4}>
            <Card>
              <CardContent>
                <div
                  style={{
                    height: 30,
                    marginBottom: 30,
                  }}
                ></div>
                <img src="./wallet.svg" alt="icon-transaction" width={200}
                  style={{
                    marginBottom: '50px',
                    marginTop: "25px"
                  }} />
                <Typography variant="h5" component="div">
                  Criar Carteira
                </Typography>
                <Typography color="text.secondary">
                  Digite o nome da carteira
                </Typography>
                <TextField
                  style={{ marginTop: 20 }}
                  fullWidth
                  id="outlined-basic"
                  variant="outlined"
                  onChange={(event) => setWalletName(event.target.value)}
                />
              </CardContent>
              <CardActions>
                <Button onClick={createWalletAction}>Criar</Button>
              </CardActions>
            </Card>
          </Grid>
        </Grid>
      </Box>
      <Snackbar open={showSuccess} autoHideDuration={5000} onClose={() => setShowSuccess(false)}>
        <Alert onClose={() => setShowSuccess(false)} severity="success" sx={{ width: '100%' }}>
          Carteira criada com sucesso!
        </Alert>
      </Snackbar>

      <Snackbar open={showError} autoHideDuration={5000} onClose={() => setShowError(false)}>
        <Alert onClose={() => setShowError(false)} severity="error" sx={{ width: '100%' }}>
          {errorMessage}
        </Alert>
      </Snackbar>
    </>
  );
}

export default CreateWallet;
