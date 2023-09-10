import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

import { primary } from '../../constants/colors';

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
  const [walletName, setWalletName] = useState();

  const createWalletAction = async () => {
    //em um ambiente real teria que pegar as urls base de um env*
    try {
      const body = { name: walletName };
      await axios.post("http://localhost:8081/api/balance", body)
    } catch (error) {
      alert(error)
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
                    backgroundColor: primary,
                    marginBottom: 30,
                  }}
                ></div>
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
    </>
  );
}

export default CreateWallet;
