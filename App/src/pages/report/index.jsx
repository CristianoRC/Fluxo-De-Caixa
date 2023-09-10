import { useState } from 'react';
import { FormControl, Grid, Box } from '@mui/material';
import { LoadingButton } from '@mui/lab'
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { Snackbar, Alert } from "@mui/material"

import AppBarComponent from '../../components/appBar';
import BalanceInputField from '../../components/nameBalanceInputField/index';
import axios from 'axios';

const Report = () => {
  const [balance, setBalance] = useState();
  const [datePicker, setDatePicker] = useState();
  const [loading, setLoading] = useState();
  const [showError, setShowError] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

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

      if (error?.response?.status == 400)
        setErrorMessage(error.response.data.error)
      else
        setErrorMessage('Ocorreu um erro, preencha todos campos ou tente mais tarde');
      setShowError(true);
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
          <img src="./report.svg" alt="icon-report" width={400} paddingRight={500} />
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
                      label="Data do relatório"
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
      <Snackbar open={showError} autoHideDuration={5000} onClose={() => setShowError(false)}>
        <Alert onClose={() => setShowError(false)} severity='error' sx={{ width: '100%' }}>
          {errorMessage}
        </Alert>
      </Snackbar>
    </>
  );
};

export default Report;
