package uk.co.dcurrey.owlapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.widget.TextView;

public class CharacterDetailsActivity extends AppCompatActivity
{

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_character_details);

        int charId = getSharedPreferences(getApplicationContext().getString(R.string.pref_charId_key), Context.MODE_PRIVATE).getInt(getApplicationContext().getString(R.string.pref_charId_key), 0);
        TextView textView = findViewById(R.id.textView2);
        textView.setText(textView.getText().toString()+"\n ID: "+charId);
    }
}
