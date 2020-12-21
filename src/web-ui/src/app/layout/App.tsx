import React, { useContext, useEffect } from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { RootStoreContext } from '../stores/rootStore';
import { ToastContainer } from 'react-toastify';
import Modal from '../common/modal/Modal';
import SeasonStandings from '../../features/seasons/SeasonStandings';
import HomePage from '../../features/home/HomePage';

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
        <Modal show={modal.open}></Modal>
        <Route exact path='/' component={HomePage} />
        <Route
          path={'/(.+)'}
          render={() => (
            <>
              <Switch>
                <Route exact path='/seasons/:id' component={SeasonStandings} />
                {/* <PrivateRoute exact path='/profile' component={UserProfile} />
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
                <Route component={NotFound} /> */}
              </Switch>
            </>
          )}
        />
      </div>
    </>
  );
}

export default withRouter(observer(App));
