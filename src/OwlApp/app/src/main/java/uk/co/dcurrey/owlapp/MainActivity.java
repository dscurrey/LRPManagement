package uk.co.dcurrey.owlapp;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.google.android.material.navigation.NavigationView;

import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;
import uk.co.dcurrey.owlapp.sync.Synchroniser;

public class MainActivity extends AppCompatActivity
{
    // TODO - Remove editing of certain entities
    NetworkMonitor networkMonitor;

    private AppBarConfiguration mAppBarConfiguration;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);

        OwlDatabase.getDb(this);

        setupNetworkMonitor();

        setContentView(R.layout.activity_main);
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        NavigationView navigationView = findViewById(R.id.nav_view);
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_functions, R.id.nav_home, R.id.nav_player, R.id.nav_skill, R.id.nav_items)
                .setDrawerLayout(drawer)
                .build();
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, mAppBarConfiguration);
        NavigationUI.setupWithNavController(navigationView, navController);
    }

    private void setupNetworkMonitor()
    {
        networkMonitor = new NetworkMonitor();
        networkMonitor.enable(this);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu)
    {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
        int id = item.getItemId();
        switch (id)
        {
            case R.id.action_settings:
                // Settings
                Intent settingsIntent = new Intent(MainActivity.this, SettingsActivity.class);
                MainActivity.this.startActivity(settingsIntent);
                return true;

            case R.id.action_getapi:
                // Update DB
                if (NetworkMonitor.checkNetConnectivity(this))
                {
                    PromptUpdate();
                }
                else
                {
                    Toast.makeText(this, "Not Connected to Network", Toast.LENGTH_SHORT).show();
                }
                return true;

            case R.id.action_forcesync:
                if (NetworkMonitor.checkNetConnectivity(this))
                {
                    Synchroniser mSync = new Synchroniser();
                    mSync.SyncDbToAPI(getApplicationContext());
                }
                else
                {
                    Toast.makeText(this, "Not Connected to Network", Toast.LENGTH_SHORT).show();
                }
                return true;

            default:
                return super.onOptionsItemSelected(item);
        }
    }

    @Override
    public boolean onSupportNavigateUp()
    {
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        return NavigationUI.navigateUp(navController, mAppBarConfiguration)
                || super.onSupportNavigateUp();
    }

    private void PromptUpdate()
    {
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(this);

        dialogBuilder.setTitle(R.string.prompt_title);

        dialogBuilder.setPositiveButton(R.string.prompt_pos, (dialog, which) -> Synchroniser.resetDb(getApplicationContext()))
        .setNegativeButton(R.string.cancel, (dialog, which) -> {
        });

        AlertDialog dialog = dialogBuilder.create();
        dialog.show();
    }
}
