import { useState } from 'react';
import {
  FormControl,
  Grid,
  Box,
  Snackbar,
  Alert,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Typography,
  Chip,
} from '@mui/material';
import { LoadingButton } from '@mui/lab';
import axios from 'axios';

import AppBarComponent from '../../components/appBar';
import BalanceInputField from '../../components/nameBalanceInputField/index';
import { primary } from '../../constants/colors';

const transactionTypeLabel = {
  0: 'Débito',
  1: 'Crédito',
};

const transactionTypeColor = {
  0: 'error',
  1: 'success',
};

const formatCurrency = (value) =>
  new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(value);

const formatDate = (dateString) =>
  new Date(dateString).toLocaleString('pt-BR', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });

const Statement = () => {
  const [balance, setBalance] = useState();
  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(false);
  const [showError, setShowError] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');
  const [searched, setSearched] = useState(false);

  const loadStatement = async () => {
    if (!balance) {
      setErrorMessage('Selecione uma conta bancária');
      setShowError(true);
      return;
    }

    try {
      setLoading(true);
      const response = await axios.get(
        `http://localhost:8081/api/balance/${balance}/statement`
      );
      setTransactions(response.data);
      setSearched(true);
    } catch (error) {
      if (error?.response?.status === 400)
        setErrorMessage(error.response.data.error || 'Requisição inválida');
      else
        setErrorMessage('Ocorreu um erro, tente mais tarde');
      setShowError(true);
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <AppBarComponent />
      <Box>
        <Grid
          style={{ height: 'auto', paddingTop: 60, paddingBottom: 40 }}
          container
          justifyContent={'center'}
          alignItems={'center'}
        >
          <img src="./statement.svg" alt="icon-statement" width={200} style={{ marginRight: 40 }} />
          <FormControl sx={{ m: 1, width: '100%', maxWidth: 350 }}>
            <div
              style={{
                display: 'flex',
                marginBottom: 20,
              }}
            >
              <BalanceInputField
                labelProp="Selecione a conta"
                setStateProp={setBalance}
                stateProp={balance}
              />
            </div>
            <LoadingButton
              loading={loading}
              variant="contained"
              style={{ marginTop: 10 }}
              onClick={loadStatement}
            >
              Consultar
            </LoadingButton>
          </FormControl>
        </Grid>

        {searched && transactions.length === 0 && (
          <Grid container justifyContent={'center'} sx={{ pb: 4 }}>
            <Typography color="text.secondary">
              Nenhuma transação encontrada para esta conta.
            </Typography>
          </Grid>
        )}

        {transactions.length > 0 && (
          <Grid container justifyContent={'center'} sx={{ pb: 4 }}>
            <TableContainer
              component={Paper}
              sx={{ maxWidth: 900, width: '90%' }}
            >
              <Table>
                <TableHead>
                  <TableRow sx={{ backgroundColor: primary }}>
                    <TableCell sx={{ color: 'white', fontWeight: 'bold' }}>
                      Data
                    </TableCell>
                    <TableCell sx={{ color: 'white', fontWeight: 'bold' }}>
                      Origem
                    </TableCell>
                    <TableCell sx={{ color: 'white', fontWeight: 'bold' }} align="center">
                      Tipo
                    </TableCell>
                    <TableCell sx={{ color: 'white', fontWeight: 'bold' }} align="right">
                      Valor
                    </TableCell>
                    <TableCell sx={{ color: 'white', fontWeight: 'bold' }} align="right">
                      Saldo
                    </TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {transactions.map((tx) => (
                    <TableRow
                      key={tx.id}
                      sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                    >
                      <TableCell>{formatDate(tx.createdAt)}</TableCell>
                      <TableCell>{tx.origin}</TableCell>
                      <TableCell align="center">
                        <Chip
                          label={transactionTypeLabel[tx.type]}
                          color={transactionTypeColor[tx.type]}
                          size="small"
                        />
                      </TableCell>
                      <TableCell
                        align="right"
                        sx={{
                          color: tx.type === 1 ? 'green' : 'red',
                          fontWeight: 'bold',
                        }}
                      >
                        {tx.type === 1 ? '+' : '-'} {formatCurrency(tx.amount)}
                      </TableCell>
                      <TableCell align="right" sx={{ fontWeight: 'bold' }}>
                        {formatCurrency(tx.balanceAfterTransaction)}
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>
        )}
      </Box>

      <Snackbar open={showError} autoHideDuration={5000} onClose={() => setShowError(false)}>
        <Alert onClose={() => setShowError(false)} severity='error' sx={{ width: '100%' }}>
          {errorMessage}
        </Alert>
      </Snackbar>
    </>
  );
};

export default Statement;
