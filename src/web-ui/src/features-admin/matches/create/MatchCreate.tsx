import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { Field, Form } from 'react-final-form';
import Button from '../../../app/common/button/Button';
import Input from '../../../app/common/form/input/Input';
import Select from '../../../app/common/form/select/Select';
import { required } from '../../../app/common/util/validation';
import { IMatch } from '../../../app/models/match';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './MatchCreate.style';

const MatchCreate = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { createMatch } = rootStore.matchStore;

  return (
    <S.MatchCreate className='admin'>
      <div className='adminForm'>
        <h2>Create Match</h2>
        <Form
          onSubmit={(values: IMatch) => console.log(values)}
          render={({ handleSubmit, submitting }) => (
            <form onSubmit={handleSubmit}>
              <Field
                name='seasonId'
                label='Season'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='1'>1. League Summer</option>
              </Field>
              <Field
                name='round'
                type='number'
                label='Round'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='homeTeamId'
                label='Home Team'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='1'>Team 1</option>
              </Field>
              <Field
                name='awayTeamId'
                label='Away Team'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='1'>Team 1</option>
              </Field>
              <Field
                name='date'
                type='date'
                label='Date'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='time'
                label='Time'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='16:00:00'>16:00</option>
              </Field>
              <Field
                name='refereeId'
                label='Referee'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='1'>Ref</option>
              </Field>
              <Field
                name='delegateId'
                label='Delegate'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='1'>Del</option>
              </Field>

              <Button
                disabled={submitting}
                loading={submitting}
                color='primary'
                block
              >
                Create
              </Button>
            </form>
          )}
        ></Form>
      </div>
    </S.MatchCreate>
  );
});

export default MatchCreate;
