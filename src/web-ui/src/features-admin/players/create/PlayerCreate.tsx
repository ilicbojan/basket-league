import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import { Field, Form } from 'react-final-form';
import Button from '../../../app/common/button/Button';
import Input from '../../../app/common/form/input/Input';
import Select from '../../../app/common/form/select/Select';
import { required } from '../../../app/common/util/validation';
import { IPlayer } from '../../../app/models/player';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './PlayerCreate.style';

const PlayerCreate = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { createPlayer } = rootStore.playerStore;
  const { loadTeams, loading, teams } = rootStore.teamStore;

  useEffect(() => {
    loadTeams();
  }, [loadTeams]);

  return (
    <S.PlayerCreate className='admin'>
      <div className='adminForm'>
        <h2>Create Player</h2>
        <Form
          onSubmit={(values: IPlayer) => createPlayer(values)}
          render={({ handleSubmit, submitting }) => (
            <form onSubmit={handleSubmit}>
              <Field
                name='email'
                type='email'
                label='Email'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='firstName'
                type='text'
                label='First Name'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='lastName'
                type='text'
                label='Last Name'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='jmbg'
                type='text'
                label='JMBG'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='phoneNumber'
                type='text'
                label='Phone Number'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='password'
                type='password'
                label='Password'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='jerseyNumber'
                type='number'
                label='Jersey Number'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='teamId'
                label='Team'
                block
                disabled={loading}
                component={Select}
              >
                {teams.map((team) => (
                  <option key={team.id} value={team.id}>
                    {team.name}
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
    </S.PlayerCreate>
  );
});

export default PlayerCreate;
