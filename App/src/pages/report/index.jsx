import { useState } from 'react';
import dayjs from 'dayjs';
import { FormControl, Button, Grid, Box } from '@mui/material';

import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';

import AppBarComponent from '../../components/appBar';
import BalanceInputField from '../../components/nameBalanceInputField/index';
import axios from 'axios';

const Report = () => {
  const [balance, setBalance] = useState();
  const [datePicker, setDatePicker] = useState();

  const Generate = async () => {
    const date = `${datePicker.get("D")}-${datePicker.get("M")}-${datePicker.get("y")}`
    console.log(date);
    const url = `http://localhost:8082/api/report?date=${date}&balance=${balance}`;
    const response = await axios.get(url)
    console.log(response);
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
          <FormControl sx={{ m: 1, witdh: '100%' }}>
            <div
              style={{
                display: 'flex',
                marginBottom: 40,
              }}
            >
              <BalanceInputField
                labelProp="Carteiras"
                setStateProp={setBalance}
                stateProp={balance}
              />

              <div style={{ marginTop: '-8px', marginLeft: 10 }}>
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                  <DemoContainer components={['DatePicker']}>
                    <DatePicker
                      label="Basic date picker"
                      disableFuture={true}
                      value={datePicker || null}
                      onChange={(newValue) => setDatePicker(newValue)}
                    />
                  </DemoContainer>
                </LocalizationProvider>
              </div>
            </div>
            <Button
              onClick={Generate}
              variant="contained"
              style={{ marginTop: 10 }}
            >
              Gerar
            </Button>
          </FormControl>
        </Grid>
      </Box>
    </>
  );
};

export default Report;
