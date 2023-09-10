import { useState } from 'react';
import { Snackbar, Alert, Box, Grid } from '@mui/material'

import {
  TextField,
  Button,
  FormControl,
  OutlinedInput,
  InputLabel,
  MenuItem,
  Select,
} from '@mui/material';

import AppBarComponent from '../../components/appBar';
import NameBalanceInputField from '../../components/nameBalanceInputField';
import axios from 'axios';

const types = [{ id: 0, name: 'Débito' }, { id: 1, name: 'Crédito' }];

function Transaction() {
  const [send, setSend] = useState('');
  const [receive, setReceive] = useState('');
  const [type, setType] = useState(0);
  const [value, setValue] = useState('');

  const [description, setDescription] = useState('');
  const [showSuccess, setShowSuccess] = useState(false);
  const [showError, setShowError] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  const createTransaction = async () => {
    var data = {
      entryBalance: send,
      offsetBalance: receive,
      amount: value,
      transactionType: type,
      description: description
    }

    try {
      await axios.post('http://localhost:8081/api/bookentry', data);
      setShowSuccess(true);
    }
    catch (error) {
      if (error.response.status == 400)
        setErrorMessage(error.response.data.error)
      else
        setErrorMessage('Ocorreu um erro, tente mais tarde');
      setShowError(true)
    }
  };

  return (
    <>
      <AppBarComponent />
      <Box>
        <Grid
          style={{ height: '90vh', flexWrap: 'wrap', flexDirection: 'column' }}
          container
          justifyContent={'center'}
          alignItems={'center'}
        >
          <FormControl>
            <InputLabel>Tipo de transação</InputLabel>
            <Select
              style={{
                width: 620,
              }}
              value={type}
              onChange={(event) => setType(event.target.value)}
              input={<OutlinedInput label='Tipo de transação' />}
            >
              {types.map((type) => (
                <MenuItem key={type.id} value={type.id}>
                  {type.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          <div style={{ marginTop: 30 }}>
            <FormControl>
              <NameBalanceInputField
                labelProp='Carteira de origem'
                setStateProp={setSend}
                stateProp={send}
              />
            </FormControl>
            <FormControl style={{ marginLeft: 20 }}>
              <NameBalanceInputField
                labelProp='Carteira de destino'
                setStateProp={setReceive}
                stateProp={receive}
              />
            </FormControl>
          </div>

          <div
            style={{
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <FormControl style={{ marginTop: 30 }} variant='outlined'>
              <TextField
                onChange={(event) => setValue(event.target.value)}
                value={value}
                style={{
                  width: 620,
                }}
                id='outlined-number'
                label='Valor'
                type='number'
                InputLabelProps={{ shrink: true, }}
                InputProps={{ endAdornment: 'R$', }}
              />
            </FormControl>

            <FormControl style={{ width: 620, marginTop: 30 }}>
              <TextField
                fullWidth
                id='filled-textarea'
                label='Descrição'
                multiline
                rows={2}
                onChange={(event) => setDescription(event.target.value)}
              />
            </FormControl>
          </div>


          <Grid container justifyContent={'center'}>
            <Grid item xs={2}>
              <Button
                fullWidth
                style={{
                  marginTop: 40,
                }}
                onClick={async () => { await createTransaction() }}
                variant='contained'
              >
                Criar
              </Button>
            </Grid>
          </Grid>
        </Grid>
      </Box>

      <Snackbar open={showSuccess} autoHideDuration={5000} onClose={() => setShowSuccess(false)}>
        <Alert onClose={() => setShowSuccess(false)} severity='success' sx={{ width: '100%' }}>
          Transação criada com sucesso!
        </Alert>
      </Snackbar>

      <Snackbar open={showError} autoHideDuration={5000} onClose={() => setShowError(false)}>
        <Alert onClose={() => setShowError(false)} severity='error' sx={{ width: '100%' }}>
          {errorMessage}
        </Alert>
      </Snackbar>
    </>
  );
}

export default Transaction;
