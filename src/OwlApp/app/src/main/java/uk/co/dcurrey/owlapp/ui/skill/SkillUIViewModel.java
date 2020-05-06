package uk.co.dcurrey.owlapp.ui.skill;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class SkillUIViewModel extends ViewModel
{

    private final MutableLiveData<String> mText;

    public SkillUIViewModel()
    {
        mText = new MutableLiveData<>();
        mText.setValue("This is the skill fragment");
    }

    public LiveData<String> getText()
    {
        return mText;
    }
}