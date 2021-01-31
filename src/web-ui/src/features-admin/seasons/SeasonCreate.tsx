import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { Field, Form } from 'react-final-form';
import Button from '../../app/common/button/Button';
import Input from '../../app/common/form/input/Input';
import Select from '../../app/common/form/select/Select';
import { required } from '../../app/common/util/validation';
import { ISeason } from '../../app/models/season';
import { RootStoreContext } from '../../app/stores/rootStore';
import { S } from './SeasonCreate.style';

const SeasonCreate = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { createSeason } = rootStore.seasonStore;

  return (
    <S.SeasonCreate className='admin'>
      <div className='adminForm'>
        <h2>Create Season</h2>
        <Form
          onSubmit={(values: ISeason) => createSeason(values)}
          render={({ handleSubmit, submitting }) => (
            <form onSubmit={handleSubmit}>
              <Field
                name='name'
                type='text'
                label='Name'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='year'
                type='number'
                label='Year'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='leagueId'
                label='League'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                  {city.name}
                </option>
              ))} */}
                <option value='1'>1. Liga</option>
              </Field>
              <Field
                name='fieldId'
                label='Field'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                  {city.name}
                </option>
              ))} */}
                <option value='1'>Field</option>
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
    </S.SeasonCreate>
  );
});

export default SeasonCreate;
