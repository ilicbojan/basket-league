import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import { Field, Form } from 'react-final-form';
import Button from '../../../app/common/button/Button';
import Input from '../../../app/common/form/input/Input';
import Select from '../../../app/common/form/select/Select';
import { hours } from '../../../app/common/util/dates';
import { required } from '../../../app/common/util/validation';
import { IMatch } from '../../../app/models/match';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './MatchCreate.style';

const MatchCreate = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { createMatch } = rootStore.matchStore;
  const { loadTeams, loading: loadingTeams, teams } = rootStore.teamStore;
  const {
    loadSeasons,
    loading: loadingSeasons,
    seasons,
  } = rootStore.seasonStore;

  useEffect(() => {
    loadSeasons();
    loadTeams();
  }, [loadSeasons, loadTeams]);

  return (
    <S.MatchCreate className='admin'>
      <div className='adminForm'>
        <h2>Create Match</h2>
        <Form
          onSubmit={(values: IMatch) => createMatch(values)}
          render={({ handleSubmit, submitting }) => (
            <form onSubmit={handleSubmit}>
              <Field
                name='seasonId'
                label='Season'
                block
                disabled={loadingSeasons}
                component={Select}
              >
                {seasons.map((season) => (
                  <option key={season.id} value={season.id}>
                    {season.name}
                  </option>
                ))}
              </Field>
              <Field
                name='round'
                type='number'
                label='Round'
                block
                validate={required}
                component={Input}
              />
              <Field
                name='homeTeamId'
                label='Home Team'
                block
                disabled={loadingTeams}
                component={Select}
              >
                {teams.map((team) => (
                  <option key={team.id} value={team.id}>
                    {team.name}
                  </option>
                ))}
              </Field>
              <Field
                name='awayTeamId'
                label='Away Team'
                block
                disabled={loadingTeams}
                component={Select}
              >
                {teams.map((team) => (
                  <option key={team.id} value={team.id}>
                    {team.name}
                  </option>
                ))}
              </Field>
              <Field
                name='date'
                type='date'
                label='Date'
                block
                validate={required}
                component={Input}
              />
              <Field name='time' label='Time' block component={Select}>
                {hours.map((hour) => (
                  <option key={hour} value={hour}>
                    {hour}
                  </option>
                ))}
              </Field>
              <Field
                name='refereeId'
                label='Referee'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='1'>Ref</option>
              </Field>
              <Field
                name='delegateId'
                label='Delegate'
                block
                // disabled={loading}
                component={Select}
              >
                {/* {cities.map((city: ICity) => (
                <option key={city.id} value={city.id}>
                {city.name}
                </option>
              ))} */}
                <option value='1'>Del</option>
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
    </S.MatchCreate>
  );
});

export default MatchCreate;
