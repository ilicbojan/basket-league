import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { Field, Form } from 'react-final-form';
import Button from '../../../app/common/button/Button';
import Input from '../../../app/common/form/input/Input';
import { required } from '../../../app/common/util/validation';
import { ITeam } from '../../../app/models/team';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './TeamCreate.style';

const TeamCreate = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { createTeam } = rootStore.teamStore;

  return (
    <S.TeamCreate className='admin'>
      <div className='adminForm'>
        <h2>Create Team</h2>
        <Form
          onSubmit={(values: ITeam) => createTeam(values)}
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
    </S.TeamCreate>
  );
});

export default TeamCreate;
