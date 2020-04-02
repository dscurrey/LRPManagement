package uk.co.dcurrey.owlapp.sync;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.Network;
import android.net.NetworkCapabilities;
import android.net.NetworkInfo;
import android.net.NetworkRequest;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.lifecycle.ViewModelProvider;
import androidx.lifecycle.ViewModelStore;
import androidx.lifecycle.ViewModelStoreOwner;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import uk.co.dcurrey.owlapp.api.APIPaths;
import uk.co.dcurrey.owlapp.api.VolleySingleton;
import uk.co.dcurrey.owlapp.database.OwlDatabase;
import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class NetworkMonitor extends ConnectivityManager.NetworkCallback
{
    final NetworkRequest networkRequest;
    private Context mContext;

    public NetworkMonitor()
    {
        networkRequest = new NetworkRequest.Builder()
                .addTransportType(NetworkCapabilities.TRANSPORT_CELLULAR)
                .addTransportType(NetworkCapabilities.TRANSPORT_WIFI)
                .build();
    }

    public void enable(Context context)
    {
        mContext = context;
        ConnectivityManager connectivityManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        connectivityManager.registerNetworkCallback(networkRequest, this);
    }

    @Override
    public void onAvailable(Network network)
    {
        HashMap<Integer, CharacterEntity> characters = Repository.getInstance().getCharacterRepository().get();

        Iterator it = characters.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry charEntry = (Map.Entry)it.next();
            CharacterEntity character = (CharacterEntity) charEntry.getValue();
            if (!character.IsSynced){
                Synchroniser.sendToAPI(mContext, character);
            }
        }

        HashMap<Integer, PlayerEntity> players = Repository.getInstance().getPlayerRepository().get();

        it = players.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry playEntry = (Map.Entry)it.next();
            PlayerEntity player = (PlayerEntity) playEntry.getValue();
            if (!player.IsSynced){
                Synchroniser.sendToAPI(mContext, player);
            }
        }

        HashMap<Integer, SkillEntity> skills = Repository.getInstance().getSkillRepository().get();

        it = skills.entrySet().iterator();
        while (it.hasNext())
        {
            Map.Entry skillEntry = (Map.Entry)it.next();
            SkillEntity skill = (SkillEntity) skillEntry.getValue();
            if (!skill.IsSynced){
                Synchroniser.sendToAPI(mContext, skill);
            }
        }
    }
}
