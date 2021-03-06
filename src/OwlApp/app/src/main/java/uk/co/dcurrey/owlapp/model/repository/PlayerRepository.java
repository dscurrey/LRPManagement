package uk.co.dcurrey.owlapp.model.repository;

import android.os.AsyncTask;
import android.util.Log;

import java.util.HashMap;
import java.util.concurrent.ExecutionException;

import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.player.PlayerDao;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;

public enum PlayerRepository
{
    INSTANCE;

    private final PlayerDao mPlayerDao;
    private HashMap<Integer, PlayerEntity> mPlayers;

    PlayerRepository()
    {
        mPlayerDao = OwlDatabase.getDb().playerDao();
        mPlayers = new HashMap<>();

        loadFromDatabase();
    }

    private void loadFromDatabase()
    {
        try
        {
            mPlayers = new loadPlayerEntitiesAsyncTask(mPlayerDao, mPlayers).execute().get();
        } catch (ExecutionException e)
        {
            e.printStackTrace();
        } catch (InterruptedException e)
        {
            e.printStackTrace();
        }
    }

    public PlayerEntity get(int id)
    {
        loadFromDatabase();
        return mPlayers.get(id);
    }

    public HashMap<Integer, PlayerEntity> get()
    {
        mPlayers.clear();
        loadFromDatabase();
        return mPlayers;
    }

    public void insert(PlayerEntity player)
    {
        try
        {
            new insertPlayerEntitiesAsyncTask(mPlayerDao, player).execute().get();
        } catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "An error occurred inserting characters into the database", e);
        }
    }

    private static class loadPlayerEntitiesAsyncTask extends AsyncTask<Void, Void, HashMap<Integer, PlayerEntity>>
    {
        private final PlayerDao mPlayerDao;
        private final HashMap<Integer, PlayerEntity> mPlayerEntities;

        loadPlayerEntitiesAsyncTask(PlayerDao playerDao, HashMap<Integer, PlayerEntity> playerEntities)
        {
            mPlayerDao = playerDao;
            mPlayerEntities = playerEntities;
        }

        protected HashMap<Integer, PlayerEntity> doInBackground(final Void... voids)
        {
            for (PlayerEntity playerEntity : mPlayerDao.get())
            {
                mPlayerEntities.put(playerEntity.Id, playerEntity);
            }
            return mPlayerEntities;
        }
    }

    private static class insertPlayerEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private final PlayerDao mDao;
        private final PlayerEntity playerEntity;

        insertPlayerEntitiesAsyncTask(PlayerDao dao, PlayerEntity player)
        {
            mDao = dao;
            playerEntity = player;
        }

        protected Void doInBackground(final Void... voids)
        {
            mDao.insertAll(playerEntity);
            return null;
        }
    }

    public void update(PlayerEntity playerEntity)
    {
        try
        {
            new updatePlayerEntitiesAsyncTask(mPlayerDao, playerEntity).execute().get();
        } catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "DB Error Occurred");
        }
    }

    private static class updatePlayerEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private final PlayerDao mDao;
        private final PlayerEntity playerEntity;

        updatePlayerEntitiesAsyncTask(PlayerDao playerDao, PlayerEntity player)
        {
            mDao = playerDao;
            playerEntity = player;
        }

        protected Void doInBackground(final Void... voids)
        {
            mDao.update(playerEntity);
            return null;
        }
    }
}
