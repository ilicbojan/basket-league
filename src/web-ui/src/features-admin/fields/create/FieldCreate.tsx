import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { Field, Form } from 'react-final-form';
import Button from '../../../app/common/button/Button';
import Input from '../../../app/common/form/input/Input';
import Select from '../../../app/common/form/select/Select';
import { required } from '../../../app/common/util/validation';
import { IField } from '../../../app/models/field';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './FieldCreate.style';

const FieldCreate = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { createField } = rootStore.fieldStore;

  return (
    <S.FieldCreate className='admin'>
      <div className='adminForm'>
        <h2>Create Field</h2>
        <Form
          onSubmit={(values: IField) => createField(values)}
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
                name='address'
                type='text'
                label='Address'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='cityId'
                label='City'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='Beograd'>Beograd</option>
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
    </S.FieldCreate>
  );
});

export default FieldCreate;
