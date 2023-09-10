import { useState } from 'react';

import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';

import {
  TextField,
  Button,
  FormControl,
  OutlinedInput,
  InputLabel,
  MenuItem,
  Select,
} from '@mui/material';

// Components
import AppBarComponent from '../../components/appBar';
import NameBalanceInputField from '../../components/nameBalanceInputField';

// Menu Itens
const types = ['Credit', 'Debit'];

function Transaction() {
  const [send, setSend] = useState('');
  const [receive, setReceive] = useState('');
  const [type, setType] = useState('');
  const [value, setValue] = useState('');
  const [description, setDescription] = useState('');

  const create = () => {
    alert('Criado');
    console.log({
      send: send,
      received: receive,
      type: type,
      value: value,
      description: description,
    });
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
          {/* ---------------------TOP---------------------*/}
          <FormControl>
            <InputLabel>Transaction Type</InputLabel>
            <Select
              style={{
                width: 620,
              }}
              value={type}
              onChange={(event) => setType(event.target.value)}
              input={<OutlinedInput label="Transaction Type" />}
            >
              {types.map((type) => (
                <MenuItem key={type} value={type}>
                  {type}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          {/* ---------------------Mid---------------------*/}
          <div style={{ marginTop: 30 }}>
            <FormControl>
              <NameBalanceInputField
                labelProp="Send"
                setStateProp={setSend}
                stateProp={send}
              />
            </FormControl>
            <FormControl style={{ marginLeft: 20 }}>
              <NameBalanceInputField
                labelProp="Receive"
                setStateProp={setReceive}
                stateProp={receive}
              />
            </FormControl>
          </div>

          {/* ---------------------Bottom---------------------*/}
          <div
            style={{
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <FormControl style={{ marginTop: 30 }} variant="outlined">
              <TextField
                onChange={(event) => setValue(event.target.value)}
                value={value}
                style={{
                  width: 360,
                }}
                id="outlined-number"
                label="Value"
                type="number"
                InputLabelProps={{
                  shrink: true,
                }}
                InputProps={{
                  endAdornment: 'R$',
                }}
              />
            </FormControl>

            <FormControl style={{ width: 620, marginTop: 30 }}>
              <TextField
                fullWidth
                id="filled-textarea"
                label="Description"
                multiline
                rows={2}
                onChange={(event) => setDescription(event.target.value)}
              />
            </FormControl>
          </div>

          {/* ---------------------Action---------------------*/}

          <Grid container justifyContent={'center'}>
            <Grid item xs={2}>
              <Button
                fullWidth
                style={{
                  marginTop: 40,
                }}
                onClick={create}
                variant="contained"
              >
                Gerar
              </Button>
            </Grid>
          </Grid>
        </Grid>
      </Box>
    </>
  );
}

export default Transaction;
