import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import { Field, Form } from 'react-final-form';
import Button from '../../../app/common/button/Button';
import Input from '../../../app/common/form/input/Input';
import Select from '../../../app/common/form/select/Select';
import { required } from '../../../app/common/util/validation';
import { ILeague } from '../../../app/models/league';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './LeagueCreate.style';

const LeagueCreate = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { createLeague } = rootStore.leagueStore;
  const { loadCities, loading, cities } = rootStore.cityStore;

  useEffect(() => {
    loadCities();
  }, [loadCities]);

  return (
    <S.LeagueCreate className='admin'>
      <div className='adminForm'>
        <h2>Create League</h2>
        <Form
          onSubmit={(values: ILeague) => createLeague(values)}
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
                name='cityId'
                label='City'
                block
                disabled={loading}
                component={Select}
              >
                {cities.map((city) => (
                  <option key={city.id} value={city.id}>
                    {city.name}
                  </option>
                ))}
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
    </S.LeagueCreate>
  );
});

export default LeagueCreate;
