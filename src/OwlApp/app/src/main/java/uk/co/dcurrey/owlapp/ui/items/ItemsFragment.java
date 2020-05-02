package uk.co.dcurrey.owlapp.ui.items;

import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;

import android.content.Intent;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.Toast;

import com.google.android.material.floatingactionbutton.FloatingActionButton;

import java.util.ArrayList;
import java.util.List;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.item.ItemEntity;
import uk.co.dcurrey.owlapp.database.item.ItemViewModel;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;
import uk.co.dcurrey.owlapp.sync.Synchroniser;

import static android.app.Activity.RESULT_OK;

public class ItemsFragment extends Fragment
{

    private ItemViewModel mViewModel;
    public static final int NEW_ITEM_ACTIVITY_REQUEST_CODE = 1;
    private ItemListAdapter adapter;
    private EditText searchTerm;
    private List<ItemEntity> items;

    public static ItemsFragment newInstance()
    {
        return new ItemsFragment();
    }

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState)
    {
        View root = inflater.inflate(R.layout.items_fragment, container, false);

        searchTerm = root.findViewById(R.id.itemSearch);

        // FAB
        FloatingActionButton fab = root.findViewById(R.id.fab);
        fab.setVisibility(View.INVISIBLE);

        // Recycler
        RecyclerView recyclerView = root.findViewById(R.id.recyclerview_item);
        searchTerm = root.findViewById(R.id.itemSearch);
        adapter = new ItemListAdapter(getContext());
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        return root;
    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState)
    {
        super.onActivityCreated(savedInstanceState);

        mViewModel = new ViewModelProvider(this).get(ItemViewModel.class);
        mViewModel.getAllItems().observe(getViewLifecycleOwner(), itemEntities -> {
            items = itemEntities;
            adapter.setItems(items);
        });

        searchTerm.addTextChangedListener(new TextWatcher()
        {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after)
            {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count)
            {

            }

            @Override
            public void afterTextChanged(Editable s)
            {
                filter(s.toString());
            }
        });
    }

    private void filter(String text)
    {
        List<ItemEntity> filteredItems = new ArrayList<>();
        for (ItemEntity i : items)
        {
            if(i.Name.toLowerCase().contains(text.toLowerCase()) || String.valueOf(i.Id).contains(text))
            {
                filteredItems.add(i);
            }
        }
        adapter.setItems(filteredItems);
    }

    public void onActivityResult(int reqCode, int resCode, Intent data)
    {
        super.onActivityResult(reqCode, reqCode, data);

        if (reqCode == NEW_ITEM_ACTIVITY_REQUEST_CODE && resCode == RESULT_OK)
        {
            ItemEntity itemEntity = new ItemEntity();
            itemEntity.Name = data.getStringExtra(NewItemActivity.EXTRA_REPLY_ITEMNAME);
            saveItem(itemEntity);
        }
        else
        {
            Toast.makeText(getContext(), R.string.empty_not_saved, Toast.LENGTH_LONG).show();
        }
    }

    private void saveItem(ItemEntity itemEntity)
    {
        if (NetworkMonitor.checkNetConnectivity(getContext()))
        {
            saveAPI(itemEntity);
        }
        else
        {
            saveLocal(itemEntity);
        }
    }

    private void saveLocal(ItemEntity itemEntity)
    {
        mViewModel.insert(itemEntity);
    }

    private void saveAPI(ItemEntity itemEntity)
    {
        Synchroniser synchroniser = new Synchroniser();
        synchroniser.sendToAPI(getContext(), itemEntity);
        itemEntity.IsSynced = true;
        saveLocal(itemEntity);
    }
}
