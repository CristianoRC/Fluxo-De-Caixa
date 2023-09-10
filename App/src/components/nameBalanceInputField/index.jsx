import { Select, MenuItem, InputLabel, OutlinedInput } from '@mui/material';
import PropTypes from 'prop-types';
import { useState, useEffect } from 'react';
import axios from 'axios';

const NameBalanceInputField = (props) => {
  const [balances, setBalances] = useState([]);

  const updateBalances = async () => {
    var balances = await axios.get("http://localhost:8081/api/balance")
    setBalances(balances.data);
  }

  useEffect(() => { updateBalances() }, [])

  return (
    <>
      <InputLabel>{props.labelProp}</InputLabel>
      <Select
        value={props.stateProp || ''}
        style={{
          width: 300,
          height: 55,
        }}
        onChange={(event) => props.setStateProp(event.target.value)}
        input={<OutlinedInput label={props.labelProp} />}
      >
        {balances.map((balance) => (
          <MenuItem key={balance.id} value={balance.id}>
            {balance.name}
          </MenuItem>
        ))}
      </Select>
    </>
  );
};

NameBalanceInputField.propTypes = {
  labelProp: PropTypes.string,
  setStateProp: PropTypes.func,
  stateProp: PropTypes.string,
};

export default NameBalanceInputField;
