package uk.co.dcurrey.owlapp.database.item;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.OnConflictStrategy;
import androidx.room.Query;
import androidx.room.Update;

import java.util.List;

@Dao
public interface ItemDao
{
    @Query("SELECT * FROM item")
    LiveData<List<ItemEntity>> getAll();

    @Query("SELECT * FROM item WHERE id IN (:itemIds)")
    List<ItemEntity> loadAllById(int[] itemIds);

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    void insertAll(ItemEntity... item);

    @Delete
    void deleteItem(ItemEntity item);

    @Update
    void update(ItemEntity item);

    @Query("DELETE FROM item")
    void deleteAll();

    @Query("SELECT * FROM item")
    List<ItemEntity> get();

    @Query("SELECT * FROM item WHERE Id = :id")
    ItemEntity get(int id);

    @Query("SELECT * FROM item WHERE Id IN (:Ids)")
    List<ItemEntity> get(int... Ids);
}
