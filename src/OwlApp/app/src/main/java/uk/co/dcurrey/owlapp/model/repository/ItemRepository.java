package uk.co.dcurrey.owlapp.model.repository;

import android.os.AsyncTask;
import android.util.Log;

import java.util.HashMap;
import java.util.concurrent.ExecutionException;

import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.item.ItemDao;
import uk.co.dcurrey.owlapp.database.item.ItemEntity;

public enum ItemRepository
{
    INSTANCE;

    private ItemDao mItemDao;

    private HashMap<Integer, ItemEntity> mItems;

    ItemRepository()
    {
        mItemDao = OwlDatabase.getDb().itemDao();
        mItems = new HashMap<>();

        loadFromDatabase();
    }

    private void loadFromDatabase()
    {
        try
        {
            mItems = new loadItemEntitiesAsyncTask(mItemDao, mItems).execute().get();
        } catch (InterruptedException e)
        {
            Log.e(this.name(), "Error Occurred reading from DB");
            e.printStackTrace();
        } catch (ExecutionException e)
        {
            Log.e(this.name(), "Error Occurred reading from DB");
            e.printStackTrace();
        }
    }

    public ItemEntity get(int id)
    {
        loadFromDatabase();
        return mItems.get(id);
    }

    public HashMap<Integer, ItemEntity> get()
    {
        mItems.clear();
        loadFromDatabase();
        return mItems;
    }

    public void insert(ItemEntity item)
    {
        try
        {
            new insertItemEntitiesAsyncTask(mItemDao, item).execute().get();
        } catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "An error occurred inserting items into the database", e);
        }
    }

    private static class loadItemEntitiesAsyncTask extends AsyncTask<Void, Void, HashMap<Integer, ItemEntity>>
    {
        private ItemDao mItemDao;
        private HashMap<Integer, ItemEntity> mItemEntities;


        loadItemEntitiesAsyncTask(ItemDao itemDao, HashMap<Integer, ItemEntity> itemEntities)
        {
            mItemDao = itemDao;
            mItemEntities = itemEntities;
        }

        protected HashMap<Integer, ItemEntity> doInBackground(final Void... voids)
        {
            for (ItemEntity itemEntity : mItemDao.get())
            {
                mItemEntities.put(itemEntity.Id, itemEntity);
            }
            return mItemEntities;
        }
    }

    private static class insertItemEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private ItemDao mDao;
        private ItemEntity itemEntity;

        insertItemEntitiesAsyncTask(ItemDao itemDao, ItemEntity item)
        {
            mDao = itemDao;
            itemEntity = item;
        }

        protected Void doInBackground(final Void... voids)
        {
            mDao.insertAll(itemEntity);
            return null;
        }
    }

    public void update(ItemEntity itemEntity)
    {
        try
        {
            new updateItemEntitiesAsyncTask(mItemDao, itemEntity).execute().get();
        } catch (ExecutionException | InterruptedException e)
        {
            Log.e(this.name(), "DB Error Occurred");
        }
    }

    private static class updateItemEntitiesAsyncTask extends AsyncTask<Void, Void, Void>
    {
        private ItemDao mDao;
        private ItemEntity itemEntity;

        updateItemEntitiesAsyncTask(ItemDao itemDao, ItemEntity item)
        {
            mDao = itemDao;
            itemEntity = item;
        }

        protected Void doInBackground(final Void... voids)
        {
            mDao.update(itemEntity);
            return null;
        }
    }
}
