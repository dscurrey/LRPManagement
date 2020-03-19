package uk.co.dcurrey.owlapp.database;

import android.content.Context;

import androidx.room.Database;
import androidx.room.Room;
import androidx.room.RoomDatabase;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

@Database(
        entities = {
                CharacterEntity.class
        },
        version = 1,
        exportSchema = false
)
public abstract class OwlDatabase extends RoomDatabase
{
    public abstract CharacterDao characterDao();

    private static volatile OwlDatabase INSTANCE;
    private static final int NUMBER_THREADS = 4;
    static final ExecutorService databaseWriteExecutor = Executors.newFixedThreadPool(NUMBER_THREADS);

    static OwlDatabase getDb(final Context context)
    {
        if (INSTANCE == null)
        {
            synchronized (OwlDatabase.class)
            {
                if (INSTANCE == null)
                {
                    INSTANCE = Room.databaseBuilder(context.getApplicationContext(), OwlDatabase.class, "owl_db").build();
                }
            }
        }
        return INSTANCE;
    }
}
