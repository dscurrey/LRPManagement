package uk.co.dcurrey.owlapp.ui.functions;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;
import androidx.lifecycle.ViewModelProvider;
import androidx.lifecycle.ViewModelProviders;

import java.util.HashMap;

import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;
import uk.co.dcurrey.owlapp.ui.items.ItemsViewModel;

public class FunctionsViewModel extends ViewModel
{
    private MutableLiveData<String> mText;
    private MutableLiveData<Integer> mTotalCharacters;

    public FunctionsViewModel()
    {
        mText = new MutableLiveData<>();
        mText.setValue("This is the functions page");

        mTotalCharacters = new MutableLiveData<>();
        HashMap<Integer, CharacterEntity> chars = Repository.getInstance().getCharacterRepository().get();
        mTotalCharacters.setValue(chars.size());
    }

    public LiveData<String> getText()
    {
        return mText;
    }

    public LiveData<Integer> getCharCount()
    {
        return mTotalCharacters;
    }
}
