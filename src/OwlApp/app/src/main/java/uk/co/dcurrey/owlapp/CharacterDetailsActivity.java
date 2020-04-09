package uk.co.dcurrey.owlapp;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.LiveData;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProvider;

import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.character.CharacterViewModel;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class CharacterDetailsActivity extends AppCompatActivity
{
    EditText charName;
    CheckBox isRetired;
    CharacterEntity character;
    Button saveChanges;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_character_details);
        popViews();

        int charId = getSharedPreferences(getApplicationContext().getString(R.string.pref_charId_key), Context.MODE_PRIVATE).getInt(getApplicationContext().getString(R.string.pref_charId_key), 0);

        character = Repository.getInstance().getCharacterRepository().get(charId);

        charName.setText(character.Name);
        isRetired.setChecked(character.IsRetired);

        saveChanges.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Toast.makeText(CharacterDetailsActivity.this, "DEBUG: Character Updated", Toast.LENGTH_SHORT).show();
                finish();
            }
        });
    }

    private void popViews()
    {
        charName = findViewById(R.id.detailsCharName);
        isRetired = findViewById(R.id.detailsCharIsRetired);
        saveChanges = findViewById(R.id.btnEditChar);
    }
}
