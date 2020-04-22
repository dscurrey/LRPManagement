package uk.co.dcurrey.owlapp.ui.functions;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class FunctionsViewModel extends ViewModel
{
    private MutableLiveData<String> mText;

    public FunctionsViewModel()
    {
        mText = new MutableLiveData<>();
        mText.setValue("This is the functions page");
    }

    public LiveData<String> getText()
    {
        return mText;
    }
}
