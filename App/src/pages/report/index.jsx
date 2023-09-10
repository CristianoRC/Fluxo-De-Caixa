import { useState } from 'react';

import { FormControl, Button, Grid, Box } from '@mui/material';

// Date Picker
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';

// Components
import AppBarComponent from '../../components/appBar';
import NameBalanceInputField from '../../components/nameBalanceInputField';

const Report = () => {
  const [nameBalance, setNameBalance] = useState();
  const [datePicker, setDatePicker] = useState();

  const Generate = () => {
    alert([nameBalance, datePicker]);
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
              <NameBalanceInputField
                labelProp="Carteiras"
                setStateProp={setNameBalance}
                stateProp={nameBalance}
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
