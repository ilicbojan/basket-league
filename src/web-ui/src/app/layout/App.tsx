import React, { useContext, useEffect } from 'react';
import { withRouter } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { RootStoreContext } from '../stores/rootStore';
import { ToastContainer } from 'react-toastify';
import Button from '../common/button/Button';
import Input from '../common/form/input/Input';
import { Field, Form } from 'react-final-form';
import Select from '../common/form/select/Select';
import Modal from '../common/modal/Modal';

function App() {
  const rootStore = useContext(RootStoreContext);
  const { token, setAppLoaded, appLoaded } = rootStore.commonStore;
  const { modal } = rootStore.modalStore;

  useEffect(() => {
    if (token) {
      //currentUser().finally(() => setAppLoaded());
    } else {
      setAppLoaded();
    }
  }, [token, setAppLoaded]);

  //if (!appLoaded) return <LoadingSpinner />;

  return (
    <>
      <ToastContainer position='bottom-right' />
      <div>
        <h1>This is React Template</h1>
        <Modal show={modal.open}></Modal>
        <Form
          onSubmit={(values: any) => console.log(values)}
          render={({ handleSubmit, submitting }) => (
            <form onSubmit={handleSubmit}>
              <Field name='test' label='Test' type='text' component={Input} />
              <Field name='select' label='Test' component={Select}>
                <option value='aaa'>aaa</option>
                <option value='bbb'>bbb</option>
              </Field>
              <Button color='primary'>Button</Button>
            </form>
          )}
        />
        {/* <Route exact path='/' component={HomePage} />
        <Route
          path={'/(.+)'}
          render={() => (
            <>
              <Switch>
                <Route exact path='/fields' component={SportObjectList} />
                <PrivateRoute exact path='/profile' component={UserProfile} />
                <PrivateRoute
                  exact
                  path='/favourites'
                  user
                  component={FavouritesList}
                />
                <PrivateRoute
                  exact
                  path='/working-hours'
                  client
                  component={WorkingHours}
                />
                <Route component={NotFound} />
              </Switch>
            </>
          )}
        /> */}
      </div>
    </>
  );
}

export default withRouter(observer(App));
