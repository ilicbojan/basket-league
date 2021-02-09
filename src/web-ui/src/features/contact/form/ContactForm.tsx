import React from 'react';
import { Field, Form } from 'react-final-form';
import Button from '../../../app/common/button/Button';
import Input from '../../../app/common/form/input/Input';
import { required } from '../../../app/common/util/validation';
import { S } from './ContactForm.style';

const ContactForm = () => {
  return (
    <S.ContactForm>
      <h2>Create team</h2>
      <Form
        onSubmit={(values) => alert('Message sent')}
        render={({ invalid, pristine, handleSubmit }) => (
          <form onSubmit={handleSubmit}>
            <Field
              name='name'
              label='Name'
              type='text'
              block
              validate={required}
              component={Input}
            />
            <Field
              name='email'
              label='Email'
              type='text'
              block
              validate={required}
              component={Input}
            />
            <Field
              name='phoneNumber'
              label='Phone number'
              type='text'
              block
              validate={required}
              component={Input}
            />
            <Field
              name='teamName'
              label='Team name'
              type='text'
              block
              validate={required}
              component={Input}
            />

            <Button color='primary' block>
              Create
            </Button>
          </form>
        )}
      ></Form>
    </S.ContactForm>
  );
};

export default ContactForm;
