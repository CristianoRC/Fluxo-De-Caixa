import { Select, MenuItem, InputLabel, OutlinedInput } from '@mui/material';
import PropTypes from 'prop-types';

const names = ['Oliver Hansen', 'Van Henry', 'April Tucker'];

const NameBalanceInputField = (props) => {
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
        {names.map((name) => (
          <MenuItem key={name} value={name}>
            {name}
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
