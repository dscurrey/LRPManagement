package uk.co.dcurrey.owlapp.database.item;

import android.app.Application;

import androidx.lifecycle.LiveData;

import java.util.List;

import uk.co.dcurrey.owlapp.database.OwlDatabase;

public class ItemRepository
{
    private ItemDao mItemDao;
    private LiveData<List<ItemEntity>> mAllItems;

    public ItemRepository(Application application)
    {
        OwlDatabase db = OwlDatabase.getDb(application);
        mItemDao = db.itemDao();
        mAllItems = mItemDao.getAll();
    }

    public LiveData<List<ItemEntity>> getAllItems()
    {
        return mAllItems;
    }

    public void insert(ItemEntity item)
    {
        OwlDatabase.databaseWriteExecutor.execute(() -> mItemDao.insertAll(item));
    }

    void update(ItemEntity itemEntity)
    {
        OwlDatabase.databaseWriteExecutor.execute(() ->
                mItemDao.update(itemEntity));
    }
}
