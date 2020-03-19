package uk.co.dcurrey.owlapp.database.skill;

import android.app.Application;

import androidx.lifecycle.AndroidViewModel;
import androidx.lifecycle.LiveData;

import java.util.List;

public class SkillViewModel extends AndroidViewModel
{
    private SkillRepository mRepo;
    private LiveData<List<SkillEntity>> mSkills;

    public SkillViewModel(Application application)
    {
        super(application);
        mRepo = new SkillRepository(application);
        mSkills = mRepo.getAllSkills();
    }

    public LiveData<List<SkillEntity>> getAllSkills()
    {
        return mSkills;
    }

    public void insert(SkillEntity skill)
    {
        mRepo.insert(skill);
    }
}
