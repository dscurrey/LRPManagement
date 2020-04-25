package uk.co.dcurrey.owlapp.ui.functions;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.characterItem.CharacterItemEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;
import uk.co.dcurrey.owlapp.sync.Synchroniser;

public class BondItemActivity extends AppCompatActivity
{
    EditText charId;
    EditText itemId;
    Button btnSave;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_bond_item);

        charId = findViewById(R.id.bondChar);
        itemId = findViewById(R.id.bondItem);
        btnSave = findViewById(R.id.saveBond);

        btnSave.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                CharacterItemEntity charItem = new CharacterItemEntity();
                charItem.CharacterId = Integer.parseInt(charId.getText().toString());
                charItem.ItemId = Integer.parseInt(itemId.getText().toString());
                charItem.IsSynced = false;

                saveBond(charItem);

                finish();
            }
        });
    }

    private void saveBond(CharacterItemEntity charItem)
    {
        if (NetworkMonitor.checkNetConnectivity(getApplicationContext()))
        {
            saveAPI(charItem);
        }
        else
        {
            saveLocal(charItem);
        }
    }

    private void saveAPI(CharacterItemEntity charItem)
    {
        Synchroniser synchroniser = new Synchroniser();
        synchroniser.sendToAPI(getApplicationContext(), charItem);
        charItem.IsSynced = true;
        saveLocal(charItem);
    }

    private void saveLocal(CharacterItemEntity charItem)
    {
        Repository.getInstance().getBondRepository().insert(charItem);
    }
}
