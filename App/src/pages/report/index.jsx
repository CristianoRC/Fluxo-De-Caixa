import { useState } from 'react';
import { FormControl, Grid, Box } from '@mui/material';
import { LoadingButton } from '@mui/lab'
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
  const [loading, setLoading] = useState();

  const generate = async () => {
    try {
      setLoading(true)
      const date = datePicker.format("DD-MM-YYYY");
      const url = `http://localhost:8082/api/report?date=${date}&balance=${balance}`;
      const response = await axios({
        url,
        method: 'GET',
        responseType: 'blob',
      })
      download(response)

    } catch (error) {
      alert(error)
    }
    finally {
      setLoading(false)
    }

  };

  const download = (response) => {
    const href = window.URL.createObjectURL(response.data);
    const anchorElement = document.createElement('a');
    anchorElement.href = href;
    anchorElement.download = `${balance}.pdf`;
    document.body.appendChild(anchorElement);
    anchorElement.click();
    document.body.removeChild(anchorElement);
    window.URL.revokeObjectURL(href);
  }

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
            <LoadingButton
              loading={loading}
              variant="contained"
              style={{ marginTop: 10 }}
              onClick={generate}
            >Gerar
            </LoadingButton>
          </FormControl>
        </Grid>
      </Box>
    </>
  );
};

export default Report;
