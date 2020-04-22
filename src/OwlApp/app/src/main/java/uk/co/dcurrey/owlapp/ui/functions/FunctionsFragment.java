package uk.co.dcurrey.owlapp.ui.functions;

import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import uk.co.dcurrey.owlapp.R;

public class FunctionsFragment extends Fragment
{

    private FunctionsViewModel mViewModel;

    public static FunctionsFragment newInstance()
    {
        return new FunctionsFragment();
    }

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState)
    {
        mViewModel = ViewModelProviders.of(this).get(FunctionsViewModel.class);
        View root = inflater.inflate(R.layout.functions_fragment, container, false);
        final TextView textView = root.findViewById(R.id.txtFunction);
        mViewModel.getText().observe(getViewLifecycleOwner(), new Observer<String>()
        {
            @Override
            public void onChanged(String s)
            {
                textView.setText(s);
            }
        });

        return root;
    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState)
    {
        super.onActivityCreated(savedInstanceState);
    }

}
