package uk.co.dcurrey.owlapp.database.characterSkill;

import android.app.Application;

import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;

import java.util.List;

public class CharacterSkillViewModel extends AndroidViewModel
{
    private CharacterSkillRepository mRepo;
    private LiveData<List<CharacterSkillEntity>> mCharSkills;

    CharacterSkillViewModel(Application application)
    {
        super(application);
        mRepo = new CharacterSkillRepository(application);
        mCharSkills = mRepo.getAll();
    }

    public LiveData<List<CharacterSkillEntity>> getAll()
    {
        return mCharSkills;
    }

    public void insert(CharacterSkillEntity charSkill)
    {
        mRepo.insert(charSkill);
    }
}
