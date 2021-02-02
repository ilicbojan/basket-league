import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
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
  const { loadFields, loading: loadingFields, fields } = rootStore.fieldStore;
  const {
    loadLeagues,
    loading: loadingLeagues,
    leagues,
  } = rootStore.leagueStore;

  useEffect(() => {
    loadLeagues();
    loadFields();
  }, [loadLeagues, loadFields]);

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
                disabled={loadingLeagues}
                component={Select}
              >
                {leagues.map((league) => (
                  <option key={league.id} value={league.id}>
                    {league.name}
                  </option>
                ))}
              </Field>
              <Field
                name='fieldId'
                label='Field'
                block
                disabled={loadingFields}
                component={Select}
              >
                {fields.map((field) => (
                  <option key={field.id} value={field.id}>
                    {field.name}
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
    </S.SeasonCreate>
  );
});

export default SeasonCreate;
