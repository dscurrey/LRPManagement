package uk.co.dcurrey.owlapp.database.characterItem;

import android.app.Application;

import androidx.lifecycle.LiveData;

import java.util.List;

import uk.co.dcurrey.owlapp.database.OwlDatabase;

public class CharacterItemRepository
{
    private CharacterItemDao mDao;
    private LiveData<List<CharacterItemEntity>> mBonds;

    CharacterItemRepository(Application application)
    {
        OwlDatabase db = OwlDatabase.getDb(application);
        mDao = db.charItemDao();
        mBonds = mDao.getAll();
    }

    LiveData<List<CharacterItemEntity>> getAllBonds()
    {
        return mBonds;
    }

    void insert(CharacterItemEntity charItem)
    {
        OwlDatabase.databaseWriteExecutor.execute(() -> {
            mDao.insert(charItem);
        });
    }
}
