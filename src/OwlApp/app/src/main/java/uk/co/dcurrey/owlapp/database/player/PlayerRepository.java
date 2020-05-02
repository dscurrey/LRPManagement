package uk.co.dcurrey.owlapp.database.player;

import android.app.Application;

import androidx.lifecycle.LiveData;

import java.util.List;

import uk.co.dcurrey.owlapp.database.OwlDatabase;

public class PlayerRepository
{
    private PlayerDao mDao;
    private LiveData<List<PlayerEntity>> mPlayers;

    PlayerRepository(Application application)
    {
        OwlDatabase db = OwlDatabase.getDb(application);
        mDao = db.playerDao();
        mPlayers = mDao.getAll();
    }

    LiveData<List<PlayerEntity>> getAllPlayers()
    {
        return mPlayers;
    }

    void insert(PlayerEntity player)
    {
        OwlDatabase.databaseWriteExecutor.execute(() ->
                mDao.insertAll(player));
    }

    void update(PlayerEntity player)
    {
        mDao.update(player);
    }
}
