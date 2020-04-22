package uk.co.dcurrey.owlapp.database;

import android.content.Context;

import androidx.annotation.NonNull;
import androidx.room.Database;
import androidx.room.Room;
import androidx.room.RoomDatabase;
import androidx.sqlite.db.SupportSQLiteDatabase;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import uk.co.dcurrey.owlapp.database.character.CharacterDao;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerDao;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillDao;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;

@Database(
        entities = {
                CharacterEntity.class,
                SkillEntity.class,
                PlayerEntity.class
        },
        version = 2,
        exportSchema = false
)
public abstract class OwlDatabase extends RoomDatabase
{
    public abstract CharacterDao characterDao();
    public abstract SkillDao skillDao();
    public abstract PlayerDao playerDao();

    private static volatile OwlDatabase INSTANCE;
    private static final int NUMBER_THREADS = 4;
    public static final ExecutorService databaseWriteExecutor = Executors.newFixedThreadPool(NUMBER_THREADS);

    public static OwlDatabase getDb(final Context context)
    {
        if (INSTANCE == null)
        {
            synchronized (OwlDatabase.class)
            {
                if (INSTANCE == null)
                {
                    INSTANCE = Room.databaseBuilder(context.getApplicationContext(), OwlDatabase.class, "owl_db").fallbackToDestructiveMigration().addCallback(sOwlDatabaseCallback).build();
                }
            }
        }
        return INSTANCE;
    }

    public static OwlDatabase getDb()
    {
        if (INSTANCE == null)
        {
            throw new IllegalStateException();
        }
        return INSTANCE;
    }

    private static OwlDatabase.Callback sOwlDatabaseCallback = new RoomDatabase.Callback()
    {
        @Override
        public void onOpen(@NonNull SupportSQLiteDatabase db)
        {
            // Test data (Re-applied every restart)
//            databaseWriteExecutor.execute(() ->
//            {
//                // populate
//                PlayerDao playDao = INSTANCE.playerDao();
//                playDao.deleteAll();
//                PlayerEntity player = new PlayerEntity();
//                player.FirstName = "Player";
//                player.LastName = "1";
//                playDao.insertAll(player);
//                player.FirstName = "John";
//                player.LastName = "Smith";
//                playDao.insertAll(player);
//
//                SkillDao skillDao = INSTANCE.skillDao();
//                skillDao.deleteAll();
//                SkillEntity skill = new SkillEntity();
//                skill.Name = "Skill 1";
//                skillDao.insertAll(skill);
//                skill.Name = "Test Skill";
//                skillDao.insertAll(skill);
//            });
        }
    };
}
