package uk.co.dcurrey.owlapp.database;

import android.content.Context;

import androidx.annotation.NonNull;
import androidx.room.Database;
import androidx.room.Room;
import androidx.room.RoomDatabase;
import androidx.sqlite.db.SupportSQLiteDatabase;

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
                    INSTANCE = Room.databaseBuilder(context.getApplicationContext(), OwlDatabase.class, "owl_db").addCallback(sOwlDatabaseCallback).build();
                }
            }
        }
        return INSTANCE;
    }

    private static OwlDatabase.Callback sOwlDatabaseCallback = new RoomDatabase.Callback()
    {
        @Override
        public void onOpen(@NonNull SupportSQLiteDatabase db)
        {
            // Test data (Re-applied every restart)
            databaseWriteExecutor.execute(() ->
            {
                // populate
                CharacterDao dao = INSTANCE.characterDao();
                dao.deleteAll();

                CharacterEntity character = new CharacterEntity();
                character.Name = "Test1";
                dao.insertAll(character);
                character.Name = "Test2";
                dao.insertAll(character);
            });
        }
    };
}
