import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect } from 'react';
import { Field, Form } from 'react-final-form';
import { useParams } from 'react-router-dom';
import Button from '../../../app/common/button/Button';
import LoadingSpinner from '../../../app/layout/spinner/LoadingSpinner';
import { ILineupFormValues } from '../../../app/models/lineup';
import { RootStoreContext } from '../../../app/stores/rootStore';
import { S } from './LineupCreate.style';

interface IParam {
  matchId: string;
  teamId: string;
}

const LineupCreate = observer(() => {
  const rootStore = useContext(RootStoreContext);
  const { setPredicate, players, loadingPlayers } = rootStore.playerStore;
  const { createLineup } = rootStore.matchPlayerStore;

  const { matchId, teamId } = useParams<IParam>();
  const matchID = Number.parseInt(matchId);
  const teamID = Number.parseInt(teamId);

  useEffect(() => {
    setPredicate('teamId', teamId + '');
  }, [setPredicate]);

  const handleFormSubmit = (values: ILineupFormValues) => {
    values.teamId = teamID;
    values.matchId = matchID;
    createLineup(values);
  };

  if (loadingPlayers) return <LoadingSpinner />;

  return (
    <S.LineupCreate>
      <div className='adminForm'>
        <h2>Create Lineup</h2>
        <Form
          onSubmit={(values: ILineupFormValues) => handleFormSubmit(values)}
          render={({ handleSubmit, submitting }) => (
            <form onSubmit={handleSubmit}>
              <div className='checkboxes'>
                {players.map((player) => (
                  <div key={player.id} className='checkbox'>
                    <label>
                      <Field
                        name='playersIds'
                        component='input'
                        type='checkbox'
                        value={player.id}
                      />
                      {player.firstName} {player.lastName}
                    </label>
                  </div>
                ))}
              </div>

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
        />
      </div>
    </S.LineupCreate>
  );
});

export default LineupCreate;
