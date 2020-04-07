package uk.co.dcurrey.owlapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.widget.TextView;

public class PlayerDetailsActivity extends AppCompatActivity
{

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_player_details);

        int playerId = getSharedPreferences(getApplicationContext().getString(R.string.pref_playerId_key), Context.MODE_PRIVATE).getInt(getApplicationContext().getString(R.string.pref_playerId_key), 0);
        TextView textView = findViewById(R.id.textView3);
        textView.setText(textView.getText().toString()+"\n ID: "+playerId);
    }
}
