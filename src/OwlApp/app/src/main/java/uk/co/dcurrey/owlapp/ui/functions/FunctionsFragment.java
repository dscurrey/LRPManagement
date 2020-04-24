package uk.co.dcurrey.owlapp.ui.functions;

import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;
import androidx.lifecycle.ViewModelProviders;

import android.content.Intent;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.character.CharacterViewModel;
import uk.co.dcurrey.owlapp.ui.character.BondItemActivity;
import uk.co.dcurrey.owlapp.ui.items.ItemsViewModel;

public class FunctionsFragment extends Fragment
{
    private CharacterViewModel mCharViewModel;
    private View root;

    public static FunctionsFragment newInstance()
    {
        return new FunctionsFragment();
    }

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState)
    {
        FunctionsViewModel mViewModel = new ViewModelProvider(this).get(FunctionsViewModel.class);
        root = inflater.inflate(R.layout.functions_fragment, container, false);

        TextView mCharCountView = root.findViewById(R.id.charCount);
        mViewModel.getCharCount().observe(getViewLifecycleOwner(), new Observer<Integer>()
        {
            @Override
            public void onChanged(Integer integer)
            {
                mCharCountView.setText(""+integer);
            }
        });

        return root;
    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState)
    {
        super.onActivityCreated(savedInstanceState);

        Button btnBond = root.findViewById(R.id.btnBond);
        btnBond.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent intent = new Intent(getContext(), BondItemActivity.class);
                startActivity(intent);
            }
        });
    }

}
