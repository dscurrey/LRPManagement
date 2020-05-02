package uk.co.dcurrey.owlapp.ui.functions;

import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;

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

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.character.CharacterViewModel;

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
        TextView mItemCountView = root.findViewById(R.id.itemCount);
        TextView mSkillCountView = root.findViewById(R.id.skillCount);
        TextView mPlayerCountView = root.findViewById(R.id.playerCount);

        mViewModel.getCharCount().observe(getViewLifecycleOwner(), integer -> mCharCountView.setText(""+integer));

        mViewModel.getItemCount().observe(getViewLifecycleOwner(), integer -> mItemCountView.setText(""+integer));

        mViewModel.getSkillCount().observe(getViewLifecycleOwner(), integer -> mSkillCountView.setText(""+integer));

        mViewModel.getPlayerCount().observe(getViewLifecycleOwner(), integer -> mPlayerCountView.setText(""+integer));

        return root;
    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState)
    {
        super.onActivityCreated(savedInstanceState);

        Button btnBond = root.findViewById(R.id.btnBond);
        btnBond.setOnClickListener(v -> {
            Intent intent = new Intent(getContext(), BondItemActivity.class);
            startActivity(intent);
        });

        Button btnAddSkill = root.findViewById(R.id.btnCharSkill);
        btnAddSkill.setOnClickListener(v -> {
            Intent intent = new Intent(getContext(), CharSkillActivity.class);
            startActivity(intent);
        });
    }

}
