import React, { useContext, useEffect } from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { RootStoreContext } from '../stores/rootStore';
import { ToastContainer } from 'react-toastify';
import Modal from '../common/modal/Modal';
import HomePage from '../../features/home/page/HomePage';
import Nav from './nav/navigation/Nav';
import SeasonDetails from '../../features/seasons/details/SeasonDetails';
import MatchDetails from '../../features/matches/details/MatchDetails';
import TeamDetails from '../../features/teams/details/TeamDetails';
import PlayerDetails from '../../features/players/details/PlayerDetails';
import LeagueCreate from '../../features-admin/leagues/create/LeagueCreate';
import SeasonCreate from '../../features-admin/seasons/SeasonCreate';
import FieldCreate from '../../features-admin/fields/create/FieldCreate';
import TeamCreate from '../../features-admin/teams/create/TeamCreate';
import PlayerCreate from '../../features-admin/players/create/PlayerCreate';
import MatchCreate from '../../features-admin/matches/create/MatchCreate';
import MatchManage from '../../features-admin/matches/manage/MatchManage';
import LineupCreate from '../../features-admin/lineup/create/LineupCreate';
import ContactPage from '../../features/contact/page/ContactPage';

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
      <div className='app'>
        <Nav />
        <div className='appContainer'>
          <Modal show={modal.open}></Modal>
          <Route exact path='/' component={HomePage} />
          <Route
            path={'/(.+)'}
            render={() => (
              <>
                <Switch>
                  <Route
                    exact
                    path='/seasons/create'
                    component={SeasonCreate}
                  />
                  <Route exact path='/seasons/:id' component={SeasonDetails} />
                  <Route exact path='/matches/create' component={MatchCreate} />
                  <Route
                    exact
                    path='/matches/:matchId/teams/:teamId/lineup'
                    component={LineupCreate}
                  />
                  <Route
                    exact
                    path='/matches/:id/manage'
                    component={MatchManage}
                  />
                  <Route exact path='/matches/:id' component={MatchDetails} />
                  <Route exact path='/teams/create' component={TeamCreate} />
                  <Route exact path='/teams/:id' component={TeamDetails} />
                  <Route
                    exact
                    path='/players/create'
                    component={PlayerCreate}
                  />
                  <Route exact path='/players/:id' component={PlayerDetails} />
                  <Route
                    exact
                    path='/leagues/create'
                    component={LeagueCreate}
                  />
                  <Route exact path='/fields/create' component={FieldCreate} />
                  <Route exact path='/contact' component={ContactPage} />
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
      </div>
    </>
  );
}

export default withRouter(observer(App));
