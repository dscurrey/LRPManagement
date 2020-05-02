package uk.co.dcurrey.owlapp.ui.character;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Toast;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
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

        saveChanges.setOnClickListener(v -> {
            Toast.makeText(CharacterDetailsActivity.this, "DEBUG: Character Updated", Toast.LENGTH_SHORT).show();

            if (character.Name != charName.getText().toString() || character.IsRetired != isRetired.isChecked())
            {
                character.Name = charName.getText().toString();
                character.IsRetired = isRetired.isChecked();
                character.IsSynced = false;
                Repository.getInstance().getCharacterRepository().update(character);
            }
            finish();
        });
    }

    private void popViews()
    {
        charName = findViewById(R.id.detailsCharName);
        isRetired = findViewById(R.id.detailsCharIsRetired);
        saveChanges = findViewById(R.id.btnEditCharacter);
    }
}
