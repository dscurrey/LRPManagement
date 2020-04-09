package uk.co.dcurrey.owlapp.ui.player;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class PlayerDetailsActivity extends AppCompatActivity
{
    PlayerEntity player;
    EditText playerFname;
    EditText playerLname;
    Button saveChanges;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_player_details);
        popViews();

        int playerId = getSharedPreferences(getApplicationContext().getString(R.string.pref_playerId_key), Context.MODE_PRIVATE).getInt(getApplicationContext().getString(R.string.pref_playerId_key), 0);

        player = Repository.getInstance().getPlayerRepository().get(playerId);

        playerFname.setText(player.FirstName);
        playerLname.setText(player.LastName);

        saveChanges.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Toast.makeText(PlayerDetailsActivity.this, "DEBUG: Player Updated", Toast.LENGTH_SHORT).show();
                if (player.FirstName != playerFname.getText().toString() || player.LastName != playerLname.getText().toString())
                {
                    player.FirstName = playerFname.getText().toString();
                    player.LastName = playerLname.getText().toString();
                    player.IsSynced = false;
                    Repository.getInstance().getPlayerRepository().update(player);
                }
                finish();
            }
        });

    }

    private void popViews()
    {
        playerFname = findViewById(R.id.detailsPlayerFName);
        playerLname = findViewById(R.id.detailsPlayerLName);
        saveChanges = findViewById(R.id.btnEditPlayer);
    }
}
